using Microsoft.AspNetCore.Identity;

namespace RS.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePicture { get; set; } = "/Images/default-avatar.png";
    }
}
