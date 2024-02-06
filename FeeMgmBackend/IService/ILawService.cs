using FeeMgmBackend.Entity;

namespace FeeMgmBackend.Services;

public interface ILawService
{
    Task<List<Law>> IndexAsync();
    Task<Law> AddAsync(Law law);
    Task UpdateAsync(Law law);
    Task DeleteAsync(Guid id);
}