using Microsoft.AspNetCore.Identity;

namespace Blog.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        // You can add custom properties here
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? ProfilePictureUrl { get; set; }

        // Example: Adding a nullable date for when the user joined
        public DateTime? JoinDate { get; set; }
    }
}