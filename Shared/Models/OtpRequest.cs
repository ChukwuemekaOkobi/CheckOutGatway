using System.ComponentModel.DataAnnotations;

namespace Shared.Models
{
    public class OtpRequest
    {
        [Required]
        public string Reference { get; set; }
        [Required]
        public string Otp { get; set; }
    }
}
