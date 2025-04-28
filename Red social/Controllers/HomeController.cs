using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RS.Core.Application.Interfaces.Services;
using RS.Core.Application.ViewModels.Post;

namespace Red_social.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;

        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await _postService.GetAllViewModelWithInclude();
            return View(posts.OrderByDescending(p => p.PostDate).ToList());
        }

        public IActionResult Create()
        {
            return View(new SavePostViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePostViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                TempData["ModelErrors"] = ModelState.Values.SelectMany(v => v.Errors)
                                                           .Select(e => e.ErrorMessage)
                                                           .ToList();
                return View(vm);
            }

            SavePostViewModel postVm = await _postService.Add(vm);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var post = await _postService.GetByIdSaveViewModel(id);
            if (post == null) return RedirectToAction("Index"); // Redirigir si no existe

            return View(post);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePostViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            SavePostViewModel postvm = await _postService.GetByIdSaveViewModel(vm.Id);

            if (postvm == null)
            {
                return RedirectToAction("Index");
            }

            if (vm.PublicationType == "IMAGE" && vm.File != null)
            {
                vm.Archive = UploadFile(vm.File, vm.User_Id, true, postvm.Archive);
            }
            // Si es un video, actualizar el enlace
            else if (vm.PublicationType == "VIDEO")
            {
                vm.Archive = vm.Archive;  // Mantener la URL actual
            }
            // Si es un texto, NO debe haber archivo
            else if (vm.PublicationType == "TEXT")
            {
                vm.Archive = null;
            }
            // Si no se cambia el archivo, mantener el existente
            else
            {
                vm.Archive = postvm.Archive;
            }

            await _postService.Update(vm, vm.Id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var vm = await _postService.GetByIdSaveViewModel(id);
            if (vm == null) return NotFound();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            var post = await _postService.GetByIdSaveViewModel(id);
            if (post == null) return NotFound();

            await _postService.Delete(id);

            // Eliminar imagen si tiene
            if (!string.IsNullOrEmpty(post.Archive))
            {
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{post.Archive}");
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            return RedirectToAction("Index");
        }

        private string UploadFile(IFormFile file, string userId, bool isEditMode = false, string? existingPath = null)
        {
            if (isEditMode && file == null)
            {
                return existingPath ?? "";
            }

            string basePath = $"/Images/Users/{userId}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string fullPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode && !string.IsNullOrEmpty(existingPath))
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{existingPath}");
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }

            return $"{basePath}/{fileName}";
        }
    }
}