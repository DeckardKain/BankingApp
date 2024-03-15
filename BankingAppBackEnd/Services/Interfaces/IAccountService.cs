using BankingAppCore.DTO.Account;

namespace BankingAppBackEnd.Services.Interfaces
{
    public interface IAccountService
    {
        Task<AccountDTO> CreateNewAccount(AccountDTO accountDTO);
    }
}
