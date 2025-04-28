using AutoMapper;
using Microsoft.AspNetCore.Http;
using RS.Core.Application.Dtos.Account;
using RS.Core.Application.Helpers;
using RS.Core.Application.Interfaces.Repositories;
using RS.Core.Application.Interfaces.Services;
using RS.Core.Application.ViewModels.Comment;
using RS.Core.Application.ViewModels.Post;
using RS.Core.Domain.Entities;


namespace RS.Core.Application.Services
{
    public class PostService : GenericService<SavePostViewModel, PostViewModel, Post>, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IFriendshipRepository _friendshipRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IAccountService _accountService;
        private readonly AuthenticationResponse? _userSession;

        public PostService(IPostRepository postRepository, IHttpContextAccessor httpContextAccessor,
            IMapper mapper, IAccountService accountService, IFriendshipRepository friendshipRepository)
            : base(postRepository, mapper)
        {
            _postRepository = postRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _accountService = accountService;
            _friendshipRepository = friendshipRepository;
            _userSession = _httpContextAccessor.HttpContext?.Session.Get<AuthenticationResponse>("user");
        }

        public override async Task<SavePostViewModel> Add(SavePostViewModel vm)
        {
            vm.PostDate = DateTime.UtcNow;
            vm.User_Id = _userSession?.Id ?? vm.User_Id;

            if (vm.File != null && vm.PublicationType == "IMAGE")
            {
                vm.Archive = UploadFile(vm.File, vm.User_Id);
            }

            return await base.Add(vm);
        }

        public override async Task Update(SavePostViewModel vm, int id)
        {
            var existingPost = await _postRepository.GetByIdAsync(id);
            if (existingPost == null) return;

            vm.PostDate = existingPost.PostDate;
            vm.User_Id = _userSession?.Id ?? vm.User_Id;

            if (vm.File != null)
            {
                vm.Archive = UploadFile(vm.File, vm.User_Id, true, existingPost.Archive);
            }

            await base.Update(vm, id);
        }

        public async Task<List<PostViewModel>> GetAllViewModelWithInclude()
        {
            var posts = await _postRepository.GetAllWithCommentsAsync(new List<string> { "Comments" });

            if (_userSession != null)
            {
                posts = posts.Where(post => post.User_Id == _userSession.Id)
                             .OrderByDescending(post => post.PostDate)
                             .ToList();

                var allUsers = await _accountService.GetUsersByIdsAsync(posts.SelectMany(p => p.Comments.Select(c => c.UserID)).Distinct().ToList());

                return posts.Select(post => new PostViewModel
                {
                    Id = post.Id,
                    Body = post.Body,
                    PostDate = post.PostDate,
                    Archive = post.Archive,
                    PublicationType = post.PublicationType ?? "TEXT",
                    UserName = _userSession.UserName,
                    ProfilePicture = _userSession.ProfilePicture,
                    Comments = post.Comments?.Select(comment =>
                    {
                        var commentUser = allUsers.FirstOrDefault(u => u.Id == comment.UserID);
                        return new CommentViewModel
                        {
                            Id = comment.Id,
                            PostId = comment.PostID,
                            User_Id = comment.UserID,
                            UserName = commentUser?.UserName ?? "Usuario desconocido",
                            Content = comment.Content,
                            CommentDate = comment.CommentDate,
                            ProfilePicture = commentUser?.ProfilePicture ?? "/images/default-avatar.png",
                            ParentCommentId = comment.ParentCommentID
                        };
                    }).ToList() ?? new List<CommentViewModel>()
                }).ToList();
            }
            return new List<PostViewModel>();
        }


        public async Task<List<PostViewModel>> GetFriendsPostsAsync()
        {
            if (_userSession == null) return new List<PostViewModel>();

            var friendIds = await _friendshipRepository.GetFriendIdsByUserIdAsync(_userSession.Id);
            var posts = await _postRepository.GetPostsByUserIdsAsync(friendIds);

            var allUsers = await _accountService.GetUsersByIdsAsync(friendIds.Concat(posts.SelectMany(p => p.Comments.Select(c => c.UserID))).Distinct().ToList());

            return posts.Select(post =>
            {
                var user = allUsers.FirstOrDefault(u => u.Id == post.User_Id);
                return new PostViewModel
                {
                    Id = post.Id,
                    User_Id = post.User_Id,
                    Body = post.Body,
                    PostDate = post.PostDate,
                    Archive = post.Archive,
                    PublicationType = post.PublicationType,
                    UserName = user?.UserName ?? "Usuario desconocido",
                    ProfilePicture = user?.ProfilePicture ?? "/images/default-avatar.png",
                    Comments = post.Comments?.Select(comment =>
                    {
                        var commentUser = allUsers.FirstOrDefault(u => u.Id == comment.UserID);
                        return new CommentViewModel
                        {
                            Id = comment.Id,
                            PostId = comment.PostID,
                            User_Id = comment.UserID,
                            UserName = commentUser?.UserName ?? "Usuario desconocido",
                            Content = comment.Content,
                            CommentDate = comment.CommentDate,
                            ProfilePicture = commentUser?.ProfilePicture ?? "/images/default-avatar.png",
                            ParentCommentId = comment.ParentCommentID
                        };
                    }).ToList() ?? new List<CommentViewModel>()
                };
            }).ToList();
        }




        private string UploadFile(IFormFile file, string userId, bool isEditMode = false, string? existingPath = null)
        {
            if (isEditMode && file == null) return existingPath ?? "";

            string basePath = $"/Images/Users/{userId}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            // Generar un nombre de archivo unico
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string fullPath = Path.Combine(path, fileName);

            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // Eliminar imagen anterior si existe 
            if (isEditMode && !string.IsNullOrEmpty(existingPath) && existingPath != fullPath)
            {
                string oldFilePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{existingPath}");
                if (File.Exists(oldFilePath))
                {
                    File.Delete(oldFilePath);
                }
            }

            return $"{basePath}/{fileName}";
        }
    }
}

