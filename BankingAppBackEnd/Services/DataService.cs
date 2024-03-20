using BankingAppBackEnd.Data;
using BankingAppBackEnd.Services.Interfaces;

namespace BankingAppBackEnd.Services
{
    public class DataService<T> : IDataService<T> where T : class
    {
        private readonly Repository<T> _repository;

        public DataService(BankingAppDbContext dbContext)
        {
            _repository = new Repository<T>(dbContext);
        }

        public async Task<T> Create(T entity)
        {
            if (await _repository.Save(entity))
            {
                return entity;
            }
            return entity;
        }

        public async Task Update(T entity)
        {
            await _repository.Update(entity);
        }

        public async Task Delete(T entity)
        {
            await _repository.Delete(entity);
        }

        public async Task<T> GetById(string id)
        {
            return await _repository.GetById(id);
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _repository.GetAll();
        }


        public async Task<T?> FindByColumnAsync(string columnName, string value)
        {
            return await _repository.FindByColumnAsync(columnName, value);
        }

        public async Task<IEnumerable<T>> FindAllById(string id)
        {
            return await _repository.GetAllById(id);
        }

    }
}
