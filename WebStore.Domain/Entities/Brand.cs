using WebStore.Domain.Entities.Interfaces;

namespace WebStore.Domain.Entities
{
    public class Brand : INamedEntity, IOrderedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
    }
}
