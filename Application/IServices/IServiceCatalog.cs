
namespace Application.IServices
{
    public interface IServiceCatalog<T>
    {
        Task<T> GetById(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        void Save(T entity);
        void Delete(Guid id);
        void Update(T entity);
    }
}
