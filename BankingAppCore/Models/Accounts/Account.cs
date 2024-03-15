using BankingAppCore.Models.Customers;
using BankingAppCore.Models.Transactions;

namespace BankingAppCore.Models.Accounts
{
    // Single Responsibility - Represents only a financial account.
    // Open/Closed - We can create subclasses to extend functionality.
    public abstract class Account
    {
        public string Id { get; set; }  
        public string CustomerId { get; set; }
        public AccountType AccountType { get; set; }
        public List<Transaction>? Transactions { get; set; }
        public decimal? Balance { get; set; }
    }

    public enum AccountType
    {
        Chequing,
        Savings,
        Retirement
    }
}
