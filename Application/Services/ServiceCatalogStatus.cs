using Application.IServices;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;


namespace Application.Services
{
    public class ServiceCatalogStatus : IServiceCatalog<Status>
    {
        private CatalogContext _context;

        public ServiceCatalogStatus(CatalogContext catalogContext) {
            _context = catalogContext;  
        }

        public async Task Delete(Guid id)
        {
            _context.Status.Remove(await GetById(id));
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Status>> GetAllAsync()
        {
            var status  =  await _context.Status.ToListAsync();

            if (status.Count == 0)
                throw new CommonException("Table Status is empty", "GetAllAsync");

            return status;

        }

        public async Task<Status> GetById(Guid id)
        {
            Status status = await _context.Status.FirstOrDefaultAsync(ar => ar.Id == id);

            if (status == null)
                throw new CommonException("Status not exist", "GetById");

            return status;
        }

        public async Task Save(Status entity)
        {
            _context.Status.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Status entity)
        {
            Status status = await GetById(entity.Id);
            if (status != null)
            {
                status.NameStatus = entity.NameStatus;
                status.UpdateDate = DateTime.Now;
                status.IsActive = entity.IsActive;
                _context.Status.Update(status);
                await _context.SaveChangesAsync();
            }
        }
    }
}
