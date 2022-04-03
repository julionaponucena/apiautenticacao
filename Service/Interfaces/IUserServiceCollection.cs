using Model;
using Service.Validations;

namespace Service.Interfaces;

public interface IUserServiceCollection
{
    // Registro do Usuario
    User SignUp(ValidateUserProps props);

    // Login do Usuario
    string SignIn(ValidateUserSignIn props);

    // Encontrar usuario pelo Username
    User? FindUserByUsername(string username);
}