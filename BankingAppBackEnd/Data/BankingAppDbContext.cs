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


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            // Configure a 1 to 1 relationship with Customer and CustomerData
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.CustomerData)
                .WithOne(cd => cd.Customer)
                .HasForeignKey<CustomerData>(cd => cd.Id)
                .IsRequired();

            // Configure one to many relationship between Customer and Account
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Accounts)
                .WithOne(a => a.Customer)
                .IsRequired();

            // Configure one to one relationship between Customer and User
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.UserCredentials)
                .WithOne(cd => cd.Customer)
                .IsRequired();
            
            // Configure many to one relationship between Account and Customer
            modelBuilder.Entity<Account>()
                .HasOne(a => a.Customer)
                .WithMany(a => a.Accounts)
                .IsRequired();


            // Configure one to many relationship between Account and Transaction
            modelBuilder.Entity<Account>()
                .HasMany(t => t.Transactions)
                .WithOne(a => a.Account)
                .IsRequired();

            // Configure many to one relationship between Transaction and Account
            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.Account)
                .WithMany(a => a.Transactions)
                .IsRequired();

            // Configure one to one relationship between User and Customer
            modelBuilder.Entity<User>()
                .HasOne(u => u.Customer)
                .WithOne(u => u.UserCredentials)
                .IsRequired();

        }

    }
}
