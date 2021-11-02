using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        //create an api method that shows top 30 revenue movies
        // so that my SPA, IOS, ANDRIOD app show those movies in the home screen
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("{id:int}")]
        //localhost/api/movies/{}
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                return NotFound($"No movie found with id: {id}");
            }
            return Ok(movie);
        }

        // craete the api method that shows top 30 movies, json data
        [HttpGet]
        [Route("toprevenue")]
        //Attribute base routing
        //localhost/API/movies/toprevenue
        // API
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetTop30RevenueMovies();

            //JSON data and Http Status Code
            if (!movies.Any())
            {
                // 404
                return NotFound("No Movies Found");
            }
            return Ok(movies);
            
        }

        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTop30RatedMovies();
            if (!movies.Any())
            {
                return NotFound("No Movies Found");
            }
            return Ok(movies);
        }

    }
}
