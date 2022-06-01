using Entities;
using Shared.Models;

namespace Services.Implementation
{
    public interface IPayment
    {
        Result<List<TransactionResponse>> GetTransactions(string ApiKey, DateTime? from, DateTime? to, TransactionStatus? status, Currency? currency);
        Result<PaymentResponse> IntiatePayment(string ApiKey, PaymentRequest model);
        Result<PaymentResponse> ProcessOtp(string ApiKey, OtpRequest request);
    }
}