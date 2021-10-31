﻿using ApplicationCore.Entities;
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
    public class PurchaseRepository : EfRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MovieShopDbContext dbContext): base(dbContext)
        {

        }

        public async Task<IEnumerable<Purchase>> GetAllPurchases(int pageSize = 30, int pageIndex = 1)
        {
            var purchases = await _dbContext.Purchases.Include(p=>p.Movie).ToListAsync();
            return purchases;
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesByMovie(int movieId, int pageSize = 30, int pageIndex = 1)
        {
            var purchases = await _dbContext.Purchases.Where(p => p.MovieId == movieId).ToListAsync();
            return purchases;
        }

        public async Task<IEnumerable<Purchase>> GetAllPurchasesForUser(int userId, int pageSize = 30, int pageIndex = 1)
        {
            var purchases = await _dbContext.Purchases.Include(p => p.Movie).Where(p => p.UserId == userId).ToListAsync();
            return purchases;
        }

        public async Task<Purchase> GetPurchaseDetails(int userId, int movieId)
        {
            var purchase = await _dbContext.Purchases.Include(p=>p.Movie).FirstOrDefaultAsync(p => p.UserId == userId && p.MovieId == movieId);
            return purchase;
        }
    }
}
