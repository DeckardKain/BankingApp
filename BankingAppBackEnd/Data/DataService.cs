using BankingApp.Data;

namespace BankingAppBackEnd.Data
{
    public class DataService<T> where T : class
    {
        private readonly Repository<T> _repository;

        public DataService(BankingAppDbContext dbContext)
        {
            _repository = new Repository<T>(dbContext);            
        }

        public bool Create(T entity)
        {
            if (_repository.Save(entity))
            {
                return true;
            }           
            return false;
        }

        public void Update(T entity)
        {
            _repository.Update(entity);
        }

        public void Delete(T entity)
        {
            _repository.Delete(entity);
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }
    }
}
