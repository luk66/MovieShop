using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
       
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }


        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel requestModel)
        {
            //will execute when user clicks on submit button 
            // save the user registration info to the database
            //receive the model from view
            var newUser = await _userService.RegisterUser(requestModel);
            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            //use this action method to display empty view

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginRequestModel requestModel)
        {
            var user = await _userService.LoginUser(requestModel);
            if (user == null)
            {
                //user password is wrong
                //show message to user saying is wrong
                return View();
            }
            return LocalRedirect("~/");
        }
    }
}
