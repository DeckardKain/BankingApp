using BankingAppCore.Models.Accounts;
using BankingAppCore.Models.System;

namespace BankingAppCore.Models.Customers
{
    // Single Responsilibility - 
    public class Customer
    {
        public string Id { get; set; }
        public CustomerData? CustomerData { get; set; }
        public CustomerType CustomerType { get; set; }
        public User? UserCredentials { get; set; }
        public List<Account>? Accounts { get; set; }
    }

    public enum CustomerType
    {
        Regular,
        Preferred,
        VIP
    }
}
