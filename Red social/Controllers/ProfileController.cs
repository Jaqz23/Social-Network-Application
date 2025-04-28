using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RS.Core.Application.Interfaces.Services;
using RS.Core.Application.ViewModels.User;

namespace Red_social.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileController(IAccountService accountService, IHttpContextAccessor httpContextAccessor)
        {
            _accountService = accountService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "User");
            }

            var user = await _accountService.GetSingleUserByIdAsync(userId);
            if (user == null)
            {
                TempData["Error"] = "Error al cargar los datos del usuario.";
                return RedirectToAction("Index", "Home");
            }

            var profileVm = new ProfileViewModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Phone = user.Phone,
                ProfilePicture = user.ProfilePicture
            };

            return View(profileVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProfileViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", vm);
            }

            var userId = _httpContextAccessor.HttpContext.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "User");
            }

            var result = await _accountService.UpdateProfileAsync(userId, vm);

            if (!result.Success)
            {
                TempData["Error"] = result.Message;
                return View("Index", vm);
            }

            TempData["Success"] = "Perfil actualizado correctamente.";
            return RedirectToAction("Index");
        }
    }

}
