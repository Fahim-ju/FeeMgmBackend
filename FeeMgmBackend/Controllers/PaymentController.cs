using FeeMgmBackend.Entity;
using FeeMgmBackend.Services;
using Microsoft.AspNetCore.Mvc;

namespace FeeMgmBackend.Controllers;

[Route("[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet("GetPayments")]
    public async Task<IActionResult> IndexAsync()
    {
        try
        {
            var payments = await _paymentService.IndexAsync();
            return Ok(payments);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost("AddPayment")]
    public async Task<IActionResult> AddAsync(Payment payment)
    {
        try
        {
            var result = await _paymentService.AddAsync(payment);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}