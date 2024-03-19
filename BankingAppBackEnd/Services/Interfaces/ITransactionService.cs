using BankingAppCore.DTO.Transaction;
using BankingAppCore.Models.Transactions;

namespace BankingAppBackEnd.Services.Interfaces
{
    public interface ITransactionService
    {
        Task<TransactionDTO> CreateNewTransaction(TransactionDTO transaction);
        Task<List<Transaction>> GetAllTransactionsById(string id);
    }
}
