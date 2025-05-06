using Microsoft.EntityFrameworkCore;
using SafeVault.Models;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Seed the database with a user
        modelBuilder.Entity<User>().HasData(new User { Id = 1, Username = "testuser", Password = "letmein", Email = "testuser@safevault.org" });
    }
}
