using System.Globalization;
using FeeMgmBackend.Entity;
using Microsoft.EntityFrameworkCore;

namespace FeeMgmBackend.Services;

public class FileUploadService : IFileUploadService
{
    private readonly DatabaseContext _context;
    public FileUploadService(DatabaseContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<List<List<object>>> AnalyzeFineExcelFileAsync(Stream stream)
    {

        var excelService = new ExcelService();
        var records = excelService.ParseExcel(stream);

        var rows = records.Count;
        var col = rows > 0 ? records[0].Count : 0;
        for (var i = 1; i < rows; i++)
        {
            string userName = "", lawName = "";
            decimal amount = 0;
            DateTime fineDate;

            if (DateTime.TryParseExact(records[i][4]?.ToString(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedDate))
            {
                fineDate = DateTime.SpecifyKind(parsedDate, DateTimeKind.Utc);
            }
            else
            {
                fineDate = DateTime.UtcNow;
            }

            for (var j = 0; j < col; j++)
            {
                switch (j)
                {
                    case 0:
                        userName = records[i][j].ToString();
                        break;
                    case 1:
                        try
                        {
                            if (records[i][j].ToString()!.Length > 0)
                                amount = decimal.Parse(records[i][j].ToString() ?? decimal.Zero.ToString(CultureInfo.CurrentCulture));
                            else amount = 0;
                        }
                        catch (FormatException ex)
                        {
                            throw new Exception(ex.Message);
                        }

                        break;
                    case 2:
                        lawName = records[i][j].ToString();
                        break;
                    case 3:
                        Console.WriteLine("Implement paid - payment status");
                        break;
                    case 4:
                        break;

                }
            }

            if (userName != null && (amount == 0 || userName.Length == 0)) continue;

            var existingFine = await _context.Fines
                .Include(f => f.Law)
                .Include(f => f.Member)
                .FirstOrDefaultAsync(f =>
                    f.Member.Name == userName &&
                    f.Law.Name == lawName &&
                    f.FineDate == fineDate &&
                    f.amount == amount);

            if (existingFine != null)
            {
                existingFine.amount = amount;
                await _context.SaveChangesAsync();
            }

            else
            {
                var member = await _context.Members.FirstOrDefaultAsync(u => u.Name == userName);
                var law = await _context.Laws.FirstOrDefaultAsync(l => l.Name == lawName);

                if (member == null)
                {
                    member = new Member();
                    member.Name = userName;
                    await _context.Members.AddAsync(member);
                    await _context.SaveChangesAsync();
                }
                else if (member.IsDeactivated)
                {
                    member.IsDeactivated = false;
                    _context.Members.Update(member);
                }

                if (law == null)
                {
                    law = new Law();
                    law.Name = lawName;
                    law.Amount = amount;
                    law.IsDeleted = false;
                    await _context.Laws.AddAsync(law);
                    await _context.SaveChangesAsync();
                }

                else if (law.IsDeleted)
                {
                    law.IsDeleted = false;
                    _context.Laws.Update(law);
                }

                var newFine = new Fine
                {
                    LawId = law.Id,
                    MemberId = member.Id,
                    amount = amount,
                    FineDate = fineDate
                };

                await _context.Fines.AddAsync(newFine);
                await _context.SaveChangesAsync();
            }
        }

        return records;
    }

    public async Task<List<List<object>>> AnalyzePaymentExcelFileAsync(Stream stream)
    {
        var excelService = new ExcelService();
        var records = excelService.ParseExcel(stream);
        var rows = records.Count;
        var cols = rows > 0 ? records[0].Count : 0;
        for (var i = 1; i < rows; i++)
        {
            var memberName = "";
            decimal amount = 0;

            for (var j = 0; j < cols; j++)
            {
                switch (j)
                {
                    case 0:
                        memberName = records[i][j].ToString();
                        break;
                    case 1:
                        try
                        {
                            if (records[i][j].ToString().Length > 0)
                                amount = decimal.Parse(records[i][j].ToString());
                            else amount = 0;
                        }
                        catch (FormatException ex)
                        {
                            throw new Exception(ex.Message);
                        }

                        break;
                }
            }

            var user = await _context.Members.FirstOrDefaultAsync(u => u.Name == memberName);
            var payment = new Payment();
            payment.Amount = amount;
            payment.MemberId = user.Id;
            await _context.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        return records;
    }
}