using RS.Core.Domain.Entities;

namespace RS.Core.Application.Interfaces.Repositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<List<Comment>> GetByPostIdAsync(int postId);
    }
}
