﻿using ApplicationCore.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieShopMVC.Controllers
    
{
    public class UserController : Controller
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IUserService _userService;
        public UserController(ICurrentUserService currentUserSerivce, IUserService userService)
        {
            _currentUserService = currentUserSerivce;
            _userService = userService;
        }

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
        //Filters in ASP.NET
        //[Authorize]
        //the alt page set in startup, services.AddAuthentication
        public async Task<IActionResult> Purchases()
        {
            //get all the movies purchased by user => List<MovieCard>
            //reuse movieCard
            //var userIdentity = this.User.Identity;
            //if (userIdentity != null && userIdentity.IsAuthenticated)
            //{
            //    return View();
            //}
            //RedirectToAction("Login", "Action");
            //int userId = Convert.ToInt32((HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));
            var userId = _currentUserService.UserId;
            var userPurchases = await _userService.GetAllPurchasesForUser(userId);
            return View(userPurchases);
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
            var movieReviews = await _userService.GetAllReviewsByUser(id);
            return View(movieReviews);
        }
    }
}
