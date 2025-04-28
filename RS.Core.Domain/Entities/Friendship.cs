using RS.Core.Domain.Common;

namespace RS.Core.Domain.Entities
{
   public class Friendship : BaseEntity
   {
        public string User_Id1 { get; set; }
        public string User_Id2 { get; set; }

    }
}
