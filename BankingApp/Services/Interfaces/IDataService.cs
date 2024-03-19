using BankingApp.DTO.Account;
using BankingApp.DTO.Customer;
using BankingApp.DTO.Transaction;
using BankingApp.DTO.UI;


namespace BankingApp.Services.Interfaces
{
    public interface IDataService
    {
        Task<bool> CreateNewCustomer(UserRegisterDTO userRegistration);
        Task<List<AllCustomerDTO>> GetAllCustomers();
        Task<UserLoginDTO> Login(UserLoginDTO loginDTO);
        Task<AccountDTO> CreateNewAccount(AccountDTO accountDTO);
        Task<List<AccountDTO>> GetAllAccountsByUserId(string id);
        Task<string> GetCustomerId(string id);
        Task<List<TransactionDTO>> GetAllTransactionsByAccountId(string accountNumber);
        Task<TransactionDTO> CreateNewTransaction(TransactionDTO transactionDTO);
    }
}
