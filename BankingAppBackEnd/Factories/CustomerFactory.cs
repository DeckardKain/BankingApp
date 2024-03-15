﻿using BankingAppCore.Models.Customers;
using BankingAppBackEnd.Utilities;
using BankingAppCore.Models.System;
using BankingAppBackEnd.Services.Interfaces;
using BankingAppCore.DTO.UI;

namespace BankingAppBackEnd.Factories
{
    public class CustomerFactory
    {
        private readonly IDataService<Customer> _customerDataService;
        private readonly IDataService<CustomerData> _customerDataDataService;
        private readonly IDataService<User> _userDataService;

        public CustomerFactory(IDataService<Customer> customerDataService, IDataService<CustomerData> customerDataDataService,
            IDataService<User> userDataService)
        {
            _customerDataService = customerDataService;
            _customerDataDataService = customerDataDataService;
            _userDataService = userDataService;
        }

        public async Task<Customer> CreateNewCustomerRegistration(UserRegisterDTO newUser)
        {
            var id = UUIDGenerator.GenerateUUID();
            var CustomerDataId = UUIDGenerator.GenerateUUID();
            var UserId = UUIDGenerator.GenerateUUID();            

            var customerData = new CustomerData()
            {
                CreatedDate = DateTime.Now,
                Address = newUser.Address,
                PhoneNumber = newUser.PhoneNumber,
                Email = newUser.Email,
                Name = newUser.Name,
                Id = CustomerDataId,
                CustomerId = id,
            };

            await _customerDataDataService.Create(customerData);

            var user = new User()
            {
                Username = newUser.Name,
                Id = UserId,
                IsAuthenticated = false,
                Password = newUser.Password,
                Role = Role.Customer,
                CustomerId = id,
            };

            await _userDataService.Create(user);

            var customer = new Customer()
            {
                Id = id,
                CustomerType = CustomerType.Regular,
                CustomerDataId = CustomerDataId,
                UserCredentialsId = UserId
            };

            try
            {
                await _customerDataService.Create(customer);
                return customer;
            }
            catch (Exception ex) 
            {
                return customer;
            }
        }
    }
}
