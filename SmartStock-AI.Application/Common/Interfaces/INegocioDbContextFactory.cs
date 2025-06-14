namespace SmartStock_AI.Application.Common.Interfaces;

public interface INegocioDbContextFactory
{
    INegocioDbContext CreateDbContext(string connectionString);
}