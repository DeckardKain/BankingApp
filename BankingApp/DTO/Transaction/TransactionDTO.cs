using BankingAppCore.Models.Transactions;

namespace BankingApp.DTO.Transaction
{
    public record TransactionDTO
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string AccountId { get; set; }
        public TransactionType TransactionType { get; set; }
        public string? MerchantInfo { get; set; }
        public DateTime DateTime { get; set; }
        public decimal? Amount { get; set; }

    }
}
