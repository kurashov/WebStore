using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public User(): base() { }
        public User( string userName = null ) : base( userName ) { }

        public const string Admin = "Admin";
        public const string DefaultAdminPassword = "Admin";
    }
}
