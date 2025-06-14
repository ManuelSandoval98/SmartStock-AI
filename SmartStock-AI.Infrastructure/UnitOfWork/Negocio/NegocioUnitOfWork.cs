using Microsoft.EntityFrameworkCore;
using SmartStock_AI.Application.Categories.Interfaces;
using SmartStock_AI.Application.Common.Interfaces;
using SmartStock_AI.Application.Products.Interfaces;
using SmartStock_AI.Application.UnitOfWork.Negocio;
using SmartStock_AI.Infrastructure.Categories.Repositories;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio;
using SmartStock_AI.Infrastructure.Products.Repositories;
using SmartStock_AI.Infrastructure.UnitOfWork.Admin;

namespace SmartStock_AI.Infrastructure.UnitOfWork.Negocio;

public class NegocioUnitOfWork : INegocioUnitOfWork
{
    private readonly INegocioDbContext _context;

    public ICategoryRepository CategoryRepository { get; }
    public IProductRepository ProductRepository { get; }

    public NegocioUnitOfWork(INegocioDbContext context)
    {
        _context = context;
        CategoryRepository = new CategoryRepository(_context);
        ProductRepository = new ProductRepository(_context);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}