using Application.IServices;
using Domain.Entities;
using Persistence.Data;

namespace Application.Services
{
    public class ServiceCatalogArea : IServiceCatalog<Area>
    {
        private CatalogContext _context;

        public ServiceCatalogArea(CatalogContext catalogContext) {
            _context = catalogContext;  
        }

        public void Delete(Guid id)
        {
            _context.Areas.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<Area> GetAll()
        {
            return _context.Areas.ToList();
        }

        public Area GetById(Guid id)
        {
            return _context.Areas.Where(ar => ar.Id == id).First();
        }

        public void Save(Area entity)
        {
            _context.Areas.Add(entity);
            _context.SaveChanges();
        }

        public void Update(Area entity)
        {
            Area aree = GetById(entity.Id);
            if (aree != null)
            {
                aree.DescriptionArea = entity.DescriptionArea;
                aree.UpdateDate = DateTime.Now;
                aree.IsActive = entity.IsActive;
                aree.NameArea = entity.NameArea;
                _context.Areas.Update(aree);
                _context.SaveChanges();
            }
        }
    }
}
