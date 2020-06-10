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

            using (var transaction = db.BeginTransaction())
            {
                foreach( var section in TestData.Sections )
                {
                    section.Id = 0;
                    _dbContext.Sections.Add( section );
                }
                _dbContext.SaveChanges();
                transaction.Commit();
            }

            using (var transaction = db.BeginTransaction())
            {
                foreach( var brand in TestData.Brands )
                {
                    brand.Id = 0;
                    _dbContext.Brands.Add( brand );
                }
                _dbContext.SaveChanges();
                transaction.Commit();
            }

            using (var transaction = db.BeginTransaction())
            {
                foreach( var product in TestData.Products )
                {
                    product.Id = 0;
                    _dbContext.Products.Add( product );
                }
                _dbContext.SaveChanges();
                transaction.Commit();
            }

        }
    }
}
