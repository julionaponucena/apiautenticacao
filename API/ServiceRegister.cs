using Infra.Context;
using Service.Interfaces;
using Service.Services;
using Service.Providers;
using Infra.Repositories.Interfaces;
using Infra.Repositories;

namespace API;

public static class ServiceRegister
{

    public static void RegisterDatabase(this IServiceCollection services)
    {
        services.AddDbContext<DatabaseContext>();
    }

    public static void DependencyInjection(this IServiceCollection services)
    {
        services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
        services.AddScoped(typeof(IHashProvider), typeof(HashProvider));
        services.AddScoped(typeof(ITokenProvider), typeof(TokenProvider));

        services.AddTransient<IUserServiceCollection, UserServiceCollection>();
    }

}