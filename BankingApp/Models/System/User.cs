namespace BankingApp.Models.System
{
    public class User
    {
        public int Id { get; set; }
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
