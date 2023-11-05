using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("countries")]
    public  class Country
    {
        public Guid Id { get; set; }
        public string CountryName { get; set; }
        public string CountryDescription { get; set; }
        public DateTime DateCreated { get; set; }
        public bool Active { get; set; }

    }
}
