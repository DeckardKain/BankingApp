using BankingAppCore.Models.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppCore.DTO.Transaction
{
    public record TransactionDTO
    {
        public string Id { get; set; }
        public string AccountId { get; set; }
        public string CustomerId { get; set; }
        public TransactionType TransactionType { get; set; }
        public string? MerchantInfo { get; set; }
        public DateTime DateTime { get; set; }
        public decimal? Amount { get; set; }
    }
}
