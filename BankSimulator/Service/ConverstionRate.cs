using Shared.Models;

namespace BankSimulator.Service
{
    public static class ConverstionRate
    {
        public const double GBPToUSD = 1.26;
        public const double GBPToEuro = 1.17;
        public const double EurotoUSD = 1.07;


        public static (Currency, double) CurrencyConversion(double Amount, Currency from, Currency to)
        {
            var newAmount = 0.0;

            if (from == Currency.GBP && to == Currency.USD)
            {
                newAmount = Amount * ConverstionRate.GBPToUSD;
            }

            else if (from == Currency.GBP && to == Currency.EURO)
            {
                newAmount = Amount * ConverstionRate.GBPToEuro;
            }

            else if (from == Currency.EURO && to == Currency.GBP)
            {
                newAmount = Amount / ConverstionRate.GBPToEuro;
            }

            return (to, newAmount);
        }
    }


}
