using SmartStock_AI.Domain.Authentication.Entities;

namespace SmartStock_AI.Application.Authentication.Interfaces;

public interface INegocioLoginTrackingRepository
{
    Task<NegocioLoginTracking?> GetByCorreoAsync(string correo);
    Task AddAsync(NegocioLoginTracking tracking);
    Task SaveChangesAsync();
}