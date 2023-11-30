using FeeMgmBackend.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeeMgmBackend.Controllers
{
    [Route("controller")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PaymentController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("GetPayments")]
        public async Task<IActionResult> GetPayments()
        {
            var payments = await _context.Payments.ToListAsync();
            return Ok(payments);
        }

        [HttpPost("AddPayment")]
        public async Task< IActionResult> AddPayment(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            var user = _context.Users.FirstOrDefaultAsync(x => x.Id == payment.UserId).Result;
            if(user != null)
            {
                user.Due -= payment.Amount;
                user.Paid += payment.Amount;
                _context.Users.Update(user);
            }
            await _context.SaveChangesAsync();
            return Ok(payment);
        }
    }
}
