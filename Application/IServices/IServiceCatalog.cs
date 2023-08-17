
namespace Application.IServices
{
    public interface IServiceCatalog<T>
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        void Save(T entity);
        void Delete(Guid id);
        void Update(T entity);
    }
}
