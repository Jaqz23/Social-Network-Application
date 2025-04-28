using System.ComponentModel.DataAnnotations;

namespace RS.Core.Application.ViewModels.Friendship
{
    public class SaveFriendshipViewModel
    {
        public int Id { get; set; }

        public string? User_Id1 { get; set; }
        public string User_Id2 { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string FriendUser { get; set; }
    }
}
