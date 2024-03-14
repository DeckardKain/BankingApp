using BankingAppCore.Models.Accounts;

namespace BankingAppCore.Models.Transactions
{

    // Single Responsilibility - Represents a financial transaction only.
    // Open/Closed - Being an abstract class this class is open for extension to represent different types of transactions.   
    public abstract class Transaction
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public TransactionType TransactionType { get; set; }
        public string? MerchantInfo { get; set; }
        public DateTime DateTime { get; set; }
        public decimal? Amount { get; set; }

    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }
}
