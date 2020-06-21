using WebStore.Domain.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Mapping
{
    public static class ProductMapper
    {
        public static ProductViewModel ToViewModel( this Product product )
        {
            return new ProductViewModel
            {
                Id = product.Id,
                ImageUrl = product.ImageUrl,
                Name = product.Name,
                Order = product.Order,
                Price = product.Price
            };
        }

        public static Product ToDomain( this ProductViewModel vm )
        {
            return new Product
            {
                Id = vm.Id,
                ImageUrl = vm.ImageUrl,
                Name = vm.Name,
                Order = vm.Order,
                Price = vm.Price
            };
        }
    }
}
