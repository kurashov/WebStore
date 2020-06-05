namespace WebStore.Domain.Entities.Interfaces
{
    interface INamedEntity : IBaseEntity
    {
        string Name { get; set; }
    }
}
