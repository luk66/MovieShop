using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ICurrentUserService _currentUserService;

        public MoviesController(IMovieService movieService, ICurrentUserService currentUserSerivce)
        {
            _movieService = movieService;
            _currentUserService = currentUserSerivce;
        }
        // localhost:24234/movies/details/343
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var userId = _currentUserService.UserId;
            var movieDetails = await _movieService.GetMovieDetails(id, userId);
            return View(movieDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Reviews(int id)
        {
            var movieReviews = await _movieService.GetMovieReviewsByMovieId(id);
            return View(movieReviews);
        }

        //[HttpGet]
        //public async Task<IActionResult> Purchase(int id)
        //{
        //    var moviePurchase = await _movieService.GetAllPurchasesByMovie(id);
        //    return View(moviePurchase);
        //}
    }
}
