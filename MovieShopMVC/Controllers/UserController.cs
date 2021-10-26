using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
{
    public class UserController : Controller
    {
        // all the action methods in User Controller should work only when user is Authenticated 
        [HttpPost]
        public async Task<IActionResult> Purchase()
        {
            // purchase a movie when user clicks BUY button on movieDetails page
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Favorite()
        {
            // favorite a movie when user clicks BUY button on movieDetails page
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Review()
        {
            // review a movie when user clicks BUY button on movieDetails page
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Purchases(int id)
        {
            //get all the movies purchased by user => List<MovieCard>
            //reuse movieCard
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Favorites(int id)
        {
            //reuse movieCard
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Reviews(int id)
        {
            return View();
        }
    }
}
