using BankingAppBackEnd.Services.Interfaces;
using BankingAppBackEnd.Utilities;
using BankingAppCore.DTO.Account;
using BankingAppCore.Models.Accounts;

namespace BankingAppBackEnd.Factories
{
    public class AccountFactory
    {
        private readonly IDataService<Account> _accountDataService;

        public AccountFactory(IDataService<Account> accountDataService)
        {
            _accountDataService = accountDataService;
        }

        public async Task<AccountDTO> Create(AccountDTO accountDTO)
        {
            switch (accountDTO.AccountType)
            {
                case AccountType.Chequing:
                    var chequingAccount = new Chequing
                    {
                        Id = UUIDGenerator.GenerateUUID(),
                        CustomerId = accountDTO.CustomerId,
                        Balance = 0                        
                    };

                    var newChequingAccount = await _accountDataService.Create(chequingAccount);

                    accountDTO.Id = newChequingAccount.Id;

                    return accountDTO;

                case AccountType.Savings:
                    var savingsAccount = new Savings
                    {
                        Id = UUIDGenerator.GenerateUUID(),
                        CustomerId = accountDTO.CustomerId,
                        Balance = 0
                    };

                    var newSavingsAccount = await _accountDataService.Create(savingsAccount);

                    accountDTO.Id = newSavingsAccount.Id;

                    return accountDTO;

                case AccountType.Retirement:
                    var retirementAccount = new Savings
                    {
                        Id = UUIDGenerator.GenerateUUID(),
                        CustomerId = accountDTO.CustomerId,
                        Balance = 0
                    };

                    var newRetirementAccount = await _accountDataService.Create(retirementAccount);

                    accountDTO.Id = newRetirementAccount.Id;

                    return accountDTO;

                default:

                    return accountDTO;
            }
        }
    }
}
