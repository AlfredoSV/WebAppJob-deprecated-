using Application.IServices;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Services
{
    public class ServiceCatalogArea : IServiceCatalog<Area>
    {
        private CatalogContext _context;

        public ServiceCatalogArea(CatalogContext catalogContext)
        {
            _context = catalogContext;
        }

        public async void Delete(Guid id)
        {
            _context.Areas.Remove(await GetById(id));
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Area>> GetAllAsync()
        {

            var catArea = await _context.Areas.ToListAsync();

            if (catArea.Count == 0)
                throw new CommonException("Table Areas is noy values", "GetAllAsync");

            return catArea;
        }

        public async Task<Area> GetById(Guid id)
        {
            Area area = await _context.Areas.FirstOrDefaultAsync(ar => ar.Id.Equals(id));

            if (area == null)
                throw new CommonException("Area not exist", "GetById");

            return area;
        }

        public void Save(Area entity)
        {
            _context.Areas.Add(entity);
            _context.SaveChanges();
        }

        public async void Update(Area entity)
        {
            Area aree = await GetById(entity.Id);
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
