using RateLimitMinimalApi.Core.App.Services;
using RateLimitMinimalApi.Core.Domain.Interfaces.Repos;
using RateLimitMinimalApi.Infra.Repos;

namespace RateLimitMinimalApi.Api.Configs;

public static class DependencyInjectionConfig
{
    public static IServiceCollection ConfigureDependencies(this IServiceCollection services)
    {
        // Application Services
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, UserService>();

        // Infrastructure
        services.AddSingleton<IProductRepo, InMemoryProductRepo>();
        services.AddSingleton<IUserRepo, InMemoryUserRepo>();

        return services;
    }
}