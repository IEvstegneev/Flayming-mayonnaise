namespace FuelAccounting.DataAccess
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> FindAsync(Guid id);
        Task<T> AddAsync(T item);
        Task UpdateAsync(Guid id, T item);
        Task DeleteAsync(Guid id);
    }
}