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
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> Get30TopRatedMovies()
        {
            var movies = await _dbContext.Reviews.Include(m => m.Movie)
                        .GroupBy(r => new
                        {
                            id = r.MovieId,
                            r.Movie.PosterUrl,
                            r.Movie.Title,
                            r.Movie.ReleaseDate
                        })
                        .OrderByDescending(r => r.Average(r => r.Rating))
                        .Select(m => new Movie
                        {
                            Id = m.Key.id,
                            PosterUrl = m.Key.PosterUrl,
                            Title = m.Key.Title,
                            ReleaseDate = m.Key.ReleaseDate,
                            Rating = m.Average(x => x.Rating)
                        }).Take(30).ToListAsync();

            
            return movies;
            
        }

        public async Task<Movie> GetMovieById(int Id)
        {
            var movie = await _dbContext.Movies.Include(m => m.Casts).ThenInclude(m => m.Cast)
                .Include(m => m.Genres).ThenInclude(m => m.Genre)
                .Include(m => m.Trailers).FirstOrDefaultAsync(m => m.Id == Id);

            var movieRating = await _dbContext.Reviews.Where(r => r.MovieId == Id).DefaultIfEmpty().AverageAsync(r => r == null ? 0 : r.Rating);
            if (movieRating > 0) movie.Rating = movieRating;

            return movie;
        }

        public async Task<IEnumerable<Review>> GetMovieReviews(int id, int pageSize = 30, int page = 1)
        {
            var reviews = await _dbContext.Reviews.Include(r => r.User).Where(r => r.MovieId == id).ToListAsync();
            return reviews;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenreId(int id)
        {
            var movies = await _dbContext.movieGenres.Where(g => g.GenreId == id).Include(mg => mg.Movie).Select(m => m.Movie).ToListAsync();

            return movies;
        }

        public async Task<IEnumerable<Movie>> GetTop30RevenueMovies()
        {
            // we are gonna use EF with LINQ to get top 30 movies by revenue
            // SQL select top 30 * from movies order by Revenue
            // you can await only for tasks
            // EF and Dapper have both sync and async methods
            var movies = await  _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }


    }
}
