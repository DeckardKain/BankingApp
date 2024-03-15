using Microsoft.AspNetCore.Mvc;
using BankingAppBackEnd.Services.Interfaces;
using BankingAppCore.DTO.UI;
using BankingAppCore.DTO.Customer;
using BankingAppCore.DTO.Account;

namespace BankingAppBackEnd.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly ICustomerService _customerService;
        private readonly ICustomerDataService _customerDataService;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public HomeController(IHttpClientFactory httpClientFactory, ICustomerService customerService,
            ICustomerDataService customerDataService, IUserService userService,
            IAccountService accountService)
        {
            _httpClient = httpClientFactory.CreateClient("BankingAppFrontEnd");
            _customerService = customerService;
            _customerDataService = customerDataService;
            _userService = userService;
            _accountService = accountService;
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

        [HttpGet("api/getallaccountsbyid")]
        public async Task<ActionResult<List<AccountDTO>>> GetAllAccountsById([FromBody]string id)
        {
            var accountsDTO = new List<AccountDTO>();
            var accounts = await _accountService.GetAllAccountsById(id);

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
    }
}
