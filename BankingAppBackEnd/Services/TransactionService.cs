using BankingAppBackEnd.Factories;
using BankingAppBackEnd.Services.Interfaces;
using BankingAppCore.DTO.Transaction;
using BankingAppCore.Models.Transactions;


namespace BankingAppBackEnd.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly TransactionFactory _transactionFactory;
        private readonly IDataService<Transaction> _transactionDataService;

        public TransactionService(TransactionFactory transactionFactory, IDataService<Transaction> transactionDataService)
        {
            _transactionFactory = transactionFactory;
            _transactionDataService = transactionDataService;
        }

        public async Task<TransactionDTO> CreateNewTransaction(TransactionDTO transaction)
        {
            var newTransaction = await _transactionFactory.CreateNew(transaction);
            return newTransaction;
        }

        public async Task<List<Transaction>> GetAllTransactionsById(string id)
        {
            var transactions = (await _transactionDataService.FindAllById(id)).ToList();           
            return transactions;
        }

    }
}
