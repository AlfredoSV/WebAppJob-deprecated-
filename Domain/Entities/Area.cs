using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("Area")]
    public class Area
    {
        public Guid Id { get; set; }
        public string NameArea { get; set; }
        public string DescriptionArea { get; set; }
        public Guid IdUserCreated { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
