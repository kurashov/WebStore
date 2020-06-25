namespace WebStore.Domain.Entities
{
    public class ProductFilter
    {
        public ProductFilter()
        {
            
        }

        public ProductFilter( int? sectionId, int? brandId )
        {
            SectionId = sectionId;
            BrandId = brandId;
        }

        public int? SectionId { get; set; }
        public int? BrandId { get; set; }

        public int[] Ids { get; set; }
    }
}