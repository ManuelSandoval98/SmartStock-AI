using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin.Entities;

namespace SmartStock_AI.Infrastructure.Authentication.Repositories;

public interface INegocioLoginTrackingRepository
{
    Task<NegocioLoginTracking?> GetByCorreoAsync(string correo);
    Task AddAsync(NegocioLoginTracking tracking);
    Task SaveChangesAsync();
}