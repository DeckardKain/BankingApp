using BankingApp.Models.Accounts;

namespace BankingApp.Models.Customers
{
    // Single Responsilibility - 
    public class Customer
    {
        public int Id { get; set; }
        public CustomerData CustomerData { get; set; }
        public CustomerType CustomerType { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

        internal List<Account>? Accounts { get; set; }
    }

    public enum CustomerType
    {
        Regular,
        Preferred,
        VIP
    }
}
