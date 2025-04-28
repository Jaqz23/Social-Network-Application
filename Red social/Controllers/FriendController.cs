using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RS.Core.Application.Interfaces.Services;
using RS.Core.Application.ViewModels.Friendship;

namespace Red_social.Controllers
{
    [Authorize]
    public class FriendController : Controller
    {
        private readonly IFriendshipService _friendshipService;
        private readonly IPostService _postService;

        public FriendController(IFriendshipService friendshipService, IPostService postService)
        {
            _friendshipService = friendshipService;
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            // Obtener amigos
            var friends = await _friendshipService.GetAllFriendsAsync();

            // Obtener publicaciones de los amigos
            var friendPosts = await _postService.GetFriendsPostsAsync();

            // Pasar ambas listas a la vista
            var model = new FriendViewModel
            {
                Friends = friends,
                FriendPosts = friendPosts
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddFriend(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                TempData["Error"] = "Debe ingresar un nombre de usuario.";
                return RedirectToAction("Index");
            }

            var (success, message) = await _friendshipService.AddFriendAsync(username);

            if (success)
            {
                TempData["Success"] = message; // Mensaje de éxito
            }
            else
            {
                TempData["Error"] = message; // Mensaje de error específico
            }

            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> RemoveFriend(string friendId)
        {
            var success = await _friendshipService.RemoveFriendAsync(friendId);
            TempData["Message"] = success ? "Amigo eliminado correctamente." : "No se pudo eliminar al amigo.";
            return RedirectToAction("Index");
        }
    }

}
