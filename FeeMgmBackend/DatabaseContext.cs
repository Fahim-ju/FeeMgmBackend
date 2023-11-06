using API.Entity;
using Microsoft.EntityFrameworkCore;

namespace API;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Law> Laws { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Fine> Fines { get; set; }
}