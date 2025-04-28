using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RS.Core.Application.Interfaces.Services;
using RS.Core.Application.ViewModels.Comment;

namespace Red_social.Controllers
{
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveCommentViewModel vm)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                TempData["Error"] = "No has iniciado sesión.";
                return RedirectToAction("Index", "Home");
            }

            vm.User_Id = userId;

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "El comentario no puede estar vacío.";
                return RedirectToAction("Index", "Home");
            }

            await _commentService.Add(vm);


            // Detectar si el comentario se hizo en Home o Friend
            string referer = Request.Headers["Referer"].ToString();
            if (!string.IsNullOrEmpty(referer) && referer.Contains("/Friend"))
            {
                return RedirectToAction("Index", "Friend");
            }

            return RedirectToAction("Index", "Home");
        }



        [HttpPost]
        public async Task<IActionResult> Reply(SaveCommentViewModel vm)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            vm.User_Id = userId;

            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }

            await _commentService.Add(vm);

            // Detectar de donde viene la solicitud y redirigir correctamente
            string referer = Request.Headers["Referer"].ToString();
            if (referer.Contains("/Friend"))
            {
                return RedirectToAction("Index", "Friend"); // Redirigir a Amigos si el reply se hizo en Amigos
            }

            return RedirectToAction("Index", "Home"); // Si el reply se hizo en Home, volver al Home
        }
    }
}
