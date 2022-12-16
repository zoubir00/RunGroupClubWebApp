using System.ComponentModel.DataAnnotations;

namespace RunGroupClubWebApp.ViewModel
{
    public class RegisterViewModel
    {
        [Display(Name = "Email Address")]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public string? Password { get; set; }
        [Required]
        [Compare("Password",ErrorMessage ="wrong password")]
        public string? ConfirmPassword { get; set; }

    }
}
