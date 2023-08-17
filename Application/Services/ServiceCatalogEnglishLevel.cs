using Application.IServices;
using Domain.Entities;
using Persistence.Data;


namespace Application.Services
{
    public class ServiceCatalogEnglishLevel : IServiceCatalog<EnglishLevel>
    {
        private CatalogContext _context;

        public ServiceCatalogEnglishLevel(CatalogContext catalogContext) {
            _context = catalogContext;  
        }

        public void Delete(Guid id)
        {
            _context.EnglishLevel.Remove(GetById(id));
            _context.SaveChanges();
        }

        public IEnumerable<EnglishLevel> GetAll()
        {
            return _context.EnglishLevel
                           .ToList();
        }

        public EnglishLevel GetById(Guid id)
        {
            return _context.EnglishLevel
                           .Where(ar => ar.Id == id)
                           .First();
        }

        public void Save(EnglishLevel entity)
        {
            _context.EnglishLevel.Add(entity);
            _context.SaveChanges();
        }

        public void Update(EnglishLevel entity)
        {
            EnglishLevel englishLevel = GetById(entity.Id);
            if (englishLevel != null)
            {
                englishLevel.NameLevel = entity.NameLevel;
                englishLevel.UpdateDate = DateTime.Now;
                englishLevel.IsActive = entity.IsActive;
                _context.EnglishLevel
                        .Update(englishLevel);
                _context.SaveChanges();
            }
        }
    }
}
