

using Shared.Models;

namespace BankSimulator.Models
{
    /// <summary>
    /// Bank Transaction Model
    /// </summary>
    public class Transaction
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public int AccountId { get; set; }

        public TransactionStatus Status { get; set; }

        public DateTime StartTime { get; set; }
        public int ToAccount { get; set; }

        public Transaction(Guid? id, decimal amt, int accountId, TransactionStatus status, int ToAccountId)
        {
            Id = id == null ? Guid.NewGuid() : id.Value;
            StartTime = DateTime.Now;
            Amount = amt;
            AccountId = accountId;
            Status = status;
            ToAccount = ToAccountId;

        }
    }

}
