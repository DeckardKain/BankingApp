using BankingAppCore.DTO.UI;
using BankingAppCore.Models.System;

namespace BankingAppBackEnd.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserById(string id);
        Task<UserLoginDTO> AuthenticateUser(UserLoginDTO userLoginDTO);
    }
}
