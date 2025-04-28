using RS.Core.Application.Enums;
using RS.Core.Application.ViewModels.Comment;

namespace RS.Core.Application.ViewModels.Post
{
    public class PostViewModel
    {
        public int Id { get; set; }
        public string? Body { get; set; }
        public string PublicationType { get; set; }
        public DateTime PostDate { get; set; }
        public string User_Id { get; set; }
        public string UserName { get; set; }
        public string? ProfilePicture { get; set; } = "/images/default-avatar.png";
        public string? Archive { get; set; }
        public List<CommentViewModel> Comments { get; set; } = new List<CommentViewModel>();
    }
}
