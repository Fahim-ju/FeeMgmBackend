using AutoMapper;
using FeeMgmBackend.Dtos;
using FeeMgmBackend.Entity;
using Microsoft.EntityFrameworkCore;

namespace FeeMgmBackend.Services;

public class PaymentService : IPaymentService
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public PaymentService(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<PaymentDto>> IndexAsync()
    {
        var payments = await _context.Payments.ToListAsync();
        var members = await _context.Members.ToListAsync();

        var result = new List<PaymentDto>();

        foreach (var pay in payments)
        {
            var paymentDto = _mapper.Map<PaymentDto>(pay);
            paymentDto.UserName = members.Find(x => x.Id == pay.MemberId).Name;
            result.Add(paymentDto);
        }
        return result;
    }

    public async Task<Payment> AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
        return payment;
    }

}