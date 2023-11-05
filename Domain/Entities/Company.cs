
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("companies")]
    public class Company
    {
        public Guid Id { get; set; }
        public string NameCompany { get; set; }
        public string DescriptionCompany { get; set; }
        public Guid IdUserCreated { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }

    }
}
