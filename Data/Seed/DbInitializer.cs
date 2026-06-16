using Microsoft.EntityFrameworkCore;

namespace Blog.Data.Seed;

public static class DbInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

        // Apply pending migrations
        await context.Database.MigrateAsync();
    }
}