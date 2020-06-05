using WebStore.Domain.Entities.Interfaces;

namespace WebStore.Domain.Entities
{
    public class Section : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public int? ParentId { get; set; }
    }
}
