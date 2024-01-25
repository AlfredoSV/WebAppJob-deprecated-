namespace Application.IServices
{
    public interface IServiceCatalog<T>
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task Save(T entity);
        Task Delete(Guid id);
        Task Update(T entity);
    }
}
