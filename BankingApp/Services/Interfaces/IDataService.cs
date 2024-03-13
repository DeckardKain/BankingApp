using BankingApp.DTO.UI;
using Microsoft.AspNetCore.Mvc;

namespace BankingApp.Services.Interfaces
{
    public interface IDataService
    {
        Task<bool> CreateNewCustomer(UserRegisterDTO userRegistration);

    }
}
