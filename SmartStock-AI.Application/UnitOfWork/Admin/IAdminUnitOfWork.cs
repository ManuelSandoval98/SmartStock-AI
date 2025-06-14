using SmartStock_AI.Application.Authentication.Interfaces;

namespace SmartStock_AI.Application.UnitOfWork.Admin;

public interface IAdminUnitOfWork
{
    INegocioRepository NegocioRepository { get; }
    INegocioLoginTrackingRepository NegocioLoginTrackingRepository { get; }
    Task SaveChangesAsync();
}