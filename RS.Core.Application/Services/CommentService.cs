using AutoMapper;
using Microsoft.AspNetCore.Http;
using RS.Core.Application.Dtos.Account;
using RS.Core.Application.Helpers;
using RS.Core.Application.Interfaces.Repositories;
using RS.Core.Application.Interfaces.Services;
using RS.Core.Application.ViewModels.Comment;
using RS.Core.Domain.Entities;
using System.Security.Claims;

namespace RS.Core.Application.Services
{
    public class CommentService : GenericService<SaveCommentViewModel, CommentViewModel, Comment>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly AuthenticationResponse? _userSession;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper)
            : base(commentRepository, mapper)
        {
            _commentRepository = commentRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _userSession = _httpContextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user");
        }

        public override async Task<SaveCommentViewModel> Add(SaveCommentViewModel vm)
        {
            if (string.IsNullOrEmpty(vm.User_Id))
            {
                throw new Exception("El usuario no está autenticado.");
            }

            vm.CommentDate = DateTime.UtcNow;
            var comment = _mapper.Map<Comment>(vm);
            
            // Si es un reply, validar que el comentario padre exista
            if (vm.ParentCommentId.HasValue)
            {
                var parentComment = await _commentRepository.GetByIdAsync(vm.ParentCommentId.Value);
                if (parentComment == null)
                {
                    throw new Exception("El comentario padre no existe.");
                }
            }
            return await base.Add(vm);
        }

        public async Task<List<CommentViewModel>> GetByPostIdAsync(int postId)
        {
            var comments = await _commentRepository.GetByPostIdAsync(postId);

            return comments.Select(comment => new CommentViewModel
            {
                Id = comment.Id,
                PostId = comment.PostID,
                User_Id = comment.UserID,
                UserName = _userSession?.UserName ?? "Usuario",
                ProfilePicture = _userSession?.ProfilePicture ?? "/images/default-avatar.png",
                Content = comment.Content,
                CommentDate = comment.CommentDate,
                ParentCommentId = comment.ParentCommentID
            }).ToList();
        }
    }
}
