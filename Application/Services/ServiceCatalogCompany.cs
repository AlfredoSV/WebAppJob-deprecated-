using Application.IServices;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Application.Services
{
    public class ServiceCatalogCompany : IServiceCatalog<Company>
    {
        private CatalogContext _catalogContext;

        public ServiceCatalogCompany(CatalogContext context)
        {
            _catalogContext = context;
        }

        public async Task Delete(Guid id)
        {
            _catalogContext.Companies.Remove(await GetById(id));
            await _catalogContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {

            var companies = await _catalogContext.Companies.ToListAsync();

            if (companies.Count == 0)
                throw new CommonException("Table Companies is empty", "GetAllAsync");

            return companies;

        }

        public async Task<Company> GetById(Guid id)
        {
            Company company = await _catalogContext.Companies.FirstOrDefaultAsync(ar => ar.Id.Equals(id));

            if(company == null)
                throw new CommonException("Company not exist", "GetById");

            return company;
        }

        public async Task Save(Company entity)
        {
            _catalogContext.Companies.Add(entity);
            await _catalogContext.SaveChangesAsync();
        }

        public async Task Update(Company entity)
        {
            Company com = await GetById(entity.Id);
            if (com != null)
            {

                com.UpdateDate = DateTime.Now;
                com.IsActive = entity.IsActive;
                com.NameCompany = entity.NameCompany;
                com.DescriptionCompany = entity.DescriptionCompany;
                _catalogContext.Companies.Update(com);
                await _catalogContext.SaveChangesAsync();
            }
        }
    }
}
