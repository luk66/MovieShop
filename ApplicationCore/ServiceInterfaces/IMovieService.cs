using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.ServiceInterfaces
{
    public interface IMovieService
    {   
        Task<List<MovieCardResponseModel>> GetTop30RatedMovies();

        Task<List<MovieCardResponseModel>> GetTop30RevenueMovies();

        Task<MovieDetailsResponseModel> GetMovieDetails(int id, int? userId = null);

        Task<List<MovieReviewResponseModel>> GetMovieReviewsByMovieId(int id);
        Task<List<MovieCardResponseModel>> GetAllMovies();
        Task<List<MovieCardResponseModel>> GetMoviesByGenreId(int id, int pageSize = 30, int page = 1);

        //Task<List<PurchaseResponseModel>> GetAllPurchasesByMovie(int id);
    }

}
