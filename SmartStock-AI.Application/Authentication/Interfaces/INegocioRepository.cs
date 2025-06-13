using SmartStock_AI.Domain.Authentication.Entities;

namespace SmartStock_AI.Application.Authentication.Interfaces;

public interface INegocioRepository
{
    Task<Negocio> GetByCorreoAsync(string correo);
    Task<Negocio> GetByCorreoSinTrackingAsync(string correo);
    
    Task<Negocio> GetByNombreComercialAsync(string nombreComercial);
    Task AddAsync(Negocio negocio);
}