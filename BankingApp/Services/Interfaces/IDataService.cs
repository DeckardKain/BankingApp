using BankingApp.Models.Accounts;
using BankingApp.Models.Customers;
using BankingApp.Models.Transactions;

namespace BankingApp.Services.Interfaces
{
    public interface IDataService
    {
        public List<Customer> GetAllCustomers();
        public List<Account> GetAllAccountsByCustomerId(int id);
        public List<Transaction> GetAllTransactionsByAccountId(string id);
        public Customer GetCurrentCustomer();
        public List<Account> GetCustomerAccounts();
        public void AddNewCustomer(Customer customer);
        public void AddNewTransaction(Transaction transaction);
        public bool SeedData();
    }
}
