using SmartStock_AI.Application.Categories.Interfaces;
using SmartStock_AI.Application.Products.Interfaces;

namespace SmartStock_AI.Application.UnitOfWork.Negocio;

public interface INegocioUnitOfWork
{
    ICategoryRepository CategoryRepository { get; }
    IProductRepository ProductRepository { get; }

    Task SaveChangesAsync();
}