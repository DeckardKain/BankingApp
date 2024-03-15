using BankingAppCore.Models.Customers;

namespace BankingAppBackEnd.Services.Interfaces
{
    public interface ICustomerDataService
    {
        Task<CustomerData> GetCustomerDataById(string id);
    }
}
