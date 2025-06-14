using Microsoft.EntityFrameworkCore;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin;
using SmartStock_AI.Application.Authentication.Interfaces;
using SmartStock_AI.Domain.Authentication.Entities;

namespace SmartStock_AI.Infrastructure.Authentication.Repositories;

public class NegocioLoginTrackingRepository : INegocioLoginTrackingRepository
{
    private readonly AdminDbContext _context;

    public NegocioLoginTrackingRepository(AdminDbContext context)
    {
        _context = context;
    }

    public async Task<NegocioLoginTracking?> GetByCorreoAsync(string correo)
    {
        var entity = await _context.NegocioLoginTracking
            .FirstOrDefaultAsync(t => t.CorreoAdmin == correo);

        if (entity == null) return null;

        return new NegocioLoginTracking
        {
            Id = entity.Id,
            CorreoAdmin = entity.CorreoAdmin,
            IntentosFallidos = entity.IntentosFallidos,
            BloqueadoHasta = entity.BloqueadoHasta
        };
    }

    public async Task AddAsync(NegocioLoginTracking tracking)
    {
        var entity = new NegocioLoginTracking
        {
            CorreoAdmin = tracking.CorreoAdmin,
            IntentosFallidos = tracking.IntentosFallidos,
            BloqueadoHasta = tracking.BloqueadoHasta
        };

        await _context.NegocioLoginTracking.AddAsync(entity);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}