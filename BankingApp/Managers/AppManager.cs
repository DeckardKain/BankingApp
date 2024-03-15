using BankingApp.DTO.UI;

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
                        
        public static List<UserLoginDTO> LoggedInUsers = new List<UserLoginDTO>();        

        public static void AddNewUserLogin(UserLoginDTO userLogin)
        {

            if (userLogin.LoginResult)
            {
                LoggedInUsers.Add(userLogin);
            }
        }





    }
}
