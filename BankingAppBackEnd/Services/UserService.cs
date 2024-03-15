using BankingAppBackEnd.Services.Interfaces;
using BankingAppCore.DTO.UI;
using BankingAppCore.Models.System;

namespace BankingAppBackEnd.Services
{
    public class UserService : IUserService
    {
        private readonly IDataService<User> _dataService;

        public UserService(IDataService<User> dataService)
        {
            _dataService = dataService;
        }

        public async Task<User> GetUserById(string id)
        {
            var user = await _dataService.GetById(id);
            return user;
        }

        public async Task<UserLoginDTO> AuthenticateUser(UserLoginDTO userLoginDTO)
        {
            var dbUser = await _dataService.FindByColumnAsync("Username", userLoginDTO.Username);
            if (dbUser != null)
            {
                if (dbUser.Password == userLoginDTO.Password)
                {
                    userLoginDTO.LoginResult = true;
                }
            }
            else
            {
                userLoginDTO.LoginResult = false;
            }

            return userLoginDTO;
        }

    }
}
