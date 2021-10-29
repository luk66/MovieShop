using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Models;
using ApplicationCore.ServiceInterfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;

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

            //check the model is valid
            if (!ModelState.IsValid)
            {
                return View();
            }

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
                // username/pwd is wrong
                // prompt message to user

                return View();
            }

            // create cookies 
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,user.Email ),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToShortDateString()),
                new Claim("FullName", user.FirstName + " " + user.LastName)
            };

            // Identity
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            // print out our Card
            // creating the cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));
            //HttpContext.Response.Cookies.Append("test", user.LastName);
            return LocalRedirect("~/");
            // logout=> 
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // invalidate the cookie 
            await HttpContext.SignOutAsync();
            // re-direct to Login
            return RedirectToAction("Login");
        }
    }
}
