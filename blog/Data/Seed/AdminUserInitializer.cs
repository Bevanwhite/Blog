using Blog.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Blog.Data.Seed;

public static class AdminUserInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();

        var adminEmail = configuration["AdminSettings:Email"] ?? "admin@example.com";
        var adminPassword = configuration["AdminSettings:Password"] ?? "Admin@123456";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true,
                FirstName = "System",
                LastName = "Administrator",
                ProfilePictureUrl = string.Empty
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}