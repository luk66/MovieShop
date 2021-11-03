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
    public class FavoriteRepository : EfRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<Favorite>> GetAllFavoritesByUser(int id)
        {
            var favors = await _dbContext.Favorites.Include(m => m.Movie).Where(u => u.UserId == id).ToListAsync();
            return favors;
        }
    }
}
