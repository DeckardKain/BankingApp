using BankingApp.DTO.UI;
using BankingAppCore.Models.Customers;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Services.Interfaces
{
    public interface IDataService
    {
        Task<bool> CreateNewCustomer(UserRegisterDTO userRegistration);
        Task<List<Customer>> GetAllCustomers();

    }
}
