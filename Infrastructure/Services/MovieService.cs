using ApplicationCore.Models;
using ApplicationCore.RepositoryInterfaces;
using ApplicationCore.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class MovieService: IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public List<MovieCardResponseModel> GetTop30RevenueMovies()
        {
            // method should call movie repo and get the data from movie table
            //var movieCards = new List<MovieCardResponseModel> { 

            //    new MovieCardResponseModel {Id = 1, Title = "Inception", PosterUrl = "https://image.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg"},
            //    new MovieCardResponseModel {Id = 2, Title ="Interstellar", PosterUrl = "https://image.tmdb.org/t/p/w342//gEU2QniE6E77NI6lCU6MxlNBvIx.jpg"},
            //    new MovieCardResponseModel {Id = 3, Title ="The Dark Knight", PosterUrl = "https://image.tmdb.org/t/p/w342//qJ2tW6WMUDux911r6m7haRef0WH.jpg" }
            //};
            //return movieCards;

            //calling MovieRepository with DI based on IMovieRepository
            var movies = _movieRepository.GetTop30RevenueMovies();
            var movieCards = new List<MovieCardResponseModel>();

            foreach(var movie in movies)
            {
                movieCards.Add(new MovieCardResponseModel
                {
                    Id = movie.Id,
                    PosterUrl = movie.PosterUrl,
                    Title = movie.Title
                });
            }
            return movieCards;

        }

    }
}
