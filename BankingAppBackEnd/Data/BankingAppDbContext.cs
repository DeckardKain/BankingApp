using BankingAppBackEnd.Factories;
using BankingAppBackEnd.Services.Interfaces;
using BankingAppCore.Models.Accounts;
using BankingAppCore.Models.Customers;
using BankingAppCore.Models.System;
using BankingAppCore.Models.Transactions;
using Microsoft.EntityFrameworkCore;

namespace BankingAppBackEnd.Data
{
    public class BankingAppDbContext : DbContext
    {
        public BankingAppDbContext(DbContextOptions<BankingAppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerData> CustomersData { get; set; }
        public DbSet<Chequing> ChequingAccounts { get; set; }
        public DbSet<Savings> SavingsAccounts { get; set; }
        public DbSet<Retirement> RetirementAccounts { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
        public DbSet<User> Users { get; set; }

        public async Task SeedInitialData(IServiceProvider serviceProvider)
        {
            // Resolve the required services
            var customerDataService = serviceProvider.GetRequiredService<IDataService<Customer>>();
            var customerDataDataService = serviceProvider.GetRequiredService<IDataService<CustomerData>>();
            var userDataService = serviceProvider.GetRequiredService<IDataService<User>>();
            var accountDataService = serviceProvider.GetRequiredService<IDataService<Account>>();

            // Create an instance of CustomerFactory and pass in the services
            var customerFactory = new CustomerFactory(customerDataService, customerDataDataService, userDataService);

            // CustomerFactory object to create customers
            await customerFactory.CreateCustomers(10);

            var accountFactory = new AccountFactory(accountDataService, customerDataService);

            await accountFactory.CreateAccounts();

        }
 



    }
}
