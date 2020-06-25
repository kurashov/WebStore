using WebStore.Domain.Entities;

namespace WebStore.ViewModels
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            
        }
        public ProductViewModel( Product product )
        {
            Id = product.Id;
            Name = product.Name;
            Order = product.Order;
            ImageUrl = product.ImageUrl;
            Price = product.Price;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Order { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
    }
}