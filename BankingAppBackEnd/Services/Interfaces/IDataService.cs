namespace BankingAppBackEnd.Services.Interfaces
{
    public interface IDataService<T>
    {
        public bool Create(T entity);
        public void Update(T entity);
        public void Delete(T entity);
        public T GetById(int id);

    }
}
