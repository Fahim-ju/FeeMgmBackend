using AutoMapper;
using FeeMgmBackend.Dtos;
using FeeMgmBackend.Entity;
using FeeMgmBackend.IService;
using Microsoft.EntityFrameworkCore;

namespace FeeMgmBackend.Services;

public class FineService : IFineService
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;
    public FineService(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<FineDto>> GetFinesAsync()
    {
        var fines = await _context.Fines.ToListAsync();
        var members = await _context.Members.ToListAsync();
        var laws = await _context.Laws.ToListAsync();


        var fineList = new List<FineDto>();

        foreach (var fine in fines)
        {
            var fn = _mapper.Map<FineDto>(fine);

            fn.UserName = members.Find(member => member.Id == fine.MemberId).Name;
            var law = laws.Find(law => law.Id == fine.LawId);
            fn.LawName = law.Name;
            fineList.Add(fn);
        }

        return fineList;
    }

    public async Task AddAsync(Fine fine)
    {
        await _context.Fines.AddAsync(fine);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var existingFine = await _context.Fines.FirstOrDefaultAsync(x => x.Id == id);
        existingFine.IsDeleted = true;

        _context.Fines.Update(existingFine);
        await _context.SaveChangesAsync();
    }
}