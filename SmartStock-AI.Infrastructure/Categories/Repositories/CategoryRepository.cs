using Microsoft.EntityFrameworkCore;
using SmartStock_AI.Application.Categories.Interfaces;
using SmartStock_AI.Application.UnitOfWork;
using SmartStock_AI.Domain.Categories.Entities;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio;
using SmartStock_AI.Infrastructure.UnitOfWork;

namespace SmartStock_AI.Infrastructure.Categories.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly NegocioDbContext _dbContext;

    public CategoryRepository(NegocioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Categoria>> GetAllAsync()
    {
        return await _dbContext.Categorias.ToListAsync();
    }
    public async Task<Categoria> GetByIdAsync(int id)
    {
        return await _dbContext.Categorias.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Categoria categoria)
    {
        await _dbContext.Categorias.AddAsync(categoria);
    }

    public void Update(Categoria categoria)
    {
        _dbContext.Categorias.Update(categoria);
    }

    public void Delete(Categoria categoria)
    {
        _dbContext.Categorias.Remove(categoria);
    }
}