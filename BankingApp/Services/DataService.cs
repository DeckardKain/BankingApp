using BankingApp.Services.Interfaces;
using BankingApp.DTO.UI;
using System.Net.Http.Json;
using Newtonsoft.Json;
using BankingApp.DTO.Customer;
using BankingApp.DTO.Account;
using BankingApp.DTO.Transaction;
using System.Text;


namespace BankingApp.Services
{

    // Single Responsibility - Focuses only on handling data-related operations such as adding customers, transactions and seeding data.
    // Open/Closed - Open to add additional functionality as required.
    // Liskov Substitution - Derived classes or implementations can be substituted for the base class or interface (IDataService) without affecting the correctness of the program.
    // Interface Segregation - Implements the IDataService interface, which defines specific data service methods. This ensures that clients are not forced to depend on methods they do not use.
    // Dependency Inversion - Depends on abstractions (IObserver, ILogger) rather than concrete implementations. This allows for flexibility and easier testing, as the specific implementations can be injected at runtime.


    // Design Patterns
    // Dependency Injection - this class takes dependencies in through its contructor parameters instead of instantiating them internally. This allows for loose coupling.
    // Repository Pattern - Encapsulates logic for data access and manipulation, though not a true representation of this pattern.
    // Observer Pattern - An IObserver is injected via the CTOR
    // Factory Method Pattern - The CreateRandomTransactions method can be classified as this, as it is creating transactions.
    // Null Object Pattern - When an exception occurs in methods like AddNewCustomer, it logs the error and re-throws the exception, which is handled by the caller. This prevents returning null or unexpected states.
    // Builder Pattern - The method CreateRandomTransactions can be seen as a simple form of the builder pattern, where it constructs complex objects (transactions) step by step using a consistent process.

    public class DataService : IDataService
    {
        private readonly IObserver _observer;
        private readonly ILogger _logger;
        private readonly HttpClient _httpClient;


        public DataService(IObserver observer, ILogger<DataService> logger, HttpClient httpClient)
        {
            _observer = observer ?? throw new ArgumentNullException(nameof(observer));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _httpClient.BaseAddress = new Uri("https://localhost:7176/api/");
        }

        public async Task<bool> CreateNewCustomer(UserRegisterDTO userRegistration)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("userregisterdto", userRegistration);
                response.EnsureSuccessStatusCode();

                return true;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new customer,");

                if (ex.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    // Example of string formatting
                    _logger.LogWarning("Bad request error occurred: {ErrorMessage}", ex.Message);
                }

                if (ex.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    // Example of string interpolation, the preferred way.
                    _logger.LogWarning($"Unauthorized: {ex.Message}");
                }

