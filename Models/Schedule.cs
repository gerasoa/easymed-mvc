using System.ComponentModel.DataAnnotations;

public class Schedule
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public Guid DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    [Required]
    [Display(Name = "Days of the Week")]
    public string DaysOfWeek { get; set; } // Ex: "Monday,Tuesday,Wednesday"

    [Required]
    [Display(Name = "Start Time")]
    [DataType(DataType.Time)]
    public TimeSpan StartTime { get; set; }

    [Required]
    [Display(Name = "End Time")]
    [DataType(DataType.Time)]
    public TimeSpan EndTime { get; set; }

    [Required]
    [Display(Name = "Valid Until")]
    [DataType(DataType.Date)]
    public DateTime ValidUntil { get; set; }

    [Required]
    [Range(15, 120, ErrorMessage = "Slot duration must be between 15 and 120 minutes.")]
    [Display(Name = "Slot Duration (minutes)")]
    public int SlotDurationInMinutes { get; set; }
}