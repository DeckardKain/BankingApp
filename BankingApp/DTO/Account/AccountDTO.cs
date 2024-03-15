
namespace BankingApp.DTO.Account
{
    public record AccountDTO
    {
        public string? Id { get; set; }
        public string? CustomerId { get; set; }
        public AccountType AccountType { get; set; }
        public decimal? Balance { get; set; }
    }

    public enum AccountType
    {
        Chequing,
        Savings,
        Retirement
    }
}
