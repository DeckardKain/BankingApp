using BankingAppCore.Models.Transactions;

namespace BankingAppCore.Models.Accounts
{
    // Single Responsibility - Represents only a financial account.
    // Open/Closed - We can create subclasses to extend functionality.
    public abstract class Account
    {
        public string id { get; set; }
        public string customerId { get; set; }
        public string accountId { get; set; }        
        public AccountType accountType { get; set; }
        public List<Transaction>? transactions { get; set; }
        public decimal? balance { get; set; }
    }

    public enum AccountType
    {
        Chequing,
        Savings,
        Retirement
    }
}
