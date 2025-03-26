using Microsoft.AspNetCore.Mvc; // Para Controller e IActionResult
using Microsoft.AspNetCore.Identity; // Para UserManager e IdentityUser
using System.Threading.Tasks; // Para Task

namespace easymed_mvc.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ProfilesController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                    // Redireciona para a página de login se o usuário não estiver autenticado
                    return RedirectToAction("Login", "Account", new { area = "Identity" });
            }

            if (await _userManager.IsInRoleAsync(user, "Doctor"))
            {
                return RedirectToAction("Profile", "Doctors");
            }
            else if (await _userManager.IsInRoleAsync(user, "Patient"))
            {
                return RedirectToAction("Profile", "Patients");
            }

            // Caso o usuário não tenha uma role atribuída
            return RedirectToAction("Index", "Home");
        }
    }
}