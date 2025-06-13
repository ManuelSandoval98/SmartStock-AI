using Microsoft.EntityFrameworkCore;
using SmartStock_AI.Application.Authentication.Interfaces;
using SmartStock_AI.Domain.Authentication.Entities;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin;

namespace SmartStock_AI.Infrastructure.Authentication.Repositories;

public class NegocioRepository : INegocioRepository
{
    private readonly AdminDbContext _dbContext;

    public NegocioRepository(AdminDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Negocio> GetByCorreoAsync(string correo)
    {
        return await _dbContext.Negocios
            .FirstOrDefaultAsync(n => n.CorreoAdmin == correo);

    }
    
    public async Task<Negocio> GetByCorreoSinTrackingAsync(string correo)
    {
        return await _dbContext.Negocios
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.CorreoAdmin == correo);
    }

    public async Task<Negocio> GetByNombreComercialAsync(string nombreComercial)
    {
        return await _dbContext.Negocios
            .FirstOrDefaultAsync(n => n.NombreComercial.ToLower() == nombreComercial.ToLower());
    }

    public async Task AddAsync(Negocio negocio)
    {
        await _dbContext.Negocios.AddAsync(negocio);
    }
}