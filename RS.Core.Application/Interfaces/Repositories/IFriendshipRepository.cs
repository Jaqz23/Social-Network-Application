using RS.Core.Domain.Entities;

namespace RS.Core.Application.Interfaces.Repositories
{
    public interface IFriendshipRepository : IGenericRepository<Friendship>
    {
        Task<List<Friendship>> GetAllByUserIdAsync(string userId);
        Task<Friendship> GetFriendshipAsync(string userId1, string userId2);
        Task<List<string>> GetFriendIdsByUserIdAsync(string userId);
    }
}
