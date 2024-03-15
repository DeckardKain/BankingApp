using BankingAppCore.Models.Accounts;
using BankingAppCore.Models.System;

namespace BankingAppCore.Models.Customers
{
    // Single Responsilibility - 
    public class Customer
    {
        // Primary key for Customer Class.
        public string Id { get; set; }

        // Foriegn key property to maintain the relationship with CustomerData
        public string? CustomerDataId { get; set; }

        public CustomerType? CustomerType { get; set; }

        // Foreign key property for User Creds
        public string? UserCredentialsId { get; set; }

        public List<Account>? Accounts { get; set; }
    }

    public enum CustomerType
    {
        Regular,
        Preferred,
        VIP
    }
}
