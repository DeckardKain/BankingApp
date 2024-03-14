using BankingAppCore.Models.Customers;
using Microsoft.EntityFrameworkCore;


namespace BankingAppBackEnd.Data
{
    public class Repository<T> where T : class
    {
        private readonly BankingAppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(BankingAppDbContext dbcontext)
        {
            _dbContext = dbcontext ?? throw new ArgumentNullException(nameof(dbcontext));
            _dbSet = _dbContext.Set<T>();
        }

        public bool Save(T entity)
        {
            try
            {
                _dbSet.Add(entity);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while adding {typeof(T).Name}: {ex.Message}");
                throw;
            }
        }

        public void Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                _dbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while Updating {typeof(T).Name}: {ex.Message}");
                throw;
            }

        }

        public void Delete(T entity) 
        {
            try
            {
                _dbSet.Remove(entity);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting {typeof(T).Name}: {ex.Message}");
                throw;
            }

        }

        public T? GetById(int id)
        {
            try
            {
                return _dbSet.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching {typeof(T).Name} by Id : {ex.Message}");
                throw;
            }            
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                if(typeof(T) == typeof(Customer))
                {
                    return _dbSet.Include("CustomerData").ToList();
                }
                
                return _dbSet.ToList();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching all {typeof(T).Name} records: {ex.Message}");
                throw;
            }
        }

    }
}
