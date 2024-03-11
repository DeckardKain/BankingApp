namespace BankingApp.Models.Transactions
{

    // Single Responsilibility - Represents a financial transaction only.
    // Open/Closed - Being an abstract class this class is open for extension to represent different types of transactions.   
    public abstract class Transaction
    {
        public int id { get; set; }
        public string accountId { get; set; }
        public TransactionType transactionType { get; set; }
        public string? merchantInfo { get; set; }
        public DateTime dateTime { get; set; }
        public decimal? amount { get; set; }

    }

    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }
}
