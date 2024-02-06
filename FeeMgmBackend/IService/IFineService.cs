using FeeMgmBackend.Dtos;
using FeeMgmBackend.Entity;

namespace FeeMgmBackend.IService;

public interface IFineService
{
    Task<List<FineDto>> GetFinesAsync();
    Task AddAsync(Fine fine);
    Task DeleteAsync(Guid id);
}