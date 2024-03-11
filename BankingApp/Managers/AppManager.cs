using BankingApp.Models.System;
using BankingApp.Models.UI;

namespace BankingApp.Managers
{
    public class AppManager
    {
        // PATTERN DEMONSTRATION
        // **** SINGLETON ****

        // The unique object of AppManager stored and shared here
        private static AppManager _instance;
        
        // Private constructor to prevent instantiation from outside this class
        private AppManager() { }

        public static AppManager GetInstance()
        {
            if(_instance == null)
            {
                _instance = new AppManager();
            }

            return _instance;
        }

        // *** END SINGLETON PATTERN ***

        
        public delegate void OnUserLogin(UserLoginDTO userLoginDTO);
                        
        static List<User> LoggedInUsers = new List<User>();        

        public static void AddNewUserLogin(UserLoginDTO userLogin)
        {
            var user = new User();
            user.Username = userLogin.Username;
            user.Password = userLogin.Password;
            user.IsAuthenticated = userLogin.LoginResult;

            if (user.IsAuthenticated)
            {
                LoggedInUsers.Add(user);
            }
        }





    }
}
