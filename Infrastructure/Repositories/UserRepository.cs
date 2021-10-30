using ApplicationCore.Entities;
using ApplicationCore.RepositoryInterfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : EfRepository<User>, IUserRepository
    {

        public UserRepository(MovieShopDbContext dbContext): base(dbContext)
        {
        }

        public async Task<IEnumerable<Review>> GetReviewsByUser(int userId)
        {
            var userReviews = await _dbContext.Reviews.Include(r => r.Movie).Include(r=>r.User).Where(r => r.UserId == userId).ToListAsync();
            return userReviews;
        }

        //public async Task<User> AddUser(User user)
        //{
        //    await _dbContext.Users.AddAsync(user);
        //    await _dbContext.SaveChangesAsync();
        //    return user;
        //}

        public async Task<User> GetUserByEmail(string email)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
            return user;
        }
    }
}
