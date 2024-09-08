using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyApi.Models;

namespace MyApi.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> context) : base(context)
    {
    }

    public DbSet<Product> Products { get; set; } = null!;
}