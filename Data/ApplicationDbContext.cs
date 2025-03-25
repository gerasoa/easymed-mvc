using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace easymed_mvc.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Doctor>()
            .HasOne(d => d.User)
            .WithOne()
            .HasForeignKey<Doctor>(d => d.UserId);
    }

public DbSet<Doctor> Doctor { get; set; } = default!;
}
