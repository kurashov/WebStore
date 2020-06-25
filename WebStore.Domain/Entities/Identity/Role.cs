using Microsoft.AspNetCore.Identity;

namespace WebStore.Domain.Entities.Identity
{
    public class Role : IdentityRole
    {
        public Role() : base() { }
        public Role( string roleName ) : base( roleName ) { }

        public const string Administrator = "Administrator";
        public const string User = "User";
    }
}