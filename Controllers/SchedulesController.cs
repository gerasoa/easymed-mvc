using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using easymed_mvc.Data;

public class SchedulesController : Controller
{
    private readonly ApplicationDbContext _context;

    public SchedulesController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Schedules/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Schedules/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ScheduleViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            if (viewModel.DoctorId == Guid.Empty)
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
                var doctorId = _context.Doctor.FirstOrDefault(d => d.UserId == userIdClaim.Value)?.Id;

                if (doctorId != null)
                {
                    viewModel.DoctorId = (Guid)doctorId;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Unable to determine the Doctor ID.");
                    return View(viewModel);
                }
            }

            // Valida a data de validade (máximo de 6 meses)
            if (viewModel.ValidUntil > DateTime.Now.AddMonths(6))
            {
                ModelState.AddModelError("ValidUntil", "The schedule cannot be valid for more than 6 months.");
                return View(viewModel);
            }

            // Converte os dias da semana para uma string separada por vírgulas
            var daysOfWeek = string.Join(",", viewModel.DaysOfWeek);

            var schedule = new Schedule
            {
                DoctorId = viewModel.DoctorId,
                DaysOfWeek = daysOfWeek,
                StartTime = viewModel.StartTime,
                EndTime = viewModel.EndTime,
                ValidUntil = viewModel.ValidUntil,
                SlotDurationInMinutes = viewModel.SlotDurationInMinutes
            };

            _context.Add(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Doctors");
        }

        return View(viewModel);
    }
}