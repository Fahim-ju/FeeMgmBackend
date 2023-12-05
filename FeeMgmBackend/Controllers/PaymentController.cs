using AutoMapper;
using FeeMgmBackend.Dtos;
using FeeMgmBackend.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FeeMgmBackend.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public PaymentController(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("GetPayments")]
        public async Task<IActionResult> GetPayments()
        {
            var payments = await _context.Payments.ToListAsync();
            var users = await _context.Users.ToListAsync();
            List<PaymentDto> result = new List<PaymentDto>();
            foreach (var pay in payments)
            {
                var paymentDto = _mapper.Map<PaymentDto>(pay);
                paymentDto.UserName = users.Find(user => user.Id == pay.UserId).Name;
                result.Add(paymentDto);
            }
            return Ok(result);
        }

        [HttpPost("AddPayment")]
        public async Task< IActionResult> AddPayment(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
            return Ok(payment);
        }
    }
}
