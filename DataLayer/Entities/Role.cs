using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeautySalonService.DataLayer.Entities
{
    public sealed class Role : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public ICollection<Client> Clients { get; set; }
    }
}
