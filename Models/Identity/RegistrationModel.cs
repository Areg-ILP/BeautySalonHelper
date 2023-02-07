using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BeautySalonService.Models.Identity
{
    public sealed class RegistrationModel
    {
        [Required]
        [EmailAddress]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string SureName { get; set; }

        [Phone]
        [StringLength(9)]
        public string MobileNumber { get; set; }

        [Required]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [MinLength(8)]
        public string ConfirmPassword { get; set; }

        public bool IsPasswordEqualConfirmPassword => Password == ConfirmPassword;
        public bool? IsMobileNumberValid => !MobileNumber?.Any(char.IsLetter);
    }
}
