using System.ComponentModel.DataAnnotations;

namespace WebAppJob.Models
{
    public class UserModel
    {
        [Required(ErrorMessage ="User name es required.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password es required.")]
        public string Password { get; set; }
        
    }
}
