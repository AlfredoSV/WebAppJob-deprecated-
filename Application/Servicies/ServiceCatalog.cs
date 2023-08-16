using Application.IServicies;
using Domain.Entities;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Servicies
{
    public class ServiceCatalogArea : IServiceCatalog<Area>
    {
        private CatalogContext _context;

        public ServiceCatalogArea(CatalogContext catalogContext) {
            _context = catalogContext;  
        }

        public void DeleteArea(Guid id)
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

        public void SaveArea(Area entity)
        {
            _context.Areas.Add(entity);
            _context.SaveChanges();
        }

        public void UpdateAre(Area entity)
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
