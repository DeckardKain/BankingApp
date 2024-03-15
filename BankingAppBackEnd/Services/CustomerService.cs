using BankingAppBackEnd.Factories;
using BankingAppBackEnd.Services.Interfaces;
using BankingAppCore.DTO.UI;
using BankingAppCore.Models.Customers;

namespace BankingAppBackEnd.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IDataService<Customer> _dataService;
        private readonly CustomerFactory _customerFactory;

        public CustomerService(IDataService<Customer> dataService, CustomerFactory customerFactory)
        {
            _dataService = dataService;
            _customerFactory = customerFactory;
        }

        public async Task<Customer> CreateNewCustomer(UserRegisterDTO userDTO)
        {
            // Call the CustomerFactory to create a new customer object
            var newCustomer = await _customerFactory.CreateNewCustomerRegistration(userDTO);                      

            return newCustomer;
        }

        public async Task<Customer> GetCustomerById(string id)
        {
            var customer = await _dataService.GetById(id);
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            var customers = await _dataService.GetAll();
            return customers;
        }

    }
}
