﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MovieShopMVC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using Infrastructure.Services;

namespace MovieShopMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;


        // by default its get
        [HttpGet]
        public IActionResult Index()
        {
            // call movie service class to get list of movie card models
            MovieService service = new MovieService();
            var movieCards = service.GetTop30RevenueMovies();
            //passing data from controller to view, strongly typed models
            // ViewBag and ViewData
            ViewBag.PageTitle = "Top Revenue Movies";
            ViewData["xyz"] = "test data";
            return View(movieCards);
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
