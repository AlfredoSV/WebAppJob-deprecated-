using Application.IServices;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Services
{
    public class ServiceCatalogCivilStatus : IServiceCatalog<CivilStatus>
    {
        private CatalogContext _context;

        public ServiceCatalogCivilStatus(CatalogContext catalogContext) {
            _context = catalogContext;  
        }

        public async void Delete(Guid id)
        {
            _context.CivilStatus.Remove(await GetById(id));
            _context.SaveChanges();
        }

        public async Task<IEnumerable<CivilStatus>> GetAllAsync()
        {
            var catCivilStatis = await _context.CivilStatus
                           .ToListAsync();

            if (catCivilStatis.Count == 0)
                throw new CommonException("Table CivilStatus is not values", "GetAllAsync");

            return catCivilStatis;
        }

        public async Task<CivilStatus> GetById(Guid id)
        {

            CivilStatus civilStatus = await  _context.CivilStatus.
                FirstOrDefaultAsync(ar => ar.Id.Equals(id));

            if (civilStatus == null)
                throw new CommonException("Civil Status not exist", "GetById");

            return civilStatus; 
        }

        public void Save(CivilStatus entity)
        {
            _context.CivilStatus.Add(entity);
            _context.SaveChanges();
        }

        public async void Update(CivilStatus entity)
        {
            CivilStatus civilStatus = await GetById(entity.Id);
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
