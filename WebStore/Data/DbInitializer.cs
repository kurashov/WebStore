using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WebStore.DAL.Contexts;
using WebStore.Domain.Entities.Identity;

namespace WebStore.Data
{
    public class DbInitializer
    {
        private readonly WebStoreDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public DbInitializer( WebStoreDbContext dbContext,
            UserManager<User> userManager,
            RoleManager<Role> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
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

            InitializeIdentityAsync().Wait();

            InitializeEmployees( db );

            InitializeProducts( db );
        }

        private async Task InitializeIdentityAsync()
        {
            if( !await _roleManager.RoleExistsAsync( Role.Administrator ) )
            {
                await _roleManager.CreateAsync( new Role( Role.Administrator ) );
            }

            if( !await _roleManager.RoleExistsAsync( Role.User ) )
            {
                await _roleManager.CreateAsync( new Role( Role.User ) );
            }

            if (await _userManager.FindByNameAsync( User.Admin ) is null)
            {
                var admin = new User( User.Admin );

                var result = await _userManager.CreateAsync(admin, User.DefaultAdminPassword);
                if( result.Succeeded )
                {
                    await _userManager.AddToRoleAsync(admin, Role.Administrator);
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description);
                    throw new InvalidOperationException($"Error during create Admin: {string.Join(",", errors)}");
                }
            }
        }

        private void InitializeProducts( DatabaseFacade db )
        {
            if( _dbContext.Products.Any() )
            {
                return;
            }

            if (_dbContext.Products.Any())
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

        private void InitializeEmployees( DatabaseFacade db )
        {
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
        }
    }
}
