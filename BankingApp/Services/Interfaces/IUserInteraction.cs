using BankingApp.DTO.Account;
using BankingApp.DTO.UI;

namespace BankingApp.Services.Interfaces
{
    public interface IUserInteraction
    {
        string? Username { get; set; }
        bool isAuthenticated { get; set; }
        string? UserId { get; set; }
        Task<bool> Authenticate(UserLoginDTO userLogin);
        Task<AccountDTO> CreateNewAccount(AccountDTO accountDTO);
        Task<List<AccountDTO>> GetAllAccountsById(string id);

    }
}
