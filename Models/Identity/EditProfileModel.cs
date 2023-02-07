using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BeautySalonService.Models.Identity
{
    public sealed class EditProfileModel
    {
        [Required]
        public int Id { get; set; }

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

        [StringLength(30)]
        public string MobileNumber { get; set; }
    }
}
