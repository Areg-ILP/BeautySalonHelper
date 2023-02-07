using System.ComponentModel.DataAnnotations;

namespace BeautySalonService.Models.Identity
{
    public sealed class ResetPasswordModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MinLength(8)]
        public string OldPassword { get; set; }

        [Required]
        [MinLength(8)]
        public string NewPassword { get; set; }

        [Required]
        [MinLength(8)]
        public string ConfirmPassword { get; set; }

        public bool IsPasswordEqualConfirmPassword => NewPassword == ConfirmPassword;
    }
}
