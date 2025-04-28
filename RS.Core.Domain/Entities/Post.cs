using RS.Core.Domain.Common;

namespace RS.Core.Domain.Entities
{
   public class Post : BaseEntity
   {
        public string User_Id { get; set; }
        public string PublicationType { get; set; }
        public DateTime PostDate { get; set; }
        public string? Body { get; set; }
        public string? Archive { get; set; }

        // Navigation Property
        public ICollection<Comment> Comments { get; set; } 
    }
}
