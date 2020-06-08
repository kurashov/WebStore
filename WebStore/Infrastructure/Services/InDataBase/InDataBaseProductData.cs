using System.Collections.Generic;
using System.Linq;
using WebStore.DAL.Contexts;
using WebStore.Domain.Entities;
using WebStore.Infrastructure.Interfaces;

namespace WebStore.Infrastructure.Services.InDataBase
{
    public class InDataBaseProductData : IProductData
    {
        private readonly WebStoreDbContext _dbContext;

        public InDataBaseProductData( WebStoreDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Section> GetSections() => _dbContext.Sections;

        public IEnumerable<Brand> GetBrands() => _dbContext.Brands;

        public IEnumerable<Product> GetProducts( ProductFilter filter = null )
        {
            IQueryable<Product> result = _dbContext.Products;

            if( filter?.SectionId != null )
            {
                result = result.Where( p => p.SectionId == filter.SectionId );
            }

            if (filter?.BrandId != null)
            {
                result = result.Where(p => p.BrandId == filter.BrandId);
            }

            return result;
        }
    }
}
