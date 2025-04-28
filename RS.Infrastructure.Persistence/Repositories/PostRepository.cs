using Microsoft.EntityFrameworkCore;
using RS.Core.Application.Interfaces.Repositories;
using RS.Core.Domain.Entities;
using RS.Infrastructure.Persistence.Contexts;

namespace RS.Infrastructure.Persistence.Repositories
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        private readonly ApplicationContext _dbContext;

        public PostRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Post>> GetAllWithCommentsAsync(List<string> properties)
        {
            var query = _dbContext.Set<Post>().AsQueryable();

            foreach (var property in properties)
            {
                query = query.Include(property);
            }

            return await query.Include(p => p.Comments).ToListAsync();
        }

        public async Task<List<Post>> GetPostsByUserIdsAsync(List<string> userIds)
        {
            return await _dbContext.Posts
                .Where(post => userIds.Contains(post.User_Id))
                .Include(p => p.Comments)
                .OrderByDescending(post => post.PostDate)
                .ToListAsync();
        }

    }
}
