using BankingApp.Data;
using BankingApp.Models.Accounts;
using BankingApp.Models.Customers;
using BankingApp.Models.System;
using BankingApp.Models.Transactions;
using BankingApp.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;

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
        private readonly BankingAppDbContext _dbContext;
        private readonly IObserver _observer;
        private readonly ILogger _logger;
        public Customer? Customer;
        public List<Account>? Accounts = new List<Account>();
        public List<Transaction>? Transactions = new List<Transaction>();

        public DataService(BankingAppDbContext dbcontext, IObserver observer, ILogger<DataService> logger)
        {
            _dbContext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            _observer = observer ?? throw new ArgumentNullException(nameof(observer));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void AddNewCustomer(Customer customer)
        {
            try
            {
                _dbContext.Customers.Add(customer);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new customer.");
                throw; // Re-throw the exception for the caller to handle.
            }
            
        }

        public void AddNewTransaction(Transaction transaction)
        {
            try
            {
                _dbContext.Transactions.Add(transaction);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error occurred while adding a new transaction. Error message: {ex.Message} Stack Trace: {ex.StackTrace}");
                throw; // Re-throw the exception for the caller to handle.
            }
            
        }

        public List<Account> GetAllAccountsByCustomerId(int id)
        {
            try
            {
                var accounts = _dbContext.Accounts.Where(x => x.customerId == id).ToList();
                return accounts;
            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.Message}");
                return null;
            }            
        }

        public List<Customer> GetAllCustomers()
        {
            var customers = _dbContext.Customers.ToList();
            return customers;
        }

        public List<Transaction> GetAllTransactionsByAccountId(string id)
        {
            var transactions = Transactions.Where(x => x.accountId == id).ToList();
            return transactions;
        }

        public Customer GetCurrentCustomer()
        {
            return Customer;
        }

        public List<Account> GetCustomerAccounts()
        {
            return Accounts;
        }

        public bool SeedData()
        {
            bool result = false;
            string message = "Data Service: Data was not seeded.";

            if (Accounts.IsNullOrEmpty())
            {
                Accounts =
                [
                    new Chequing {
                    id = 1,
                    accountId = "c1 152 34FD 2134",
                    accountType = AccountType.Chequing,
                    balance = 0,
                    customerId = 1
                },
                new Savings
                {
                    id = 1,
                    accountId = "s4 578 65SD 2357",
                    accountType = AccountType.Savings,
                    balance = 0,
                    customerId = 1
                },
                new Retirement
                {
                    id = 1,
                    accountId = "r2 567 234D 7677",
                    accountType = AccountType.Retirement,
                    balance = 0,
                    customerId = 1
                }
                ];

                message = "SeedData: Account data seeded success.";

                Customer = new Customer
                {
                    Id = 1,
                    Accounts = Accounts,
                    Username = "MMoney",
                    Password = "password",
                    CustomerData = new CustomerData()
                    {
                        Id = 1,
                        Name = "Moe Money",
                        Address = "123 Fake Street",
                        CustomerId = 1,
                        Email = "BigMoney@gmail.com",
                        PhoneNumber = "403-555-1234"
                    }
                };

                message += "\nSeedData: Customer data seeded success.";

                CreateRandomTransactions("c1 152 34FD 2134", 20);
                CreateRandomTransactions("s4 578 65SD 2357", 20);
                CreateRandomTransactions("r2 567 234D 7677", 20);

                message += "\nSeedData: Transaction data seeded success.";

                CalculateAccountBalances();

                message += "\nSeedData: Calculate account balances success";

                result = true;
            }
            else
            {
                message = "Data Service: No need to seed data as it has already been completed";
            }

            var state = new StateChangeDTO()
            {
                Message = message
            };

            _observer.Notify(state);

            return result;
        }

        private void CalculateAccountBalances()
        {
            foreach (var account in Accounts)
            {
                var debits = Transactions.Where(x => x.accountId == account.accountId)
                    .Where(y => y.transactionType == TransactionType.Withdrawal).Sum(z => z.amount);
                var credits = Transactions.Where(x => x.accountId == account.accountId)
                    .Where(y => y.transactionType == TransactionType.Deposit).Sum(z => z.amount);

                account.balance = credits - debits;
            }
        }

        public void CreateRandomTransactions(string account, int amount)
        {
            for (int i = 0; i < amount; i++)
            {

                if (new Random().NextDouble() < 0.5)
                {
                    var transaction = new Deposit()
                    {
                        id = i,
                        accountId = account,
                        amount = (decimal)(new Random().Next(0, 10000) / 100.00m),
                        dateTime = DateTime.Now.AddDays(-new Random().Next(1, 365)),
                        transactionType = TransactionType.Deposit,
                        merchantInfo = GetRandomMerchantName()
                    };
                    Transactions.Add(transaction);
                }
                else
                {
                    var transaction = new Withdrawal()
                    {
                        id = i,
                        accountId = account,
                        amount = (decimal)(new Random().Next(0, 10000) / 100.00m),
                        dateTime = DateTime.Now.AddDays(-new Random().Next(1, 365)),
                        transactionType = TransactionType.Withdrawal,
                        merchantInfo = GetRandomMerchantName()
                    };
                    Transactions.Add(transaction);
                }
            }
        }


        private string GetRandomMerchantName()
        {
            string[] prefixes = { "Mama's", "Papa's", "Doctor", "Dr.", "Happy", "Healthy", "Delicious", "Fresh", "Quick", "Golden", "Tasty" };
            string[] foods = { "Pizza", "Burger", "Taco", "Sushi", "Noodle", "BBQ", "Salad", "Sandwich", "Pasta", "Ice Cream", "Smoothie", "Coffee", "Donut", "Bakery", "Pancake", "Fruit", "Cookie" };
            string[] services = { "Dental", "Massage", "Spa", "Fitness", "Gym", "Yoga", "Hair", "Barber", "Nail", "Laundry", "Cleaning", "Car Wash", "Repair", "Plumbing", "Electric", "Painting" };
            string[] health = { "Pharmacy", "Clinic", "Hospital", "Medical", "Wellness", "Therapy", "Nutrition", "Rehab", "Chiropractic", "Optical", "Dermatology", "Surgery", "Orthodontics", "Recovery" };

            Random rand = new Random();
            string prefix = prefixes[rand.Next(prefixes.Length)];
            string[] categories = { "Food", "Service", "Health" };
            string category = categories[rand.Next(categories.Length)];
            string[] items = category switch
            {
                "Food" => foods,
                "Service" => services,
                _ => health
            };
            string item = items[rand.Next(items.Length)];

            return $"{prefix} {item}";
        }
    }
}
