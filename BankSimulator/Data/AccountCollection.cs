using BankSimulator.Models;
using Shared.Models;

namespace BankSimulator.Data
{
    /// <summary>
    /// Contains a Collection of test account details, transaction to simulate the bank functions
    /// </summary>
    public  class Database
    {

        public   List<Account> Accounts { get; }
        public   List<Transaction> Transactions { get; }

        public Database ()
        {
            Accounts = GetAccounts();

            Transactions = GetTransactions();

        }

       private static List<Account> GetAccounts() 
       {
            var cards = GetCards(); 

            var accounts = new List<Account>
            {
               new Account(1,"08294013","emmanuel@gmail.com","emmanuel",2500,Currency.GBP, "08352",cards[0]),
               new Account(2,"95260241","francis@gmail.com","francis", 450, Currency.GBP,"89372",cards[1]),
               new Account(3,"88627962","john@gmail.com","john", 300, Currency.GBP,"79275",cards[2]),
               new Account(4,"01006293","janice@gmail.com", "janice", 1100, Currency.GBP,"82064",cards[3]),
               new Account(5,"02572524","daniel@gmail.com","daniel",350,Currency.GBP,"63802",cards[4]),

               new Account(6,"30138030","info@amazon.com","amazon",56000,Currency.GBP,"47463",null),
               new Account(7,"07227042","info@jumia.com","jumia",54420,Currency.GBP,"83573",null),
               new Account(8,"00125726","info@next.com","next",24730,Currency.GBP,"56573",null),

            };

            return accounts; 
       }

       private static List<Card> GetCards()
       {
            var cards = new List<Card>
            {
                new Card("57247735457","567",2023,12, CardStatus.Active),
                new Card("52952772755","026",2022,9, CardStatus.Inactive),
                new Card("52893673695","093",2022,4, CardStatus.Inactive),
                new Card("52638026202","679",2022,8, CardStatus.Active),
                new Card("54247252893","825",2022,7, CardStatus.Active),
            };

            return cards; 
       }

       private static List<Transaction> GetTransactions()
        {
           
            var transactions = new List<Transaction>
            {
                new Transaction(new Guid("91125186-c214-4df4-a4c0-bf8ed8a029d2"),139.99m,1,TransactionStatus.pending,7),
                new Transaction(new Guid("0970bf04-c67b-4deb-8a2b-e500de607118"),43.59m,4,TransactionStatus.pending,7),
            };

            return transactions; 
        }
    }
}
