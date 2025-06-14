using Microsoft.EntityFrameworkCore;
using SmartStock_AI.Application.Common.Interfaces;
using SmartStock_AI.Application.Products.DTOs;
using SmartStock_AI.Application.Products.Interfaces;
using SmartStock_AI.Domain.Categories.Entities;
using SmartStock_AI.Domain.Products.Entities;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio;

namespace SmartStock_AI.Infrastructure.Products.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly INegocioDbContext _context;

    public ProductRepository(INegocioDbContext context)
    {
        _context = context;
    }
    public async Task<List<Producto>> GetAllWithCategoryAsync()
    {
        var productos = await _context.Productos
            .Include(p => p.Categoria) // 🔥 Join automático
            .ToListAsync();
        return productos;
    }
    
    public async Task AddAsync(Producto producto)
    {
        await _context.Productos.AddAsync(producto);
    }
    
    public async Task<Producto?> GetByIdAsync(int id)
    {
        return await _context.Productos.FindAsync(id);
    }

    public void Delete(Producto producto)
    {
        _context.Productos.Remove(producto);
    }
}