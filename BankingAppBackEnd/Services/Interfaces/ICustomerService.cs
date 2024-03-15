using BankingAppCore.DTO.UI;
using BankingAppCore.Models.Customers;

namespace BankingAppBackEnd.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> CreateNewCustomer(UserRegisterDTO userDTO);
        Task<Customer> GetCustomerById(string id);
        Task<IEnumerable<Customer>> GetAllCustomers();

    }
}
