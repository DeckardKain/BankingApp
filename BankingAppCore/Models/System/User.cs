using BankingAppCore.Models.Customers;

namespace BankingAppCore.Models.System
{
    public class User
    {
        public string Id { get; set; }
        public string CustomerId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAuthenticated { get; set; }
        public Role Role { get; set; }
    }

    public enum Role
    {
        Customer,
        Employee,
        Admin
    }
}
