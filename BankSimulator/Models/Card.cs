namespace BankSimulator.Models
{
    public class Card
    {
       public string Number { get;set; }
       public string Cvv { get; set; }
       public int ExpiryYear { get; set; }
       public int ExpiryMonth { get; set; }
       public CardStatus Status { get; set; }

        public Card(string number, string cvv, int year, int month, CardStatus status)
        {
            Number = number;
            Cvv = cvv;
            ExpiryMonth = month;
            ExpiryYear = year; 
            Status = status;
        }
    }

}
