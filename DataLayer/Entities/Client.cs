using System.ComponentModel.DataAnnotations;

namespace BeautySalonService.DataLayer.Entities
{
    public sealed class Client : BaseEntity
    {
        [Required]
        [StringLength(30)]
        public string Name { get; set; }

        [Required]
        [StringLength(30)]
        public string SureName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(30)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string MobileNumber { get; set; }

        [Required]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
