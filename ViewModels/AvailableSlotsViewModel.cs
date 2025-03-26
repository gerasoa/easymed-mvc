using System;
using System.Collections.Generic;

public class AvailableSlotsViewModel
{
    public Guid DoctorId { get; set; }
    public string DoctorName { get; set; }
    public DateTime SelectedDate { get; set; }
    public List<DateTime> AvailableSlots { get; set; }
    public DateTime SelectedSlot { get; set; }
}