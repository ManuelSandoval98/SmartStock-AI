using Microsoft.EntityFrameworkCore;
using SmartStock_AI.Application.Categories.Interfaces;
using SmartStock_AI.Application.Common.Interfaces;
using SmartStock_AI.Application.UnitOfWork;
using SmartStock_AI.Domain.Categories.Entities;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio;
using SmartStock_AI.Infrastructure.UnitOfWork;

namespace SmartStock_AI.Infrastructure.Categories.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly INegocioDbContext _context;

    public CategoryRepository(INegocioDbContext context)
    {
        _context = context;
    }

    public async Task<List<Categoria>> GetAllAsync()
    {
        return await _context.Categorias.ToListAsync();
    }
    public async Task<Categoria> GetByIdAsync(int id)
    {
        return await _context.Categorias.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Categoria categoria)
    {
        await _context.Categorias.AddAsync(categoria);
    }

    public void Update(Categoria categoria)
    {
        _context.Categorias.Update(categoria);
    }

    public void Delete(Categoria categoria)
    {
        _context.Categorias.Remove(categoria);
    }
}