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
                return false;
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

            }

            return null;
            
        }


    }
}
