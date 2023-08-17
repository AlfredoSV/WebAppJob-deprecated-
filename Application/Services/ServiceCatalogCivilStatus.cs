using Application.IServices;
using Domain.Entities;
using Persistence.Data;


namespace Application.Services
{
    public class ServiceCatalogCivilStatus : IServiceCatalog<CivilStatus>
    {
        private CatalogContext _context;

        public ServiceCatalogCivilStatus(CatalogContext catalogContext) {
            _context = catalogContext;  
        }

        public void Delete(Guid id)
        {
            _context.CivilStatus.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<CivilStatus> GetAll()
        {
            return _context.CivilStatus
                           .ToList();
        }

        public CivilStatus GetById(Guid id)
        {
            return _context.CivilStatus
                           .Where(ar => ar.Id == id)
                           .First();
        }

        public void Save(CivilStatus entity)
        {
            _context.CivilStatus.Add(entity);
            _context.SaveChanges();
        }

        public void Update(CivilStatus entity)
        {
            CivilStatus civilStatus = GetById(entity.Id);
            if (civilStatus != null)
            {
                civilStatus.NameStatus = entity.NameStatus;
                civilStatus.UpdateDate = DateTime.Now;
                civilStatus.IsActive = entity.IsActive;
                _context.CivilStatus
                        .Update(civilStatus);
                _context.SaveChanges();
            }
        }
    }
}
