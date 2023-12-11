using FeeMgmBackend.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using FeeMgmBackend.Models;

namespace FeeMgmBackend;

public class DatabaseContext : IdentityDbContext<ApplicationUser>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    public DbSet<Law> Laws { get; set; }
    public DbSet<Member> Members { get; set; }
    public DbSet<Fine> Fines { get; set; }
    public DbSet<Payment> Payments { get; set; }
}