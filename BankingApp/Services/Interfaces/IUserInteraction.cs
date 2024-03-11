using BankingApp.Models.UI;
using System.Runtime.InteropServices;

namespace BankingApp.Services.Interfaces
{
    public interface IUserInteraction
    {
        string Username { get; set; }
        bool isAuthenticated { get; set; }
        bool Authenticate(UserLoginDTO userLogin);
        
    }
}
