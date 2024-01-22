
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    [Table("applycompetitorjobs")]
    public class ApplyCompetitorJob
    {
        public Guid Id { get; set; }
        public DateTime DateApply { get; set; }
        public Guid IdUserCreated { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsActive { get; set; }

        //Relaciones
        public Guid IdCompetitor { get; set; }
        public Guid IdJob { get; set; }
        public Guid IdStatus { get; set; }
        
        public Competitor Competitor { get; set; }
        public Job Job { get; set; }
        public Status Status { get; set; }
       
        private ApplyCompetitorJob(Guid idCompetitor, Guid idJob, 
            Guid idStatus, Guid idUserCreated)
        {
            Id = Guid.NewGuid();
            IdCompetitor = idCompetitor;
            IdJob = idJob;
            IdStatus = idStatus;
            DateApply = DateTime.Now;
            IdUserCreated = idUserCreated;
            UpdateDate = DateTime.Now;
            CreateDate = DateTime.Now;
            IsActive = true;
        }

        public static ApplyCompetitorJob Create(Guid idCompetitor, Guid idJob,
            Guid idStatus, Guid idUserCreated)
        {
            return new ApplyCompetitorJob(idCompetitor,  idJob,
             idStatus, idUserCreated);
        }
    }
}
