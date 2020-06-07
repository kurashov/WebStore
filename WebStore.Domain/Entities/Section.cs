using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebStore.Domain.Entities.Interfaces;

namespace WebStore.Domain.Entities
{
    public class Section : INamedEntity, IOrderedEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Order { get; set; }

        public int? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Section ParentSection { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
