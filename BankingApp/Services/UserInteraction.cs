using BankingApp.DTO.Account;
using BankingApp.DTO.System;
using BankingApp.DTO.UI;
using BankingApp.Managers;
using BankingApp.Services.Interfaces;
using BankingAppCore.Models.System;

namespace BankingApp.Services
{

    // Single Responsibility - managing user interactions such as authentication. It handles user authentication logic and notifies observers about authentication events.
    // Open/Closed - May not directly demonstrate open/closed principle, it can be considered open for extension in the sense that new functionalities related to user
    // interactions can be added without modifying the existing class structure. 
    // Interface Segregation - By implementing an interface, the class commits to providing implementations for the members defined by that interface.
    public class UserInteraction : IUserInteraction
    {
        private readonly IObserver _observer;
        private readonly IDataService _dataService;

        public string Username { get; set; } = default!;
        public bool isAuthenticated { get; set; } = false;
        public string UserId { get; set; } = default!;
        public Role Role { get; set; }
        public UserInteraction(IObserver observer, IDataService dataService)
        {
            _observer = observer;
            _dataService = dataService;
        }

        public async Task<bool> Authenticate(UserLoginDTO userLogin)
        {
            // Call Backend VIA API to authenticate and return Customer Info.

            var result = await _dataService.Login(userLogin);

            if (result.LoginResult)
            {
                // Update App of new Authentication
                Username = userLogin.Username;
                isAuthenticated = true;
                UserId = result.UserId;

                // Update AppManager of a new user login.
                AppManager.AddNewUserLogin(result);

                // Observer pattern demonstration for console logging
                var state = new StateChangeDTO()
                {
                    Message = "User Interaction: User " + Username + " has successfully authenticated."
                };

                _observer.Notify(state);

                return true;
            }
            else
            {
                return false;
            }          
        }

        public async Task<AccountDTO> CreateNewAccount(AccountDTO accountDTO)
        {
            var result = await _dataService.CreateNewAccount(accountDTO);
            return result;
        }

        public async Task<List<AccountDTO>> GetAllAccountsById(string id)
        {
            var result = (await _dataService.GetAllAccountsByCustomerId(id)).ToList();
            return result;
        }
    }
}
