using Microsoft.EntityFrameworkCore;
using SmartStock_AI.Application.Common.Interfaces;
using SmartStock_AI.Domain.Categories.Entities;
using SmartStock_AI.Domain.Products.Entities;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio;

public class EfNegocioDbContextAdapter : INegocioDbContext
{
    private readonly NegocioDbContext _context;

    public EfNegocioDbContextAdapter(NegocioDbContext context)
    {
        _context = context;
    }
    public DbSet<Producto> Productos => _context.Productos;
    public DbSet<Categoria> Categorias => _context.Categorias;

    public IQueryable<T> Get<T>() where T : class
        => _context.Set<T>().AsQueryable();

    public async Task AddAsync<T>(T entity) where T : class
        => await _context.Set<T>().AddAsync(entity);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);
}