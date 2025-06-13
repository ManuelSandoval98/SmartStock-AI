using Microsoft.EntityFrameworkCore;
using SmartStock_AI.Application.Authentication.Interfaces;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin.Entities;

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
        return await _context.NegocioLoginTracking
            .FirstOrDefaultAsync(t => t.CorreoAdmin == correo);
    }

    public async Task AddAsync(NegocioLoginTracking tracking)
    {
        await _context.NegocioLoginTracking.AddAsync(tracking);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}