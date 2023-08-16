using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServicies
{
    public interface IServiceCatalog<T>
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll();
        void SaveArea(T entity);
        void DeleteArea(Guid id);
        void UpdateAre(T entity);
    }
}
