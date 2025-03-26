// filepath: c:\GitHub\easymed-mvc\ViewModels\ScheduleViewModel.cs
using System.ComponentModel.DataAnnotations;

public class ScheduleViewModel
{
    public Guid DoctorId { get; set; }

    [Required]
    [Display(Name = "Days of the Week")]
    public List<string> DaysOfWeek { get; set; } // Lista de dias selecionados

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