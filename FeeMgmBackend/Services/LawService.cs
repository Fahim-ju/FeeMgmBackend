using FeeMgmBackend.Entity;
using Microsoft.EntityFrameworkCore;

namespace FeeMgmBackend.Services;

public class LawService : ILawService
{
    private readonly DatabaseContext _context;
    public LawService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<List<Law>> IndexAsync()
    {
        return await _context.Laws.Where(x => !x.IsDeleted).ToListAsync();
    }

    public async Task<Law> AddAsync(Law law)
    {
        await _context.Laws.AddAsync(law);
        await _context.SaveChangesAsync();
        return law;
    }

    public async Task UpdateAsync(Law law)
    {
        _context.Laws.Update(law);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var law = await _context.Laws.FirstOrDefaultAsync(x => x.Id == id);
        law.IsDeleted = true;

        law.DeletedAt = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }
}