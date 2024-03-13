﻿using BankingAppCore.Models.Customers;
using BankingApp.DTO.UI;
using BankingAppBackEnd.Utilities;
using BankingAppCore.Models.System;
using BankingAppBackEnd.Services.Interfaces;

namespace BankingAppBackEnd.Factories
{
    public class CustomerFactory
    {
        private readonly IDataService<Customer> _dataService;

        public CustomerFactory(IDataService<Customer> dataService)
        {
            _dataService = dataService;
        }

        public bool CreateNewCustomerRegistration(UserRegisterDTO newUser)
        {
            var id = UUIDGenerator.GenerateUUID();
            var customer = new Customer()
            {
                Id = id,
                CustomerType = CustomerType.Regular,
                CustomerData = new CustomerData()
                {
                    CreatedDate = DateTime.Now,
                    CustomerId = id,
                    Address = newUser.Address,
                    Email = newUser.Email,
                    Name = newUser.Name,
                    Id = UUIDGenerator.GenerateUUID()
                },
                UserCredentials = new User()
                {
                    Username = newUser.Name,
                    CustomerId = id,
                    Id = UUIDGenerator.GenerateUUID(),
                    IsAuthenticated = false,
                    Password = newUser.Password,
                    Role = Role.Customer
                }
            };

            try
            {
                _dataService.Create(customer);
                return true;
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
