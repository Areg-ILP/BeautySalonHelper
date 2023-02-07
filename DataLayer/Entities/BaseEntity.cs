using System;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonService.DataLayer.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        [Required]
        public int Id { get; private set; }

        [Required]
        public DateTime CreationDate { get; private set; } = DateTime.Now;
    }
}
