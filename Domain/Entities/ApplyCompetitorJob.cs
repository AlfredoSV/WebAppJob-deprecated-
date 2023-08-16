
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("applycompetitorjob")]
    public class ApplyCompetitorJob
    {
        public Guid Id { get; set; }
        public Guid IdCompetitor { get; set; }
        public Guid IdJob { get; set; }
        public Guid IdStatus { get; set; }
        public DateTime DateApply { get; set; }
        public Guid IdUserCreated { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }
    }
}
