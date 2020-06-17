using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Contexts;

namespace WebStore.Data
{
    public class DbInitializer
    {
        private readonly WebStoreDbContext _dbContext;

        public DbInitializer( WebStoreDbContext dbContext )
        {
            _dbContext = dbContext;
        }

        public void Initialize()
        {
            var db = _dbContext.Database;

            //if( db.EnsureDeleted() )
            //{
            //    if( !db.EnsureCreated() )
            //    {
            //        throw new InvalidOperationException( "Error during creating data base" );
            //    }
            //}

            db.Migrate();

            if( !_dbContext.Employees.Any() )
            {
                using( var transaction = db.BeginTransaction() )
                {
                    foreach( var employee in TestData.Employees )
                    {
                        employee.Id = 0;
                        _dbContext.Add( employee );
                    }
                    _dbContext.SaveChanges();
                    transaction.Commit();
                }
            }

            if( _dbContext.Products.Any() )
            {
                return;
            }

            foreach (var childSection in TestData.Sections.Where(s => s.ParentId != null))
            {
                childSection.ParentSection = TestData.Sections.Single(s => s.Id == childSection.Id);
                childSection.ParentId = null;
            }

            foreach (var section in TestData.Sections)
            {
                var sectionProducts = TestData.Products.Where(p => p.SectionId == section.Id).ToList();
                sectionProducts.ForEach(p =>
                {
                    p.SectionId = 0;
                    p.Section = section;
                });
                section.Products = sectionProducts;
                section.Id = 0;
            }

            foreach (var brand in TestData.Brands)
            {
                var brandProducts = TestData.Products.Where(p => p.BrandId == brand.Id).ToList();
                brandProducts.ForEach(p =>
                {
                    p.BrandId = null;
                    p.Brand = brand;
                });
                brand.Products = brandProducts;
                brand.Id = 0;
            }

            foreach (var product in TestData.Products) product.Id = 0;

            using (db.BeginTransaction())
            {
                _dbContext.Sections.AddRange(TestData.Sections);
                _dbContext.Brands.AddRange(TestData.Brands);
                _dbContext.Products.AddRange(TestData.Products);
                _dbContext.SaveChanges();
                db.CommitTransaction();
            }

        }
    }
}
