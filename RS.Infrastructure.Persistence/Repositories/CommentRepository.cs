using Microsoft.EntityFrameworkCore;
using RS.Core.Application.Interfaces.Repositories;
using RS.Core.Domain.Entities;
using RS.Infrastructure.Persistence.Contexts;

namespace RS.Infrastructure.Persistence.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly ApplicationContext _dbContext;

        public CommentRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Comment>> GetByPostIdAsync(int postId)
        {
            return await _dbContext.Comments
                .Where(c => c.PostID == postId)
                .OrderBy(c => c.CommentDate)
                .ToListAsync();
        }
    }
}
