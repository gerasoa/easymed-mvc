using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using easymed_mvc.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using easymed_mvc.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace easymed_mvc.Controllers;
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationDbContext _context;

    public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }    

    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var doctors = await _context.Doctor.ToListAsync();
        return View(doctors);
    }

    public IActionResult Privacy()
    {
        var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
        ViewBag.User = user;

        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
