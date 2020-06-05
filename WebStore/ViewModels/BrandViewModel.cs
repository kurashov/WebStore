using WebStore.Domain.Entities;

namespace WebStore.ViewModels
{
    public class BrandViewModel
    {
        public BrandViewModel(Brand brand)
        {
            Id = brand.Id;
            Name = brand.Name;
            Order = brand.Order;

            ProductsCount = 0;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }

        public int ProductsCount { get; set; }
    }
}