using System.ComponentModel.DataAnnotations;

namespace RS.Core.Application.ViewModels.Comment
{
    public class SaveCommentViewModel
    {
        public int Id { get; set; }

        [Required]
        public string User_Id { get; set; }
       
        [Required(ErrorMessage = "Debe ingresar contenido")]
        public string Content { get; set; }
        public int? ParentCommentId { get; set; }
        [Required]
        public int PostID { get;  set; }
        public DateTime CommentDate { get;  set; } = DateTime.UtcNow;
    }
}
