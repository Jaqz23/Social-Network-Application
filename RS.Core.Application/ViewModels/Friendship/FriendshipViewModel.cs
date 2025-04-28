
namespace RS.Core.Application.ViewModels.Friendship
{
    public class FriendshipViewModel
    {
        public int Id { get; set; }
        public string User_Id1 { get; set; }
        public string User_Id2 { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ProfilePictureFriend { get; set; } = "/images/default-avatar.png";
        public string UserName { get; set; }

    }
}