                return false;
            }
        }

        public async Task<List<AllCustomerDTO>> GetAllCustomers()
        {
            List<AllCustomerDTO> customers = new List<AllCustomerDTO>();

            try
            {
                var response = await _httpClient.GetAsync("getallcustomers");

                if (response.IsSuccessStatusCode)
                {
                    string jsonReponse = await response.Content.ReadAsStringAsync();
                    customers = JsonConvert.DeserializeObject<List<AllCustomerDTO>>(jsonReponse);
                }
                else
                {
                    Console.WriteLine($"Failed to get customer data. Status Code: {response.StatusCode}");
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"An error occurred while getting customers: {ex.Message}");
            }

            return customers;
        }

        public async Task<UserLoginDTO> Login(UserLoginDTO loginDTO)
        {
            try
            {
                // Send the UserLoginDTO to the API for authentication
                var response = await _httpClient.PostAsJsonAsync("authenticate", loginDTO);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response content to get the login result
                    var loginResult = await response.Content.ReadFromJsonAsync<UserLoginDTO>();
                    return loginResult;
                }
                else
                {
                    // Handle unsuccessful response (e.g., log error, throw exception)
                    Console.WriteLine($"Failed to login. Status Code: {response.StatusCode}");
                    return new UserLoginDTO();
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log error, throw custom exception)
                Console.WriteLine($"An error occurred while attempting to authenticate: {ex.Message}");
                return new UserLoginDTO();
            }
        }

        public async Task<AccountDTO> CreateNewAccount(AccountDTO accountDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("newaccount", accountDTO);
                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response content to get the login result
                    var accountResult = await response.Content.ReadFromJsonAsync<AccountDTO>();
                    return accountResult;
                }
                else
                {
                    // Handle unsuccessful response (e.g., log error, throw exception)
                    Console.WriteLine($"Failed to login. Status Code: {response.StatusCode}");
                    return new AccountDTO();
                }
            }       
            catch (Exception ex)
            {
                // Handle exception (e.g., log error, throw custom exception)
                Console.WriteLine($"An error occurred while attempting to authenticate: {ex.Message}");
                return new AccountDTO();
            }
        }


        public async Task<List<AccountDTO>> GetAllAccountsByUserId(string userId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"getallaccountsbyid/{userId}");
                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response content to get the result
                    var result = await response.Content.ReadFromJsonAsync<List<AccountDTO>>();
                    return result;
                }
                else
                {
                    // Handle unsuccessful response (e.g., log error, throw exception)
                    Console.WriteLine($"Failed to login. Status Code: {response.StatusCode}");
                    return new List<AccountDTO>();
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log error, throw custom exception)
                Console.WriteLine($"An error occurred while attempting to authenticate: {ex.Message}");
                return new List<AccountDTO>();
            }
        }

        public async Task<string> GetCustomerId(string id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"getcustomerbyid/{id}");
                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return content;
                }
                else
                {
                    // Handle unsuccessful response (e.g., log error, throw exception)
                    Console.WriteLine($"Failed to login. Status Code: {response.StatusCode}");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log error, throw custom exception)
                Console.WriteLine($"An error occurred while attempting to authenticate: {ex.Message}");
                return string.Empty;
            }
        }

        public async Task<List<TransactionDTO>> GetAllTransactionsByAccountId(string accountId)
        {
            try
            {
                // Serialize the account number into JSON format
                var jsonData = JsonConvert.SerializeObject(accountId);

                // Create StringContent with JSON data
                var jsonContent = new StringContent(jsonData, Encoding.UTF8, "application/json");

                // Create HttpRequestMessage with GET method and content
                var request = new HttpRequestMessage(HttpMethod.Get, "GetAccountTransactions");
                request.Content = jsonContent;

                // Send the request
                var response = await _httpClient.SendAsync(request);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response content into a list of transactions
                    var content = await response.Content.ReadAsStringAsync();
                    var transactions = JsonSerializer.Deserialize<List<TransactionDTO>>(content);
                    return transactions;
                }
                else
                {
                    // Handle unsuccessful response
                    // You can throw an exception or return null, depending on your requirements
                    throw new Exception($"Failed to get transactions. Status code: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
                // You can throw the exception or log it, depending on your requirements
                throw ex;
            }
        }

        public async Task<TransactionDTO> CreateNewTransaction(TransactionDTO transactionDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("newtransaction", transactionDTO);
                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Deserialize the response content to get the login result
                    var accountResult = await response.Content.ReadFromJsonAsync<TransactionDTO>();
                    return accountResult;
                }
                else
                {
                    // Handle unsuccessful response (e.g., log error, throw exception)
                    Console.WriteLine($"Failed to login. Status Code: {response.StatusCode}");
                    return new TransactionDTO();
                }
            }
            catch (Exception ex)
            {
                // Handle exception (e.g., log error, throw custom exception)
                Console.WriteLine($"An error occurred while attempting to authenticate: {ex.Message}");
                return new TransactionDTO();
            }
        }
    }
}



        //public bool SeedData()
        //{
        //    bool result = false;
        //    string message = "Data Service: Data was not seeded.";

        //    if (Accounts.IsNullOrEmpty())
        //    {
        //        Accounts =
        //        [
        //            new Chequing {
        //            id = 1,
        //            accountId = "c1 152 34FD 2134",
        //            accountType = AccountType.Chequing,
        //            balance = 0,
        //            customerId = 1
        //        },
        //        new Savings
        //        {
        //            id = 1,
        //            accountId = "s4 578 65SD 2357",
        //            accountType = AccountType.Savings,
        //            balance = 0,
        //            customerId = 1
        //        },
        //        new Retirement
        //        {
        //            id = 1,
        //            accountId = "r2 567 234D 7677",
        //            accountType = AccountType.Retirement,
        //            balance = 0,
        //            customerId = 1
        //        }
        //        ];

        //        message = "SeedData: Account data seeded success.";

        //        Customer = new Customer
        //        {
        //            Id = 1,
        //            Accounts = Accounts,
        //            Username = "MMoney",
        //            Password = "password",
        //            CustomerData = new CustomerData()
        //            {
        //                Id = 1,
        //                Name = "Moe Money",
        //                Address = "123 Fake Street",
        //                CustomerId = 1,
        //                Email = "BigMoney@gmail.com",
        //                PhoneNumber = "403-555-1234"
        //            }
        //        };

        //        message += "\nSeedData: Customer data seeded success.";

        //        CreateRandomTransactions("c1 152 34FD 2134", 20);
        //        CreateRandomTransactions("s4 578 65SD 2357", 20);
        //        CreateRandomTransactions("r2 567 234D 7677", 20);

        //        message += "\nSeedData: Transaction data seeded success.";

        //        CalculateAccountBalances();

        //        message += "\nSeedData: Calculate account balances success";

        //        result = true;
        //    }
        //    else
        //    {
        //        message = "Data Service: No need to seed data as it has already been completed";
        //    }

        //    var state = new StateChangeDTO()
        //    {
        //        Message = message
        //    };

        //    _observer.Notify(state);

        //    return result;
        //}

        //private void CalculateAccountBalances()
        //{
        //    foreach (var account in Accounts)
        //    {
        //        var debits = Transactions.Where(x => x.accountId == account.accountId)
        //            .Where(y => y.transactionType == TransactionType.Withdrawal).Sum(z => z.amount);
        //        var credits = Transactions.Where(x => x.accountId == account.accountId)
        //            .Where(y => y.transactionType == TransactionType.Deposit).Sum(z => z.amount);

        //        account.balance = credits - debits;
        //    }
        //}

        //public void CreateRandomTransactions(string account, int amount)
        //{
        //    for (int i = 0; i < amount; i++)
        //    {

        //        if (new Random().NextDouble() < 0.5)
        //        {
        //            var transaction = new Deposit()
        //            {
        //                id = i,
        //                accountId = account,
        //                amount = (decimal)(new Random().Next(0, 10000) / 100.00m),
        //                dateTime = DateTime.Now.AddDays(-new Random().Next(1, 365)),
        //                transactionType = TransactionType.Deposit,
        //                merchantInfo = GetRandomMerchantName()
        //            };
        //            Transactions.Add(transaction);
        //        }
        //        else
        //        {
        //            var transaction = new Withdrawal()
        //            {
        //                id = i,
        //                accountId = account,
        //                amount = (decimal)(new Random().Next(0, 10000) / 100.00m),
        //                dateTime = DateTime.Now.AddDays(-new Random().Next(1, 365)),
        //                transactionType = TransactionType.Withdrawal,
        //                merchantInfo = GetRandomMerchantName()
        //            };
        //            Transactions.Add(transaction);
        //        }
        //    }
        //}


        //private string GetRandomMerchantName()
        //{
        //    string[] prefixes = { "Mama's", "Papa's", "Doctor", "Dr.", "Happy", "Healthy", "Delicious", "Fresh", "Quick", "Golden", "Tasty" };
        //    string[] foods = { "Pizza", "Burger", "Taco", "Sushi", "Noodle", "BBQ", "Salad", "Sandwich", "Pasta", "Ice Cream", "Smoothie", "Coffee", "Donut", "Bakery", "Pancake", "Fruit", "Cookie" };
        //    string[] services = { "Dental", "Massage", "Spa", "Fitness", "Gym", "Yoga", "Hair", "Barber", "Nail", "Laundry", "Cleaning", "Car Wash", "Repair", "Plumbing", "Electric", "Painting" };
        //    string[] health = { "Pharmacy", "Clinic", "Hospital", "Medical", "Wellness", "Therapy", "Nutrition", "Rehab", "Chiropractic", "Optical", "Dermatology", "Surgery", "Orthodontics", "Recovery" };

        //    Random rand = new Random();
        //    string prefix = prefixes[rand.Next(prefixes.Length)];
        //    string[] categories = { "Food", "Service", "Health" };
        //    string category = categories[rand.Next(categories.Length)];
        //    string[] items = category switch
        //    {
        //        "Food" => foods,
        //        "Service" => services,
        //        _ => health
        //    };
        //    string item = items[rand.Next(items.Length)];

        //    return $"{prefix} {item}";
        //}
