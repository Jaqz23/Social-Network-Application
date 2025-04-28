using RS.Core.Application.ViewModels.Post;

namespace RS.Core.Application.ViewModels.Friendship
{
    public class FriendViewModel
    {
        public List<FriendshipViewModel> Friends { get; set; } = new List<FriendshipViewModel>();
        public List<PostViewModel> FriendPosts { get; set; } = new List<PostViewModel>();
    }
}
