using BankingAppCore.Models.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingAppCore.DTO.Account
{
    public record AccountDTO
    {
        public string? Id { get; set; }
        public string? CustomerId { get; set; }
        public AccountType AccountType { get; set; }
        public decimal? Balance { get; set; }
    }
}
