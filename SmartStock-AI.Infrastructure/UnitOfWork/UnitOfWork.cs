using SmartStock_AI.Application.Authentication.Interfaces;
using SmartStock_AI.Application.Categories.Interfaces;
using SmartStock_AI.Application.Products.Interfaces;
using SmartStock_AI.Application.UnitOfWork;
using SmartStock_AI.Infrastructure.Authentication.Repositories;
using SmartStock_AI.Infrastructure.Categories.Repositories;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Admin;
using SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio;
using SmartStock_AI.Infrastructure.Products.Repositories;

namespace SmartStock_AI.Infrastructure.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    public AdminDbContext AdminDbContext { get; }
    public NegocioDbContext NegocioDbContext { get; }
    public ICategoryRepository CategoryRepository { get; }
    public INegocioRepository NegocioRepository { get; }
    public IProductRepository ProductRepository { get; }

    public UnitOfWork(AdminDbContext adminDbContext, NegocioDbContext negocioDbContext)
    {
        AdminDbContext = adminDbContext;
        NegocioDbContext = negocioDbContext;
        CategoryRepository = new CategoryRepository(negocioDbContext);
        NegocioRepository = new NegocioRepository(adminDbContext);
        ProductRepository = new ProductRepository(negocioDbContext);
    }

    public async Task SaveChangesAsync()
    {
        await AdminDbContext.SaveChangesAsync();
        await NegocioDbContext.SaveChangesAsync();
    }
}