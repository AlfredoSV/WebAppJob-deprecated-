using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppJob.Models
{
    public class UserForgotPasswordViewModel
    {

        public Guid Id { get; set; }

        [DisplayName("Password")]
        [Required]
        public string Password { get; set; }
        [EmailAddress]
        [DisplayName("Confirm Password")]
        [Required]
        public string ConfirmPassword { get; set; }

    }
}
