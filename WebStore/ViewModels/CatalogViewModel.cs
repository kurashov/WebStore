using System.Collections.Generic;

namespace WebStore.ViewModels
{
    public class CatalogViewModel
    {
        public CatalogViewModel(int? sectionId, int? brandId, List<ProductViewModel> products )
        {
            BrandId = brandId;
            SectionId = sectionId;
            Products = products;
        }

        public int? BrandId { get; set; }
        public int? SectionId { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}