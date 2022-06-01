using Shared.AttributeValidators;
using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class PaymentRequest
    {
        [Required]
        public string CardNumber { get; set; }
        [Required]
        public string Cvv { get; set; }
        [Required]
        [IntRange(Minimum = 2022,Maximum = 9999)]
        public int ExpiryYear { get; set; }
        [Required]
        [IntRange(Minimum = 1, Maximum = 12)]
        public int ExpiryMonth { get; set; }
        [Required]
        public Currency Currency { get; set; }

        [Required]
        public decimal Amount { get; set; }

    }
    public class PaymentModel : PaymentRequest
    {
        public string MerchantBankAccount { get; set; }
    }

        public class PaymentResponse
        {
           public string Reference { get; set; }
           public string MetaData { get; set; }
           public string Status { get; set; }
        }
}
