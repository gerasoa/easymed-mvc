using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace easymed_mvc.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Doctor> Doctor { get; set; }
    public DbSet<Schedule> Schedule { get; set; }
    public DbSet<Appointment> Appointment { get; set; }
    public DbSet<Patient> Patient { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Doctor>()
            .HasOne(d => d.User)
            .WithOne()
            .HasForeignKey<Doctor>(d => d.UserId);

        builder.Entity<Schedule>()
            .HasOne(s => s.Doctor)
            .WithMany()
            .HasForeignKey(s => s.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany()
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany()
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Patient>()
            .HasOne(d => d.User)
            .WithOne()
            .HasForeignKey<Patient>(d => d.UserId);
    }
}
