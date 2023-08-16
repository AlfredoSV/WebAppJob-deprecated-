using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("experienciework")]
    public class ExperiencieWork
    {
        public Guid Id { get; set; }
        public int Years { get; set; }
        public int Months { get; set; }
        public Guid IdCompetitor { get; set; }
        public string NameCompany { get; set; }
        public string Position { get; set; }
        public string ReasonForResignation { get; set; }
        public DateTime InitDate { get; set; }
        public DateTime FinalizationDate { get; set; }
        public string NameDirectBoss { get; set; }
        public string NumberContact { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
