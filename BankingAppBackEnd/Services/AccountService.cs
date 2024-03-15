using BankingAppBackEnd.Factories;
using BankingAppBackEnd.Services.Interfaces;
using BankingAppCore.DTO.Account;
using BankingAppCore.Models.Accounts;

namespace BankingAppBackEnd.Services
{
    public class AccountService : IAccountService
    {
        private readonly IDataService<Account> _dataService;
        private readonly AccountFactory _accountFactory;

        public AccountService(IDataService<Account> dataService, AccountFactory accountFactory)
        {
            _dataService = dataService;
            _accountFactory = accountFactory;
        }

        public async Task<AccountDTO> CreateNewAccount(AccountDTO accountDTO)
        {
            var newAccount = await _accountFactory.Create(accountDTO);
            return newAccount;

        }
    }
}
