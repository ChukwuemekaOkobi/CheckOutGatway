using Entities;
using Shared;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Data
{

    /// <summary>
    /// Data value used to seed the in memory database 
    /// </summary>
    public class Database
    {

        public static void AddData(ApplicationDbContext context)
        {
            context.AddRange(GetMerchants());
            context.AddRange(GetTransactions());

            context.SaveChanges();
        }


        private static List<Transaction> GetTransactions()
        {
            var transactions = new List<Transaction>
            {
                new Transaction(1,"f55f4879-6f4c-4b4c-9bdb-d968d8146563",DateTime.Now,23, Currency.GBP,TransactionStatus.successful,StringKeys.Success,"payment successful",1,"57247735457","567","12/2023",""),
                new Transaction(2,"D5534873-154c-4bcd-9b5b-d968f8446523",DateTime.Now,341, Currency.GBP,TransactionStatus.successful,StringKeys.Success,"payment successful", 2,"52952772755","026","9/2022",""),
                new Transaction(3,"765f4879-6f44-4b4c-9fdb-d968d8148003",DateTime.Now,45.6m, Currency.USD,TransactionStatus.failed,StringKeys.Fail,"failed: card declined",3,"52893673695","093","4/2022",""),
                new Transaction(4,"765f4079-6444-4eee-9fcb-d998d8180572",DateTime.Now,32.9m, Currency.USD,TransactionStatus.successful,StringKeys.Success,"payment successful",3,"52952772755","026","9/2022",""),
                new Transaction(5,"f0de21da-9312-4b83-af66-814ebf890e08",DateTime.Now,132.99m, Currency.GBP,TransactionStatus.pending,StringKeys.Pending,"payment pending",2,"57247735457","567","12/2023","91125186-c214-4df4-a4c0-bf8ed8a029d2"),
                new Transaction(6,"5747a3ca-3d20-4dc0-b5b9-4e5fee121fe0",DateTime.Now,43.59m, Currency.GBP,TransactionStatus.pending,StringKeys.Pending,"payment pending",2,"52638026202","679","8/2022","0970bf04-c67b-4deb-8a2b-e500de607118"),

            };

            return transactions; 
        }
        
        private static List<Merchant> GetMerchants()
        {
            var merchants = new List<Merchant>
            {
                new Merchant(1,"Amazon","info@amazon.com","30138030","d2baabad-8a32-4482-92a6-8e0a2e81f24e"),
                new Merchant(2,"Jumia","info@jumia.com"  ,"07227042","1fa0ce55-f16a-4bea-88c1-83f93fad640b"),
                new Merchant(3,"Next","info@next.com"    ,"00125726","8d8689a1-0157-4b4d-a468-4ba925e07a17")
            };

            return merchants; 
        }

        

    }
}
