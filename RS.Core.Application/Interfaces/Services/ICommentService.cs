using RS.Core.Application.ViewModels.Comment;
using RS.Core.Domain.Entities;

namespace RS.Core.Application.Interfaces.Services
{
    public interface ICommentService : IGenericService<SaveCommentViewModel, CommentViewModel, Comment>
    {
        
    }
}
