using BankingAppBackEnd.Services.Interfaces;
using BankingAppBackEnd.Utilities;
using BankingAppCore.DTO.Transaction;
using BankingAppCore.Models.Transactions;


namespace BankingAppBackEnd.Factories
{
    public class TransactionFactory
    {
        private readonly IDataService<Transaction> _transactionDataService;

        public TransactionFactory(IDataService<Transaction> transactionDataService)
        {
            _transactionDataService = transactionDataService;
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

                    transactionDTO.Id = newCredit.Id;

                    return transactionDTO;

                default:
                    return new TransactionDTO();
            }
        }
    }
}
