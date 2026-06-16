using Microsoft.EntityFrameworkCore;
using Blog.Data;
using Blog.Data.Repository;
using Microsoft.AspNetCore.Identity;
using Blog.Infrastructure.Identity;
using Blog.Data.Seed;
using Blog.Data.FileManager;


var builder = WebApplication.CreateBuilder(args);

// Register DbContext with Sql Server
var connectionString = "";
if (builder.Environment.IsDevelopment())
        connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
else
{
        connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
}

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
{
        opt.UseNpgsql(connectionString);
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
        options.Password.RequireDigit = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredLength = 6;
        options.SignIn.RequireConfirmedAccount = true;
})
.AddRoles<IdentityRole>() // <-- THIS ADDS ROLE SUPPORT 
.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
        options.LoginPath = "/Auth/Login";
});

builder.Services.AddScoped<IRepository, Repository>();

builder.Services.AddScoped<IFileManager, FileManager>();
// Add services to the container
builder.Services.AddControllersWithViews(); // for mvc
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// Initialize database with all seeders
await DatabaseInitializer.InitializeAsync(app.Services);

// app.MapGet("/", () => "Hello World!");
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Configure pipeline...
app.UseAuthentication();  // Important: Add this!
app.UseAuthorization();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
