using RS.Core.Domain.Common;

namespace RS.Core.Domain.Entities
{
    public class Comment : BaseEntity
    {

        public int PostID { get; set; }
        public string UserID { get; set; }
        public string Content { get; set; }
        public DateTime CommentDate { get; set; }
        public int? ParentCommentID { get; set; }
        public Post Post { get; set; }
        public Comment? ParentComment { get; set; }
        public ICollection<Comment>? Replies { get; set; }
    }
}
