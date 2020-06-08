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

            if( _dbContext.Products.Any() )
            {
                return;
            }

            using (var transaction = db.BeginTransaction())
            {
                _dbContext.Sections.AddRange(TestData.Sections);

                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] ON");
                _dbContext.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Sections] OFF");

                transaction.Commit();
            }

            using (var transaction = db.BeginTransaction())
            {
                _dbContext.Brands.AddRange(TestData.Brands);

                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] ON");
                _dbContext.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Brands] OFF");

                transaction.Commit();
            }

            using (var transaction = db.BeginTransaction())
            {
                _dbContext.Products.AddRange(TestData.Products);

                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] ON");
                _dbContext.SaveChanges();
                db.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Products] OFF");

                transaction.Commit();
            }

        }
    }
}
