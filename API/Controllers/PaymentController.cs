using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Implementation;
using Shared.Models;
using WebAPI.Helpers;

namespace WebAPI.Controllers
{
    [ApiKey]
    public class PaymentsController : BaseController
    {
        readonly IPayment _payment; 

        public PaymentsController(IPayment payment)
        {
            _payment = payment;
        }

        [HttpGet]
        public  IActionResult Get([FromQuery] TransactionStatus? status, [FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery]Currency? currency)
        {
            return ApiResult(_payment.GetTransactions(ApiKey,from,to,status,currency));
        }

        [HttpPost("[action]")]
        public IActionResult Initiate(PaymentRequest request)
        {
            return ApiResult(_payment.IntiatePayment(ApiKey, request));
        }

        [HttpPost("[action]")]
        public IActionResult Otp(OtpRequest request)
        {
            return ApiResult(_payment.ProcessOtp(ApiKey, request));
        }
    }
}
