using Framework.Security2023.Entities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebAppJob.Models;

public class UserViewModelRegister
{
    [Required]
    [DisplayName("User Name")]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    [Compare("Password")]
    [DisplayName("Confirm Password")]
    [Required]
    public string ConfirmPassword { get; set; }

    public DateTime DateCreated { get; set; }

    [Required]
    public string Name { get; set; }

    [DisplayName("Last Name")]
    [Required]
    public string LastName { get; set; }

    [Required]
    public int Age { get; set; }

    [DisplayName("Birth Date")]
    [Required]
    public DateTime Birthdate { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; }

    [Required]
    public List<Role> Roles { get; set; }
}
