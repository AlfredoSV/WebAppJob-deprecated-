using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppJob.Models
{
    public class UserForgotPasswordViewModel
    {
        [DisplayName("Username")]
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        [DisplayName("Email")]
        [Required]
        public string Email { get; set; }

    }
}
