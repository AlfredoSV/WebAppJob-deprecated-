using Application.IServices;
using Domain.Entities;
using Persistence.Data;


namespace Application.Services
{
    public class ServiceCatalogStatus : IServiceCatalog<Status>
    {
        private CatalogContext _context;

        public ServiceCatalogStatus(CatalogContext catalogContext) {
            _context = catalogContext;  
        }

        public void Delete(Guid id)
        {
            _context.Status.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Status> GetAll()
        {
            return _context.Status.ToList();
        }

        public Status GetById(Guid id)
        {
            return _context.Status.Where(ar => ar.Id == id).First();
        }

        public void Save(Status entity)
        {
            _context.Status.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Status entity)
        {
            Status status = GetById(entity.Id);
            if (status != null)
            {
                status.NameStatus = entity.NameStatus;
                status.UpdateDate = DateTime.Now;
                status.IsActive = entity.IsActive;
                _context.Status.Update(status);
                _context.SaveChanges();
            }
        }
    }
}
