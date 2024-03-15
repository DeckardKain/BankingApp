using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


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

        public async Task<bool> Save(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                _dbContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while adding {typeof(T).Name}: {ex.Message}");
                throw;
            }
        }

        public async Task Update(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"An error occurred while Updating {typeof(T).Name}: {ex.Message}");
                throw;
            }

        }

        public async Task Delete(T entity) 
        {
            try
            {
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting {typeof(T).Name}: {ex.Message}");
                throw;
            }
        }

        public async Task<T?> GetById(string id)
        {
            try
            {
                return await _dbSet.FindAsync(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching {typeof(T).Name} by Id : {ex.Message}");
                throw;
            }            
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                var entities = await _dbSet.ToListAsync();
                return entities;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while fetching all {typeof(T).Name} records: {ex.Message}");
                throw;
            }
        }

        public async Task<T?> FindByColumnAsync(string columnName, string value)
        {
            try
            {
                // Dynamically build expression to search for the value in the specified column
                var parameter = Expression.Parameter(typeof(T));
                var property = Expression.Property(parameter, columnName);
                var propertyValue = Expression.Constant(value);
                var predicate = Expression.Equal(property, propertyValue);
                var lambda = Expression.Lambda<Func<T, bool>>(predicate, parameter);

                // Use the dynamically created expression to filter the DbSet
                var entity = await _dbSet.FirstOrDefaultAsync(lambda);
                return entity;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while finding {typeof(T).Name} by column: {ex.Message}");
                throw;
            }
        }

    }
}
