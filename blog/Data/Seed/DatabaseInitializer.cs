namespace Blog.Data.Seed;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var services = scope.ServiceProvider;

        try
        {
            await RoleInitializer.InitializeAsync(services);
            await AdminUserInitializer.InitializeAsync(services);
            await DbInitializer.InitializeAsync(services);
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred while initializing the database.");
            throw;
        }
    }
}