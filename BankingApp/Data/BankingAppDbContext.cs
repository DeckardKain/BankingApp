using BankingAppCore.Models.Accounts;
using BankingAppCore.Models.Customers;
using BankingAppCore.Models.Transactions;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Data
{
    public class BankingAppDbContext : DbContext
    {
        public BankingAppDbContext(DbContextOptions<BankingAppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        internal DbSet<Account> Accounts { get; set; }
        internal DbSet<Transaction> Transactions { get; set; }

    }
}
