using FeeMgmBackend.Dtos;
using FeeMgmBackend.Entity;

namespace FeeMgmBackend.Services;

public interface IPaymentService
{
    Task<List<PaymentDto>> IndexAsync();
    Task<Payment> AddAsync(Payment payment);
}