
using RS.Core.Application.ViewModels.Friendship;
using RS.Core.Domain.Entities;

namespace RS.Core.Application.Interfaces.Services
{
    public interface IFriendshipService : IGenericService<SaveFriendshipViewModel, FriendshipViewModel, Friendship>
    {
        Task<List<FriendshipViewModel>> GetAllFriendsAsync();
        Task<(bool Success, string Message)> AddFriendAsync(string friendUsername);
        Task<bool> RemoveFriendAsync(string friendId);
        Task<List<string>> GetFriendIdsByUserIdAsync(string userId);

    }
}
