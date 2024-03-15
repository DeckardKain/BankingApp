using BankingAppCore.DTO.Account;
using BankingAppCore.Models.Accounts;

namespace BankingAppBackEnd.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDTO> CreateNewAccount(AccountDTO accountDTO);
        Task<List<Account>> GetAllAccountsById(string id);
    }
}
