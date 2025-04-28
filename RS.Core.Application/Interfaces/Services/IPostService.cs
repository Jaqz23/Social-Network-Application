using RS.Core.Application.ViewModels.Post;
using RS.Core.Domain.Entities;

namespace RS.Core.Application.Interfaces.Services
{
    public interface IPostService : IGenericService<SavePostViewModel, PostViewModel, Post>
    {
        Task<List<PostViewModel>> GetAllViewModelWithInclude();
        Task<List<PostViewModel>> GetFriendsPostsAsync();
    }
}
