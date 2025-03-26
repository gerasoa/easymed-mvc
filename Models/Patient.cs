using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class Patient
{
    [Key]
    public Guid Id { get; set; } // Chave primária do paciente

    [Required]
    [Display(Name = "Full Name")]
    public string FullName { get; set; }

    [Required]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; }

    [Display(Name = "Phone Number")]
    [Phone]
    public string PhoneNumber { get; set; }

    // Relacionamento opcional com IdentityUser (caso necessário)
    public string UserId { get; set; } // ID do usuário no Identity
    public IdentityUser User { get; set; }
}