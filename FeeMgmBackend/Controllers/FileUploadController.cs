using Microsoft.AspNetCore.Mvc;
using FeeMgmBackend.Services;
using Microsoft.EntityFrameworkCore;
using FeeMgmBackend.Dtos;
using FeeMgmBackend.Entity;

namespace FeeMgmBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilesController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public FilesController(DatabaseContext dbContext)
        {
            _context = dbContext;
        }
        [HttpPost("FineFileUpload")]
        public async Task<IActionResult> AnalyzeFineExcelFile(IFormFile file)
        {
            var streamFile = file.OpenReadStream();
            var excelService = new ExcelService();
            var records = excelService.ParseExcel(streamFile);
            var rows = records.Count;
            var col = rows > 0 ? records[0].Count : 0;
            for (int i = 1; i < rows; i++)
            {
                string userName = "", lawName = "";
                decimal amount = 0;
                DateTime date;

                for (int j = 0; j < col; j++)
                {
                    switch (j)
                    {
                        case 0:
                            userName = records[i][j].ToString();
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
                        case 2:
                            lawName = records[i][j].ToString();
                            break;
                        case 3:
                            Console.WriteLine("Implement paid - payment status");
                            break;
                        case 4:
                            date = records[i][j].ToString().Length > 0 ? DateTime.Parse(records[i][j].ToString()) : DateTime.Now;
                            break;

                    }
                }
                Member member = await _context.Members.FirstOrDefaultAsync(u => u.Name == userName);

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
                Law law = await _context.Laws.FirstOrDefaultAsync(l => l.Name == lawName);
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
                Fine fine = new Fine();
                var existingLaw = await _context.Laws.FirstOrDefaultAsync(l => l.Name == lawName);
                var existingMember = await _context.Members.FirstOrDefaultAsync(u => u.Name == userName);
                fine.LawId = existingLaw.Id;
                fine.MemberId = existingMember.Id;
                await _context.Fines.AddAsync(fine);
                await _context.SaveChangesAsync();
            }
            return Ok(records);
        }

        [HttpPost("PaymentFileUpload")]
        public async Task<IActionResult> AnalyzePaymentExcelFile(IFormFile file)
        {
            var streamFile = file.OpenReadStream();
            var excelService = new ExcelService();
            var records = excelService.ParseExcel(streamFile);
            var rows = records.Count;
            var cols = rows > 0 ? records[0].Count : 0;
            for (int i = 1; i < rows; i++)
            {
                string memberName = "";
                decimal amount = 0;

                for (int j = 0; j < cols; j++)
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
                Member user = await _context.Members.FirstOrDefaultAsync(u => u.Name == memberName);
                Payment payment = new Payment();
                payment.Amount = amount;
                payment.MemberId = user.Id;
                await _context.AddAsync(payment);
                await _context.SaveChangesAsync();
            }
            return Ok(records);
        }
    }
}
