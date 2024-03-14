namespace BankingAppBackEnd.Services.Interfaces
{
    public interface IDataService<T>
    {
        bool Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();

    }
}
