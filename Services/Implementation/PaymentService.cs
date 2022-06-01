using BankSimulator.Service;
using Entities;
using Microsoft.EntityFrameworkCore;
using Services.Data;
using Shared;
using Shared.Extentions;
using Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implementation
{
    /// <summary>
    /// Payment Gatway business logic function to carry out transactions
    /// </summary>
    public class PaymentService : IPayment
    {
        readonly ApplicationDbContext _context;
        public PaymentService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Initiates the payment process
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<PaymentResponse> IntiatePayment(string ApiKey, PaymentRequest model)
        {
            // Get Merchant 
            var Merchant = _context.Merchants.FirstOrDefault(m => m.ApiKey == ApiKey);


            PaymentModel bankModel = new PaymentModel
            {
                ExpiryMonth = model.ExpiryMonth,
                ExpiryYear = model.ExpiryYear,
                Amount = model.Amount,
                CardNumber = model.CardNumber,
                Currency = model.Currency,
                Cvv = model.Cvv,
                MerchantBankAccount = Merchant.AccountNumber
            };

            //process Bank Payments simulation
            var result = BankService.InitiatePayment(bankModel);


            var newTransaction = new Transaction
            {
                MerchantId = Merchant.Id,
                Expiry = $"{model.ExpiryMonth}/{model.ExpiryYear}",
                Amount = model.Amount,
                Currency = model.Currency,
                CardNumber = model.CardNumber,
                Created = DateTime.Now,
                Cvv = model.Cvv,
                Reference = Guid.NewGuid().ToString(),
                MetaData = result.Item3
            };

            string message = "";
            if (result.Item1 == true)
            {
                newTransaction.Status = TransactionStatus.pending;
                newTransaction.StatusCode = StringKeys.Pending;
                newTransaction.BankReference = result.Item2.Value.ToString();
                message = "request successful";
            }
            else
            {
                newTransaction.Status = TransactionStatus.failed;
                newTransaction.StatusCode = StringKeys.Fail;
                message = "request failed";
            }

            _context.Add(newTransaction);

            _context.SaveChanges();

            var response = new PaymentResponse
            {
                Reference = newTransaction.Reference,
                MetaData = newTransaction.MetaData,
                Status = newTransaction.Status.ToString(),
            };


            return new Result<PaymentResponse>
            {
                Data = response,
                StatusCode = newTransaction.StatusCode,
                Message = message
            };

        }

        /// <summary>
        /// Completes the payment process with the reference and otp
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result<PaymentResponse> ProcessOtp(string ApiKey, OtpRequest request)
        {
            // Get Merchant 
            var Merchant = _context.Merchants.FirstOrDefault(m => m.ApiKey == ApiKey);

            var transaction = _context.Transactions.FirstOrDefault(t => t.Reference == request.Reference);

            if (transaction == null || transaction.BankReference == null)
            {
                return new Result<PaymentResponse>
                {
                    Data = null,
                    Message = "Reference Invalid",
                    StatusCode = StringKeys.Fail

                };
            }

            var bankRef = new Guid(transaction.BankReference);

            var result = BankService.ProcessOtp(bankRef, request.Otp);

            string message = "";
            if (result.Item1 == true)
            {
                transaction.Status = TransactionStatus.successful;
                transaction.StatusCode = StringKeys.Success;
                transaction.MetaData = result.Item2;
                message = "request successful";
            }
            else
            {
                transaction.Status = TransactionStatus.failed;
                transaction.StatusCode = StringKeys.Fail;
                transaction.MetaData = result.Item2;
                message = "request failed";
            }

            _context.Update(transaction);

            _context.SaveChanges();

            var response = new PaymentResponse
            {
                Reference = transaction.Reference,
                MetaData = transaction.MetaData,
                Status = transaction.Status.ToString(),
            };

            return new Result<PaymentResponse>
            {
                Data = response,
                Message = message,
                StatusCode = transaction.StatusCode
            };

        }


        /// <summary>
        /// Gets the previous transactions for each merchant and includes 
        /// additional filter parameters
        /// </summary>
        /// <param name="ApiKey"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="status"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        public Result<List<TransactionResponse>> GetTransactions(string ApiKey,DateTime? from , DateTime? to, TransactionStatus? status, Currency? currency)
        {
            // Get Merchant 
            var Merchant = _context.Merchants.FirstOrDefault(m => m.ApiKey == ApiKey);

            if(Merchant == null)
            {
                return new Result<List<TransactionResponse>>
                {
                    Message = "invalid request",
                    StatusCode = StringKeys.Fail
                };
            }

            var transactions = _context.Transactions.Where(m => m.MerchantId == Merchant.Id); 

            // pass filters
            if(from != null)
            {
                transactions = transactions.Where(t => t.Created >= from.Value);
            }
            if(to != null)
            {
                transactions = transactions.Where(t=> t.Created <= to.Value);
            }
            if(status != null)
            {
                transactions = transactions.Where(t=>t.Status == status.Value);
            }

            if(currency!=null)
            {
                transactions = transactions.Where(t => t.Currency == currency.Value);
            }
            var response = transactions.Select(t => new TransactionResponse
                            {
                                Amount = t.Amount,
                                Comment = t.MetaData,
                                Currency = t.Currency.ToString(),
                                Reference = t.Reference,
                                StatusCode = t.StatusCode,
                                Time = t.Created,
                                Status = t.Status.ToString(),
                                CardNumber = t.CardNumber.Mask(),
                                Cvv = t.Cvv.Mask(),
                                CardExpiry = t.Expiry.Mask(),
                            }
                         ).ToList();


            return new Result<List<TransactionResponse>>
            {
                Data = response,
                Message = "success",
                StatusCode = StringKeys.Success
            };

        }
    }
}
