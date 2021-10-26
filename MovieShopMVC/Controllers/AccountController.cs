using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;


namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel requestModel)
        {
            //will execute when user clicks on submit button 
            // save the user registration info to the database
            //receive the model from view

            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            //use this action method to display empty view
            return View();
        }
    }
}
