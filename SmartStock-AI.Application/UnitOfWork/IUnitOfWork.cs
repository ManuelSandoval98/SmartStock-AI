using SmartStock_AI.Application.Authentication.Interfaces;
using SmartStock_AI.Application.Categories.Interfaces;
using SmartStock_AI.Application.Products.Interfaces;

namespace SmartStock_AI.Application.UnitOfWork;

public interface IUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }
    INegocioRepository NegocioRepository { get; }
    IProductRepository ProductRepository { get; }
    Task SaveChangesAsync();
}