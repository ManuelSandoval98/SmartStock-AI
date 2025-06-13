using SmartStock_AI.Domain.Categories.Entities;

namespace SmartStock_AI.Application.Categories.Interfaces;

public interface ICategoryRepository
{
    Task<List<Categoria>> GetAllAsync();
    Task<Categoria> GetByIdAsync(int id);
    Task AddAsync(Categoria categoria);
    void Update(Categoria categoria);
    void Delete(Categoria categoria);
}