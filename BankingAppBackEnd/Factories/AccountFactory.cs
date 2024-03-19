using BankingAppBackEnd.Services.Interfaces;
using BankingAppBackEnd.Utilities;
using BankingAppCore.DTO.Account;
using BankingAppCore.Models.Accounts;
using BankingAppCore.Models.Customers;

namespace BankingAppBackEnd.Factories
{
    public class AccountFactory
    {
        private readonly IDataService<Account> _accountDataService;
        private readonly IDataService<Customer> _customerDataService;

        public AccountFactory(IDataService<Account> accountDataService, IDataService<Customer> customerDataService)
        {
            _accountDataService = accountDataService;
            _customerDataService = customerDataService;
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
                        Balance = 0,
                        AccountType = AccountType.Chequing                        
                    };

                    var newChequingAccount = await _accountDataService.Create(chequingAccount);

                    accountDTO.Id = newChequingAccount.Id;

                    return accountDTO;

                case AccountType.Savings:
                    var savingsAccount = new Savings
                    {
                        Id = UUIDGenerator.GenerateUUID(),
                        CustomerId = accountDTO.CustomerId,
                        Balance = 0,
                        AccountType = AccountType.Savings
                    };

                    var newSavingsAccount = await _accountDataService.Create(savingsAccount);

                    accountDTO.Id = newSavingsAccount.Id;

                    return accountDTO;

                case AccountType.Retirement:
                    var retirementAccount = new Retirement
                    {
                        Id = UUIDGenerator.GenerateUUID(),
                        CustomerId = accountDTO.CustomerId,
                        Balance = 0,
                        AccountType = AccountType.Retirement
                    };

                    var newRetirementAccount = await _accountDataService.Create(retirementAccount);

                    accountDTO.Id = newRetirementAccount.Id;

                    return accountDTO;

                default:

                    return accountDTO;
            }
        }

        public async Task CreateAccounts()
        {
            var customers = await _customerDataService.GetAll();

            foreach(var customer in customers) 
            {
                var account = ObjectGenerator.GenerateRandomAccount(customer.Id);
                await Create(account);
            }
        }
    }
}
