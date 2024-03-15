namespace BankingAppBackEnd.Services.Interfaces
{
    public interface IDataService<T>
    {
        Task<T> Create(T entity);
        Task Update(T entity);
        Task Delete(T entity);
        Task<T> GetById(string id);
        Task<IEnumerable<T>> GetAll();
        Task<T?> FindByColumnAsync(string columnName, string value);
    }
}
