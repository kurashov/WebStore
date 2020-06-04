using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services
{
    public class InMemoryProductData : IProductData
    {
        public IEnumerable<Section> GetSections() => TestData.Sections;

        public IEnumerable<Brand> GetBrands() => TestData.Brands;
        public IEnumerable<Product> GetProducts( ProductFilter filter )
        {
            var result = TestData.Products;
            if( filter.SectionId.HasValue )
            {
                result = result.Where( p => p.SectionId == filter.SectionId ).ToList();
            }

            if( filter.BrandId.HasValue )
            {
                result = result.Where( p => p.BrandId == filter.BrandId ).ToList();
            }

            return result;
        }
    }
}