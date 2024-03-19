using Microsoft.AspNetCore.Mvc;
using BankingAppBackEnd.Services.Interfaces;
using BankingAppCore.DTO.UI;
using BankingAppCore.DTO.Customer;
using BankingAppCore.DTO.Account;
using Azure.Core;
using BankingAppCore.DTO.Transaction;

namespace BankingAppBackEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ICustomerService _customerService;
        private readonly ICustomerDataService _customerDataService;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public HomeController(IHttpClientFactory httpClientFactory, ICustomerService customerService,
            ICustomerDataService customerDataService, IUserService userService,
            IAccountService accountService, ITransactionService transactionService)
        {
            _httpClient = httpClientFactory.CreateClient("BankingAppFrontEnd");
            _customerService = customerService;
            _customerDataService = customerDataService;
            _userService = userService;
            _accountService = accountService;
            _transactionService = transactionService;
        }

        [HttpPost("api/userregisterdto")]
        public IActionResult HandleNewUserPostRequest([FromBody] UserRegisterDTO data)
        {
            try
            {
                var userDTO = data;
                if (userDTO == null)
                {
                    return BadRequest("Invalid JSON payload.");
                }
                // Does ModelState contain any errors or validation failures?
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                // Create new customer

                var newCustomer = _customerService.CreateNewCustomer(userDTO);

                return Ok();


            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message} Stack Trace: {ex.StackTrace}");

                return StatusCode(500, "An error occured while processing the request.");
            }
        }

        [HttpGet("api/getallcustomers")]
        public async Task<IActionResult> GetAllCustomeres()
        {
            List<AllCustomersDTO> customerDTOs = new List<AllCustomersDTO>();

            try
            {
                var customers = await _customerService.GetAllCustomers();

                if (customers == null || !customers.Any())
                {
                    return NoContent(); // If no customers found, return 204 No Content
                }

                foreach (var customer in customers)
                {
                    var customerDTO = new AllCustomersDTO()
                    {
                        Id = customer.Id,
                        CustomerData = await _customerDataService.GetCustomerDataById(customer.CustomerDataId),
                        CustomerType = customer.CustomerType.Value,
                        UserCredentials = await _userService.GetUserById(customer.UserCredentialsId)
                    };

                    customerDTOs.Add(customerDTO);
                }

                return Ok(customerDTOs);
            }
            catch (Exception ex)
            {
                // Return 500 Internal Server Error
                return StatusCode(500, "An error occurred while retrieving customers.");
            }
        }


        [HttpPost("api/authenticate")]
        public async Task<ActionResult<UserLoginDTO>> AuthenticateUser([FromBody]UserLoginDTO userLoginDTO)
        {
            var authenicationResult = await _userService.AuthenticateUser(userLoginDTO);
            return authenicationResult;
        }

        [HttpPost("api/newaccount")]
        public async Task<ActionResult<AccountDTO>> CreateNewAccount([FromBody]AccountDTO accountDTO)
        {
            var newAccountResult = await _accountService.CreateNewAccount(accountDTO);
            return Ok(newAccountResult);
        }

        [HttpGet("api/getallaccountsbyid/{userId}")]
        public async Task<IActionResult> GetAllAccountsById(string userId)
        {
            var accountsDTO = new List<AccountDTO>();
            var customer = await _userService.GetUserById(userId);
            var accounts = await _accountService.GetAllAccountsById(customer.CustomerId);

            foreach (var account in accounts)
            {
                var acc = new AccountDTO()
                {
                    AccountType = account.AccountType,
                    Balance = account.Balance,
                    CustomerId = account.CustomerId,
                    Id = account.Id
                };
                accountsDTO.Add(acc);
            }
            return Ok(accountsDTO);
        }

        [HttpGet("api/getcustomerbyid/{userId}")]
        public async Task<IActionResult> GetCustomerIdFromUserId(string userId)
        {            
            var customer = await _userService.GetUserById(userId);
            return Ok(customer.CustomerId);            
        }

        [HttpGet("api/getaccounttransactions")]
        public async Task<IActionResult> GetAccountTransactions([FromBody] string accountId)
        {
            var transactionDTOs = new List<TransactionDTO>();

            try
            {
                if (accountId == null || string.IsNullOrWhiteSpace(accountId))
                {
                    return BadRequest("Account number is required.");
                }

                var transactions = await _transactionService.GetAllTransactionsById(accountId);

                foreach(var transaction in transactions)
                {
                    var trans = new TransactionDTO()
                    {
                        AccountId = transaction.AccountId,
                        DateTime = transaction.DateTime,
                        Amount = transaction.Amount,
                        Id = transaction.Id,
                        MerchantInfo = transaction.MerchantInfo,
                        TransactionType = transaction.TransactionType
                    };
                    transactionDTOs.Add(trans);
                }

                return Ok(transactionDTOs);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while processing the request.");
            }
        }
    }
}
