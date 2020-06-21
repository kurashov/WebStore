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

            var initializeIdentityTask = InitializeIdentityAsync();

            InitializeEmployees( db );

            InitializeProducts( db );
            
            initializeIdentityTask.Wait();
        }

        private async Task InitializeIdentityAsync()
        {
            Task<IdentityResult> createAdminRoleTask = null;
            Task<IdentityResult> createUserRoleTask = null;

            if( !await _roleManager.RoleExistsAsync( Role.Administrator ) )
            {
                createAdminRoleTask = _roleManager.CreateAsync( new Role( Role.Administrator ) );
            }

            if( !await _roleManager.RoleExistsAsync( Role.User ) )
            {
                createUserRoleTask = _roleManager.CreateAsync( new Role( Role.User ) );
            }

            createAdminRoleTask?.Wait();
            createUserRoleTask?.Wait();

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

            foreach( var childSection in TestData.Sections.Where( s => s.ParentId != null ) )
            {
                childSection.ParentSection = TestData.Sections.Single( s => s.Id == childSection.Id );
                childSection.ParentId = null;
            }

            foreach( var section in TestData.Sections )
            {
                var sectionProducts = TestData.Products.Where( p => p.SectionId == section.Id ).ToList();
                sectionProducts.ForEach( p =>
                {
                    p.SectionId = 0;
                    p.Section = section;
                } );
                section.Products = sectionProducts;
                section.Id = 0;
            }

            foreach( var brand in TestData.Brands )
            {
                var brandProducts = TestData.Products.Where( p => p.BrandId == brand.Id ).ToList();
                brandProducts.ForEach( p =>
                {
                    p.BrandId = null;
                    p.Brand = brand;
                } );
                brand.Products = brandProducts;
                brand.Id = 0;
            }

            foreach( var product in TestData.Products ) product.Id = 0;

            using( db.BeginTransaction() )
            {
                _dbContext.Sections.AddRange( TestData.Sections );
                _dbContext.Brands.AddRange( TestData.Brands );
                _dbContext.Products.AddRange( TestData.Products );
                _dbContext.SaveChanges();
                db.CommitTransaction();
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
