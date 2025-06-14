using Microsoft.EntityFrameworkCore;
using SmartStock_AI.Application.Common.Interfaces;

namespace SmartStock_AI.Infrastructure.Infrastructure.Persistence.Negocio;

public class NegocioDbContextFactory : INegocioDbContextFactory
{
    public INegocioDbContext CreateDbContext(string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<NegocioDbContext>();
        optionsBuilder.UseNpgsql(connectionString);

        var dbContext = new NegocioDbContext(optionsBuilder.Options);
        return new EfNegocioDbContextAdapter(dbContext);
    }
}