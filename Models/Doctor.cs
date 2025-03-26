using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public enum DoctorStatus
{
    Pending,
    Approved,
    Rejected,
    Deactivated,
    Banned
}

public class Doctor
{
    [Key]
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public string Specialty { get; set; } // Ex: Cardiologia, Dermatologia
    public string UserId { get; set; } // Foreign Key to IdentityUser
    public DoctorStatus Status { get; set; }

    [NotMapped]
    public IdentityUser User { get; set; }
}

