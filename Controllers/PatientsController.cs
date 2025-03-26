using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using easymed_mvc.Data;
using System.Security.Claims;
using easymed_mvc.ViewModels;

namespace easymed_mvc.Controllers
{
    public class PatientsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PatientsController(ApplicationDbContext context)
        {
            _context = context;
        }


// GET: Patients/Profile
        public async Task<IActionResult> Profile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var patient = await _context.Patient.FirstOrDefaultAsync(d => d.UserId == userId);

            if (patient == null)
            {
                return View(new PatientViewModel());
            }

            var viewModel = new PatientViewModel
            {
                Id = patient.Id,
                FullName = patient.FullName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber
            };

            return View(viewModel);
        }

        // POST: Patient/Profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(PatientViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var patient = await _context.Patient.FirstOrDefaultAsync(d => d.UserId == userId);

                if (patient == null)
                {
                    patient = new Patient
                    {
                        UserId = userId,
                        FullName = viewModel.FullName,
                        Email = viewModel.Email,
                        PhoneNumber = viewModel.PhoneNumber 
                    };
                    _context.Add(patient);
                }
                else
                {
                    patient.FullName = viewModel.FullName;
                    patient.Email = viewModel.Email;
                    patient.PhoneNumber = viewModel.PhoneNumber;
                    _context.Update(patient);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Profile));
            }

            return View(viewModel);
        }

        // GET: Patients
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Patient.Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Patients/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // GET: Patients/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Patients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FullName,Email,PhoneNumber,UserId")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                patient.Id = Guid.NewGuid();
                _context.Add(patient);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", patient.UserId);
            return View(patient);
        }

        // GET: Patients/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", patient.UserId);
            return View(patient);
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,FullName,Email,PhoneNumber,UserId")] Patient patient)
        {
            if (id != patient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patient);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatientExists(patient.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", patient.UserId);
            return View(patient);
        }

        // GET: Patients/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var patient = await _context.Patient
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patient == null)
            {
                return NotFound();
            }

            return View(patient);
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var patient = await _context.Patient.FindAsync(id);
            if (patient != null)
            {
                _context.Patient.Remove(patient);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatientExists(Guid id)
        {
            return _context.Patient.Any(e => e.Id == id);
        }
    }
}
