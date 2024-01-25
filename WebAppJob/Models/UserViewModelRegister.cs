using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppJob.Models
{
    public class UserViewModelRegister
    {
        [Required]
        [DisplayName("user Name")]
        public string UserName { get;  set; }
        [Required]
        public string Password { get;  set; }
        [Compare("Password")]
        [Required]
        public string ConfirmPassword { get;  set; }
        public DateTime DateCreated { get;  set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get;  set; }
        [Required]
        public int Age { get;  set; }
        public DateTime Birthdate { get; set; }

        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
