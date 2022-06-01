

using Shared.Models;

namespace BankSimulator.Models
{
    public class Account
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }
        public decimal Balance { get; set; }

        public Currency Currency { get; set; }

        public string OTP { get; set; }

        public Card Card { get; set; }

        public Account(int id, string number, string email, string name, decimal balance, Currency currency, string oTP, Card card)
        {
            Id = id;
            Number = number;
            Email = email;
            Name = name;
            Balance = balance;
            Currency = currency;
            OTP = oTP;
            Card = card;
        }

    }


    public enum CardStatus
    {
        Active, 
        Inactive,
    }

}
