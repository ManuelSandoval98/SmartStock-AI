using SmartStock_AI.Application.Authentication.Interfaces;
using SmartStock_AI.Application.UnitOfWork.Admin;
using SmartStock_AI.Infrastructure.Authentication.Repositories;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin;

namespace SmartStock_AI.Infrastructure.UnitOfWork.Admin;

public class AdminUnitOfWork : IAdminUnitOfWork
{
    private readonly AdminDbContext _context;
    public INegocioRepository NegocioRepository { get; }
    public INegocioLoginTrackingRepository NegocioLoginTrackingRepository { get; }
    
    public AdminUnitOfWork(AdminDbContext context)
    {
        _context = context;
        NegocioRepository = new NegocioRepository(_context);
        NegocioLoginTrackingRepository = new NegocioLoginTrackingRepository(_context);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}