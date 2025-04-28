
namespace RS.Core.Application.ViewModels.Comment
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string User_Id { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public string? ProfilePicture { get; set; } = "/images/default-avatar.png";
        public int? ParentCommentId { get; set; }
        public List<CommentViewModel> Replies { get; set; } = new List<CommentViewModel>();

    }
}
