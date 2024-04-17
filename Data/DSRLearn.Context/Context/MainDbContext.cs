namespace DSRLearn.Context;

using DSRLearn.Context.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

public class MainDbContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
{
    public DbSet<UserProfile> UserProfile { get; set; }
    public DbSet<Debt> Debts { get; set; }
    public DbSet<Payment> Payments { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureUserDetails();
        modelBuilder.ConfigureDebts();
        modelBuilder.ConfigurePayments();
        modelBuilder.ConfigureUsers();
    }
}
