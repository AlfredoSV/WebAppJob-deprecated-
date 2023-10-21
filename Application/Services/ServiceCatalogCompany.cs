using Application.IServices;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServiceCatalogCompany : IServiceCatalog<Company>
    {
        private CatalogContext _catalogContext;

        public ServiceCatalogCompany(CatalogContext context)
        {
            _catalogContext = context;
        }

        public void Delete(Guid id)
        {
            _catalogContext.Companies.Remove(GetById(id));
            _catalogContext.SaveChanges();
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _catalogContext.Companies.ToListAsync();
        }

        public Company GetById(Guid id)
        {
            return _catalogContext.Companies.Where(ar => ar.Id == id).First();
        }

        public void Save(Company entity)
        {
            _catalogContext.Companies.Add(entity);
            _catalogContext.SaveChanges();
        }

        public void Update(Company entity)
        {
            Company com = GetById(entity.Id);
            if (com != null)
            {

                com.UpdateDate = DateTime.Now;
                com.IsActive = entity.IsActive;
                com.NameCompany = entity.NameCompany;
                com.DescriptionCompany = entity.DescriptionCompany;
                _catalogContext.Companies.Update(com);
                _catalogContext.SaveChanges();
            }
        }
    }
}
