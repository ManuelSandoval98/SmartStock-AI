using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartStock_AI.Application.Authentication.Interfaces;
using SmartStock_AI.Application.Common.Interfaces;
using SmartStock_AI.Application.UnitOfWork;
using SmartStock_AI.Application.UnitOfWork.Admin;
using SmartStock_AI.Application.UnitOfWork.Negocio;
using SmartStock_AI.Infrastructure.Authentication.Repositories;
using SmartStock_AI.Infrastructure.Authentication.Services;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio;
using SmartStock_AI.Infrastructure.UnitOfWork;
using SmartStock_AI.Infrastructure.UnitOfWork.Admin;
using SmartStock_AI.Infrastructure.UnitOfWork.Negocio;

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
        
        services.AddScoped<INegocioLoginTrackingRepository, NegocioLoginTrackingRepository>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<DatabaseCloneService>();
        services.AddScoped<INegocioDbContextFactory, NegocioDbContextFactory>();
        services.AddScoped<INegocioUnitOfWork>(provider =>
        {
            var accessor = provider.GetRequiredService<IHttpContextAccessor>();
            var factory = provider.GetRequiredService<INegocioDbContextFactory>();
            var httpContext = accessor.HttpContext;

            if (httpContext?.User?.Identity?.IsAuthenticated != true)
                throw new Exception("No autenticado");

            var dbName = httpContext.User.FindFirst("DbAsociada")?.Value;
            if (string.IsNullOrEmpty(dbName))
                throw new Exception("No se encontró el nombre de la base de datos del negocio.");

            var connectionString = $"Host=localhost;Database={dbName};Username=robertoflores;Password=302630";
            var dbContext = factory.CreateDbContext(connectionString);

            return new NegocioUnitOfWork(dbContext);
        });


        services.AddScoped<IAdminUnitOfWork,AdminUnitOfWork>();

        return services;
    }
}