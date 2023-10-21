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
        private CatalogContext _context;
        private JobContext _jobContext;

        public ServiceCatalogCompany(CatalogContext context, JobContext jobContext)
        {
            _context = context;
            _jobContext = jobContext;
        }

        public void Delete(Guid id)
        {
            _jobContext.Companies.Remove(GetById(id));
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Company>> GetAllAsync()
        {
            return await _jobContext.Companies.ToListAsync();
        }

        public Company GetById(Guid id)
        {
            return _jobContext.Companies.Where(ar => ar.Id == id).First();
        }

        public void Save(Company entity)
        {
            _jobContext.Companies.Add(entity);
            _context.SaveChanges();
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
                _jobContext.Companies.Update(com);
                _context.SaveChanges();
            }
        }
    }
}
