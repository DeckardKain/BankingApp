using BankingAppBackEnd.Services.Interfaces;
using BankingAppBackEnd.Utilities;
using BankingAppCore.DTO.Transaction;
using BankingAppCore.Models.Accounts;
using BankingAppCore.Models.Transactions;

namespace BankingAppBackEnd.Factories
{
    public class TransactionFactory
    {
        private readonly IDataService<Transaction> _transactionDataService;
        private readonly IDataService<Account> _accountDataService;

        public TransactionFactory(IDataService<Transaction> transactionDataService, IDataService<Account> accountDataService)
        {
            _transactionDataService = transactionDataService;
            _accountDataService = accountDataService;
        }

        public async Task<TransactionDTO> CreateNew(TransactionDTO transactionDTO)
        {
            switch (transactionDTO.TransactionType)
            {
                case TransactionType.Withdrawal:
                    var debit = new Withdrawal()
                    {
                        Id = UUIDGenerator.GenerateUUID(),
                        Amount = transactionDTO.Amount,
                        DateTime = transactionDTO.DateTime,
                        AccountId = transactionDTO.AccountId,
                        MerchantInfo = transactionDTO.MerchantInfo,
                        TransactionType = transactionDTO.TransactionType,
                    };

                    var newDebit = await _transactionDataService.Create(debit);
                    await UpdateAccount(-newDebit.Amount, newDebit.AccountId);

                    transactionDTO.Id = newDebit.Id;

                    return transactionDTO;

                case TransactionType.Deposit:
                    var credit = new Deposit()
                    {
                        Id = UUIDGenerator.GenerateUUID(),
                        Amount = transactionDTO.Amount,
                        DateTime = transactionDTO.DateTime,
                        AccountId = transactionDTO.AccountId,
                        MerchantInfo = transactionDTO.MerchantInfo,
                        TransactionType = transactionDTO.TransactionType
                    };

                    var newCredit = await _transactionDataService.Create(credit);
                    await UpdateAccount(newCredit.Amount, newCredit.AccountId);
                    transactionDTO.Id = newCredit.Id;

                    return transactionDTO;

                default:
                    return new TransactionDTO();
            }
        }

        private async Task UpdateAccount(decimal? amount, string accountId)
        {
            var account = await _accountDataService.GetById(accountId);
            account.Balance += amount;

            await _accountDataService.Update(account);

        }
    }
}
