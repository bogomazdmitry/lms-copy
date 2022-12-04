using Microsoft.AspNetCore.Identity;

namespace LmsCopy.Web.Entites;

public class UserRole : IdentityRole<Guid>
{
    public UserRole() { }
    
    public UserRole(string role) : base(role)
    {
    }

    public const string Professor = "Professor";
}
