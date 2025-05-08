using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SafeVault.Models;

var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("AppDbContextConnection") ?? throw new InvalidOperationException("Connection string 'AppDbContextConnection' not found.");;

// Configure database
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("UserDb"));

builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; // Modify as needed
})
.AddRoles<IdentityRole>() // 🔹 Enables role support
.AddEntityFrameworkStores<AppDbContext>();


// Add MVC with Identity UI
builder.Services.AddControllersWithViews();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    // Create Roles
    var roles = new[] { "Admin", "User" };
    foreach (var role in roles)
    {
        if (!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }

    // Create Admin User
    var adminUser = new IdentityUser { UserName = "admin", Email = "admin@example.com", EmailConfirmed = true };
    if (await userManager.FindByNameAsync(adminUser.UserName) == null)
    {
        await userManager.CreateAsync(adminUser, "Admin@123"); // Change password accordingly
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

    // Create Regular User
    var regularUser = new IdentityUser { UserName = "user", Email = "user@example.com", EmailConfirmed = true };
    if (await userManager.FindByNameAsync(regularUser.UserName) == null)
    {
        await userManager.CreateAsync(regularUser, "User@123"); // Change password accordingly
        await userManager.AddToRoleAsync(regularUser, "User");
    }

}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication(); // Enable authentication
app.UseAuthorization();
app.UseStaticFiles(); // Enables serving static files

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages(); // Enables Identity UI routing

// Add Content Security Policy middleware
// This is a simple example. You may want to customize the CSP based on your needs.
app.Use(async (context, next) =>
{
    context.Response.Headers.Append("Content-Security-Policy", "default-src 'self'; script-src 'self'");
    await next();
});


app.Run();
