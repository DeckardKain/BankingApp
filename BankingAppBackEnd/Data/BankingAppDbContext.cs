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
        public DbSet<Chequing> ChequingAccounts { get; set; }
        public DbSet<Savings> SavingsAccounts { get; set; }
        public DbSet<Retirement> RetirementAccounts { get; set; }
        public DbSet<Deposit> Deposits { get; set; }
        public DbSet<Withdrawal> Withdrawals { get; set; }
        public DbSet<User> Users { get; set; }
    
    }
}
