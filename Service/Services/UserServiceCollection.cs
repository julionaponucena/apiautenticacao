using Service.Interfaces;
using Infra.Repositories.Interfaces;
using Model;
using Service.Validations;
using API.Errors;

namespace Service.Services;

public class UserServiceCollection : IUserServiceCollection
{
    private readonly IUserRepository _userRepository;
    private readonly IHashProvider _hashProvider;
    private readonly ITokenProvider _tokenProvider;

    public UserServiceCollection(IUserRepository userRepository, IHashProvider hashProvider, ITokenProvider tokenProvider)
    {
        _userRepository = userRepository;
        _hashProvider = hashProvider;
        _tokenProvider = tokenProvider;
    }

    public User? FindUserByUsername(string username)
    {
        return _userRepository.FindByUsername(username);
    }

    public User SignUp(ValidateUserProps userProps)
    {
        userProps.Validate();

        // Verificar se username nao este sendo utilizado

        if (FindUserByUsername(userProps.username) != null)
        {
            throw new AppError("Username já utilizado", 400);
        }

        // Realizar o Hash do nosso Password

        var hashedPassword = _hashProvider.HashPassword(userProps.password);

        // Cria o usuario no Banco de Dados

        var user = new User
        {
            id = Guid.NewGuid(),
            username = userProps.username,
            password = hashedPassword,
            name = userProps.name,
        };

        _userRepository.Create(user);

        return user;
    }

    public string SignIn(ValidateUserSignIn userProps)
    {
        userProps.Validate();

        var user = FindUserByUsername(userProps.username);

        if (user == null)
            throw new AppError("Usuario ou senha invalidos", 400);

        var isPasswordValid = _hashProvider.CompareHash(user.password, userProps.password);

        if (!isPasswordValid)
            throw new AppError("Usuario ou senha invalidos", 400);

        var token = _tokenProvider.Generate(user);

        return token;
    }
}