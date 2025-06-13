using SmartStock_AI.Application.Products.DTOs;
using SmartStock_AI.Domain.Products.Entities;

namespace SmartStock_AI.Application.Products.Interfaces;

public interface IProductRepository
{
    Task<List<Producto>> GetAllWithCategoryAsync();
    Task AddAsync(Producto producto);
    Task<Producto?> GetByIdAsync(int id);
    void Delete(Producto producto);

}