using RS.Core.Domain.Entities;

namespace RS.Core.Application.Interfaces.Repositories
{
    public interface IPostRepository : IGenericRepository<Post>
    {
        Task<List<Post>> GetAllWithCommentsAsync(List<string> list);
        Task<List<Post>> GetPostsByUserIdsAsync(List<string> userIds);
    }
}
