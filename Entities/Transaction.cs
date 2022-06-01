using Shared.Models;

namespace Entities
{

    /// <summary>
    /// Transaction Model holding all transaction, failed, pending, successful
    /// </summary>
    public class Transaction
    {
        public int Id { get; set; }
        public string Reference { get; set; }
        public DateTime Created { get; set; }
        public decimal Amount { get; set; }
        public Currency Currency { get; set; }

        public TransactionStatus Status { get; set; }
        public string StatusCode { get; set; }
        public string MetaData { get; set; }

        public string CardNumber { get; set; }
        public string Cvv { get; set; }

        public string Expiry { get; set; }

        public string? BankReference { get; set; }

        public int MerchantId { get; set; }
        public Merchant Merchant { get; set; }


        public Transaction()
        {

        }
        public Transaction(int id, string reference, DateTime created, decimal amount, Currency currency, TransactionStatus status, string statusCode, string metaData, int merchantId, string cardNumber, string cvv, string expiry, string bankRef)
        {
            Id = id;
            Reference = reference;
            Created = created;
            Amount = amount;
            Currency = currency;
            Status = status;
            StatusCode = statusCode;
            MetaData = metaData;
            MerchantId = merchantId;
            CardNumber = cardNumber;
            Expiry = expiry;
            Cvv = cvv;
            BankReference = bankRef;
           // Merchant = merchant;
        }
    }



}
