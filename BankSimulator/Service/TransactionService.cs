using BankSimulator.Data;
using BankSimulator.Models;
using Shared.Models;

namespace BankSimulator.Service
{


    /// <summary>
    /// Bank Service to simulate Transactions 
    /// </summary>

    public class BankService
    {
        
        public static readonly Database Collection = new ();  


        /// <summary>
        /// initial payment transaction and validates payment
        /// </summary>
        /// <param name="model"></param>
        /// <returns> true with request for otp or false </returns>
        public static (bool, Guid?, string) InitiatePayment(PaymentModel model)
        { 
            if(string.IsNullOrWhiteSpace(model.CardNumber) || string.IsNullOrWhiteSpace(model.Cvv))
            {
                return (false,null, "Invalid Payment Details");
            }

            //find card and matching account 
            var account = Collection.Accounts.Find(a => a.Card != null && a.Card.Number == model.CardNumber &&
                                             a.Card.Cvv == model.Cvv && 
                                             a.Card.ExpiryMonth == model.ExpiryMonth && 
                                             a.Card.ExpiryYear == model.ExpiryYear);

            if(account is null)
            {
                return (false,null, "Invalid Card details"); 
            }

            var merchantAccont = Collection.Accounts.Find(a => a.Number == model.MerchantBankAccount);

            if(merchantAccont is null)
            {
                return (false, null, "Invalid Details");
            }

            // validate Card
            var card = account.Card;

            var currentDate = DateTime.Now;

            // validate Card Expired or Inactive 
            if (card.Status == CardStatus.Inactive || card.ExpiryYear < currentDate.Year || card.ExpiryMonth < currentDate.Month )
            {
                return (false,null, "Card declined");
            }

           // Check Account Balance 
            if(account.Balance < model.Amount)
            {
                return (false,null, "Insufficient funds");
            }

            //Add new Transaction with pending status
            var newTransaction = new Transaction(null,model.Amount, account.Id, TransactionStatus.pending, merchantAccont.Id);

            Collection.Transactions.Add(newTransaction);

            // Approved payment otp has been sent 
            SendMailWithOTP(account.Email, account.Name, account.OTP); 

            return (true,newTransaction.Id,"payment initiated, awaiting otp"); 
        }


        /// <summary>
        /// Completed transaction with correct OTP
        /// </summary>
        /// <param name="transactionId"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        public static (bool, string) ProcessOtp (Guid transactionId, string otp)
        {
            //get Account of transaction 
            var transaction = Collection.Transactions.Find(t => t.Id == transactionId);
            
            if(transaction == null)
            {
                return (false, "Invalid request"); 
            }

            // get Account 

            var fromAccount = Collection.Accounts.Find(a => a.Id == transaction.AccountId);

            if(fromAccount == null)
            {
                transaction.Status = TransactionStatus.failed; 

                return (false, "Invalid request");
            }

            var toAccount = Collection.Accounts.Find(a => a.Id == transaction.ToAccount); 

            if(toAccount == null)
            {
                transaction.Status = TransactionStatus.failed;

                return (false, "Invalid request");
            }

            // Validate Otp 
            if (fromAccount.OTP != otp.Trim())
            {
                transaction.Status = TransactionStatus.failed;
                return (false, "transaction declined"); 
            }

            // check balance
            if(fromAccount.Balance < transaction.Amount)
            {
                transaction.Status = TransactionStatus.failed;
                return (false, "Insufficient funds");
            }

            //debit  clients
            fromAccount.Balance -= transaction.Amount;

            //credit merchant
            toAccount.Balance += transaction.Amount; 

            //update transaction as successful
            transaction.Status = TransactionStatus.successful;

            //send a payment notification to account holder
            SendMailPaymentConfirmation(fromAccount.Email, fromAccount.Name, transaction.Amount);

            return (true, "Successful");
 
        }

        private static void SendMailWithOTP(string mail, string Name, string Otp)
        {
            // Sends Mail to Account holder
        }

        private static void SendMailPaymentConfirmation(string mail, string name, decimal Amount)
        {
            //send Mail to Account holder 
        }


        

    }


}
