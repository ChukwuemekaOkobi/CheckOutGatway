using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Merchant data model to hold Merchant information
    /// </summary>
    public class Merchant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string AccountNumber { get; set; }
        public string ApiKey { get; set; }
        public List<Transaction> Transactions { get; set; }


        public Merchant(int id , string name, string email, string accountNumber, string apiKey)
        {
            Id = id;
            Name = name;
            Email = email;
            AccountNumber = accountNumber;
            ApiKey = apiKey;
           // Transactions = transactions;
        }
    }




}
