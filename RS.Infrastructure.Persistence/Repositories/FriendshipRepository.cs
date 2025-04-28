using Microsoft.EntityFrameworkCore;
using RS.Core.Application.Interfaces.Repositories;
using RS.Core.Domain.Entities;
using RS.Infrastructure.Persistence.Contexts;

namespace RS.Infrastructure.Persistence.Repositories
{
    public class FriendshipRepository : GenericRepository<Friendship>, IFriendshipRepository
    {
        private readonly ApplicationContext _dbContext;

        public FriendshipRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Friendship>> GetAllByUserIdAsync(string userId)
        {
            return await _dbContext.Friendships
                .Where(f => f.User_Id1 == userId || f.User_Id2 == userId)
                .ToListAsync();
        }

        public async Task<Friendship> GetFriendshipAsync(string userId1, string userId2)
        {
            return await _dbContext.Friendships
                .FirstOrDefaultAsync(f => (f.User_Id1 == userId1 && f.User_Id2 == userId2) ||
                                          (f.User_Id1 == userId2 && f.User_Id2 == userId1));
        }

        public async Task<List<string>> GetFriendIdsByUserIdAsync(string userId)
        {
            return await _dbContext.Friendships
                .Where(f => f.User_Id1 == userId || f.User_Id2 == userId)
                .Select(f => f.User_Id1 == userId ? f.User_Id2 : f.User_Id1)
                .ToListAsync();
        }

    }
}
