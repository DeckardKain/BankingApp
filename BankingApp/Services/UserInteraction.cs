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

        public string Username { get; set; } = default!;
        public bool isAuthenticated { get; set; } = false;
        public Role Role { get; set; }
        public UserInteraction(IObserver observer)
        {
            _observer = observer;
        }

        public bool Authenticate(UserLoginDTO userLogin)
        {
            Username = userLogin.Username;
            isAuthenticated = true;
            userLogin.LoginResult = true;
            
            // Update AppManager of a new user login.

            AppManager.AddNewUserLogin(userLogin);

            // Observer pattern demonstration for console logging

            var state = new StateChangeDTO()
            {
                Message = "User Interaction: User " + Username + " has successfully authenticated."
            };
            
            _observer.Notify(state);

            return true;
        }
    }
}
