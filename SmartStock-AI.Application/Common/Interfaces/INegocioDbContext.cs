using Microsoft.EntityFrameworkCore;
using SmartStock_AI.Domain.Categories.Entities;
using SmartStock_AI.Domain.Products.Entities;

namespace SmartStock_AI.Application.Common.Interfaces;

public interface INegocioDbContext
{
    DbSet<Producto> Productos { get; }
    DbSet<Categoria> Categorias { get; }
    IQueryable<T> Get<T>() where T : class;
    Task AddAsync<T>(T entity) where T : class;

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}