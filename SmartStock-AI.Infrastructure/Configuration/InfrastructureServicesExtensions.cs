using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartStock_AI.Application.Authentication.Interfaces;
using SmartStock_AI.Application.UnitOfWork;
using SmartStock_AI.Infrastructure.Authentication.Repositories;
using SmartStock_AI.Infrastructure.Authentication.Services;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio;
using SmartStock_AI.Infrastructure.UnitOfWork;

namespace SmartStock_AI.Infrastructure.Configuration;

public static class InfrastructureServicesExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // 🔹 AdminDbContext: para la base de administración
        services.AddDbContext<AdminDbContext>((serviceProvider, options) =>
        {
            var connectionString = configuration.GetConnectionString("AdminConnection");
            options.UseNpgsql(connectionString);
        });

        // 🔹 NegocioDbContext: para la base de negocio (o plantilla)
        services.AddDbContext<NegocioDbContext>((serviceProvider, options) =>
        {
            var connectionString = configuration.GetConnectionString("NegocioTemplateConnection");
            options.UseNpgsql(connectionString);
        });

        // 🔹 Aquí luego agregarás:
        // - Repositorios (AddScoped<IProductRepository, ProductRepository>()...)
        // - UnitOfWork
        // - AutoMapper
        // - MediatR
        // - etc.
        services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();
        services.AddScoped<INegocioLoginTrackingRepository, NegocioLoginTrackingRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<DatabaseCloneService>();

        return services;
    }
}