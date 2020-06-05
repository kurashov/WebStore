namespace WebStore.Domain.Entities.Interfaces
{
    interface IOrderedEntity: IBaseEntity
    {
        int Order { get; set; }
    }
}
