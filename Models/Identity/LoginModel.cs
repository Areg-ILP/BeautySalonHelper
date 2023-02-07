using System.ComponentModel.DataAnnotations;

namespace BeautySalonService.Models.Identity
{
    public sealed class LoginModel
    {
        [Required]
        [EmailAddress]
        [StringLength(30, ErrorMessage = "Login lenght error!")]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
