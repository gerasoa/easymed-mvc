using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using easymed_mvc.Data;
using System.Security.Claims;

public class AppointmentsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AppointmentsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Appointments
    public async Task<IActionResult> Index()
    {
        var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var patient = await _context.Patient.FirstOrDefaultAsync(p => p.UserId == loggedInUserId);
        if (patient == null)
        {
            TempData["ErrorMessage"] = "Patient record not found.";
            return RedirectToAction("Index", "Home");
        }

        var appointments = await _context.Appointment
            .Include(a => a.Doctor)
            .Where(a => a.PatientId == patient.Id)
            .OrderBy(a => a.AppointmentTime)
            .ToListAsync();

        return View(appointments);
    }

    // GET: Appointments/AvailableSlots
    public async Task<IActionResult> AvailableSlots(Guid doctorId, DateTime? selectedDate)
    {
        // Verifica se o médico existe
        var doctor = await _context.Doctor.FirstOrDefaultAsync(d => d.Id == doctorId);
        if (doctor == null)
        {
            return NotFound("Doctor not found.");
        }

        // Busca a agenda do médico
        var schedule = await _context.Schedule.FirstOrDefaultAsync(s => s.DoctorId == doctorId && s.ValidUntil >= DateTime.Now);
        if (schedule == null)
        {
            return NotFound("No schedule found for this doctor.");
        }

        // Verifica se a data selecionada é válida
        if (selectedDate == null || selectedDate.Value.Date < DateTime.Now.Date || selectedDate.Value.Date > schedule.ValidUntil.Date)
        {
            ModelState.AddModelError("selectedDate", "Please select a valid date within the schedule's validity period.");
            return View(new AvailableSlotsViewModel
            {
                DoctorId = doctorId,
                DoctorName = doctor.FullName,
                AvailableSlots = new List<DateTime>()
            });
        }

        // Converte os dias da semana da agenda para uma lista
        var daysOfWeek = schedule.DaysOfWeek.Split(',').Select(d => d.Trim()).ToList();

        // Verifica se o dia selecionado está na lista de dias da semana da agenda
        var selectedDayOfWeek = selectedDate.Value.DayOfWeek.ToString();
        if (!daysOfWeek.Contains(selectedDayOfWeek))
        {
            ModelState.AddModelError("selectedDate", "The selected date is not within the doctor's working days.");
            return View(new AvailableSlotsViewModel
            {
                DoctorId = doctorId,
                DoctorName = doctor.FullName,
                AvailableSlots = new List<DateTime>()
            });
        }

        // Calcula os horários disponíveis para o dia selecionado
        var availableSlots = new List<DateTime>();
        var startTime = selectedDate.Value.Date.Add(schedule.StartTime);
        var endTime = selectedDate.Value.Date.Add(schedule.EndTime);

        for (var time = startTime; time < endTime; time = time.AddMinutes(schedule.SlotDurationInMinutes))
        {
            var isBooked = await _context.Appointment.AnyAsync(a => a.DoctorId == doctorId && a.AppointmentTime == time);
            if (!isBooked)
            {
                availableSlots.Add(time);
            }
        }

        // Cria o ViewModel com os horários disponíveis
        var viewModel = new AvailableSlotsViewModel
        {
            DoctorId = doctorId,
            DoctorName = doctor.FullName,
            AvailableSlots = availableSlots
        };

        return View(viewModel);
    }

    // POST: Appointments/Book
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Book(Guid doctorId, DateTime appointmentTime)
    {
        // Verifica se o médico existe
        var doctor = await _context.Doctor.FirstOrDefaultAsync(d => d.Id == doctorId);
        if (doctor == null)
        {
            return NotFound("Doctor not found.");
        }

        // Verifica se o horário selecionado está disponível
        var isBooked = await _context.Appointment.AnyAsync(a => a.DoctorId == doctorId && a.AppointmentTime == appointmentTime);
        if (isBooked)
        {
            ModelState.AddModelError("appointmentTime", "The selected time slot is already booked.");
            return RedirectToAction("AvailableSlots", new { doctorId, selectedDate = appointmentTime.Date });
        }

        // Cria o agendamento
        var appointment = new Appointment
        {
            DoctorId = doctorId,
            PatientId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)),
            AppointmentTime = appointmentTime
        };

        _context.Add(appointment);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> BookAppointment(BookAppointmentViewModel model)
    {
        if (ModelState.IsValid)
        {
            var loggedInUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await _context.Patient.FirstOrDefaultAsync(p => p.UserId == loggedInUserId);
            if (patient == null)
            {
                TempData["ErrorMessage"] = "Patient record not found.";
                return RedirectToAction("AvailableSlots", new { doctorId = model.DoctorId });
            }

            // model.PatientId = patient.Id;

            var appointment = new Appointment
            {
                DoctorId = model.DoctorId,
                PatientId = patient.Id, // ID do paciente fornecido no modelo
                AppointmentTime = model.SelectedSlot,
                CreatedAt = DateTime.Now
            };

            _context.Appointment.Add(appointment);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Appointment booked successfully!";
            return RedirectToAction("Index", "Appointments");
        }

        TempData["ErrorMessage"] = "Failed to book the appointment. Please try again.";
        return RedirectToAction("AvailableSlots", new { doctorId = model.DoctorId });
    }
}