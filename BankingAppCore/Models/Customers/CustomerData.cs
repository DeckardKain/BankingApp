namespace BankingAppCore.Models.Customers
{
    public class CustomerData
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        // Navigation property - One to One
        public Customer Customer { get; set; }
    }
}
