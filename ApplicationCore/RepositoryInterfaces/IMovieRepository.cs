using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    public interface IMovieRepository: IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> Get30TopRatedMovies();
        //method thats gonna get 30 highest revenue movies
        Task<IEnumerable<Movie>> GetTop30RevenueMovies();
        Task<IEnumerable<Review>> GetMovieReviews(int id, int pageSize = 30, int page = 1);
        //Task<PagedResultSet<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int page = 1);
        Task<Movie> GetMovieById(int Id);
    }
}
