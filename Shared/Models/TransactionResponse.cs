namespace Shared.Models
{
    public class TransactionResponse
    {
        public string Reference { get; set; }

        public DateTime Time { get; set; }
        public decimal Amount { get; set; }
        public string  Currency { get; set; }

        public string Status { get; set; }
        public string StatusCode { get; set; }

        public string Comment { get; set; }

        public string CardNumber { get; set; }

        public string Cvv { get; set; }
        public string CardExpiry { get; set; }
    }
}
