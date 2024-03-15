using BankingAppCore.Models.Accounts;
using BankingAppCore.Models.Customers;
using BankingAppCore.Models.System;

namespace BankingAppCore.DTO.Customer
{
    public record AllCustomersDTO
    {
        public string Id { get; set; }        
        public CustomerData CustomerData { get; set; }

        public CustomerType CustomerType { get; set; }
        
        public User? UserCredentials { get; set; }
    }
}
