using System.ComponentModel.DataAnnotations;

namespace WebAppJob.Models
{
    public class ApplyJobModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string SurName { get; set; }

        [Required]
        [MaxLength(10)]
      
        public string ContactNumber { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Birthdate { get; set; }

        [Required]
        public string PresentationLetter { get; set; }

        public FormFile Cv { get; set; }


    }
}
