using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

public class Doctor
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Specialty { get; set; } // Ex: Cardiologia, Dermatologia
    public string UserId { get; set; } // Foreign Key para IdentityUser

    [NotMapped]
    public IdentityUser User { get; set; }
}
