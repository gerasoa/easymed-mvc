using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

public class Appointment
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [Display(Name = "Doctor")]
    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    [Required]
    [Display(Name = "Patient")]
    public Guid PatientId { get; set; } // ID do usuário paciente
    public Patient Patient { get; set; }

    [Required]
    [Display(Name = "Appointment Time")]
    [DataType(DataType.DateTime)]
    public DateTime AppointmentTime { get; set; }

    public DateTime CreatedAt { get; set; }

    [Display(Name = "Is Confirmed")]
    public bool IsConfirmed { get; set; }
}

