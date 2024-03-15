using BankingAppBackEnd.Factories;
using BankingAppBackEnd.Services.Interfaces;
using BankingAppCore.Models.Customers;

namespace BankingAppBackEnd.Services
{
    public class CustomerDataService : ICustomerDataService
    {
        private readonly IDataService<CustomerData> _dataService;

        public CustomerDataService(IDataService<CustomerData> dataService)
        {
            _dataService = dataService;
        }

        public async Task<CustomerData> GetCustomerDataById(string id)
        {
            var customerData = await _dataService.GetById(id);
            return customerData;
        }
    }
}
