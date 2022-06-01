using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankSimulator;
using BankSimulator.Service;
using Shared.Models;

namespace Test.BankSimulator
{
    public class InitiatePaymentTest
    {

        [Fact]
        public void IntiatePaymentValidTrue()
        {
            PaymentModel model = new PaymentModel
            {
                Amount = 150,
                Currency = Currency.GBP,
                CardNumber = "57247735457",
                Cvv = "567",
                ExpiryMonth = 12,
                ExpiryYear = 2023,
                MerchantBankAccount = "07227042"
            };

            var result = BankService.InitiatePayment(model);

            Assert.NotNull(result.Item2);
            Assert.True(result.Item1);
        }

        [Fact]
        public void InitiatePaymentInvalidCard()
        {
            PaymentModel model = new PaymentModel
            {
                Amount = 150,
                Currency = Currency.GBP,
                CardNumber = "572477243457",
                Cvv = "566",
                ExpiryMonth = 11,
                ExpiryYear = 2022,
                MerchantBankAccount = "07227042"
            };

            var result = BankService.InitiatePayment(model);

            Assert.Null(result.Item2);
            Assert.False(result.Item1);
        }

        [Fact]
        public void InitiatePaymentExpiredCard()
        {
            PaymentModel model = new PaymentModel
            {
                Amount = 48.9m,
                Currency = Currency.GBP,
                CardNumber = "52893673695",
                Cvv = "093",
                ExpiryMonth = 4,
                ExpiryYear = 2022,
                MerchantBankAccount = "07227042"
            };

            var result = BankService.InitiatePayment(model);

            Assert.Null(result.Item2);
            Assert.False(result.Item1);
        }

        [Fact]
        public void InitiatePaymentInvalidMerchantAccount()
        {
            PaymentModel model = new PaymentModel
            {
                Amount = 48.9m,
                Currency = Currency.GBP,
                CardNumber = "52893673695",
                Cvv = "093",
                ExpiryMonth = 4,
                ExpiryYear = 2022,
                MerchantBankAccount = "80624602"
            };

            var result = BankService.InitiatePayment(model);

            Assert.Null(result.Item2);
            Assert.False(result.Item1);
        }

        [Fact]
        public void InitiatePaymentInsufficientFunds()
        {
            PaymentModel model = new PaymentModel
            {
                Amount = 56000m,
                Currency = Currency.GBP,
                CardNumber = "52893673695",
                Cvv = "093",
                ExpiryMonth = 4,
                ExpiryYear = 2022,
                MerchantBankAccount = "80624602"
            };

            var result = BankService.InitiatePayment(model);

            Assert.Null(result.Item2);
            Assert.False(result.Item1);
        }
    }
}
