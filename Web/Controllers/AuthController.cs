using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Data.Entities.Authentication;
using Web.ViewModels;
using Data.Entities.Helpers;
using System.ComponentModel.DataAnnotations;

namespace Web.Controllers
{
    public class AuthController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public ViewResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public async Task<ActionResult> Login(string Email, string Password, string returnUrl)
        {
            var result = new Result<object>();

            if (string.IsNullOrEmpty(Email))
                ModelState.AddModelError(nameof(Email), "E-mail is required");
            else if (string.IsNullOrEmpty(Password))
                ModelState.AddModelError(nameof(Password), "Password is required");
            else
            {
                var user = await _userManager.FindByEmailAsync(Email);

                if (user != null)
                {
                    var passwordResult = await _signInManager.PasswordSignInAsync(user, Password, false, false);

                    if (passwordResult.Succeeded)
                        return Redirect(!string.IsNullOrEmpty(returnUrl) ? returnUrl : "/");
                }

                ModelState.AddModelError(nameof(Email), "Login failed");
            }

            return Json(result);
        }

        [HttpPost]
        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
                await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<ActionResult> Register(UserViewModel viewModel)
        {
            var result = new Result<object>();

            if (!ModelState.IsValid)
                result.Message = ModelState.Values.First(e => e.Errors.Any()).Errors.First().ErrorMessage;
            else 
            {
                var user = new User()
                {
                    UserName = viewModel.FirstName,
                    LastName = viewModel.LastName,
                    Email = viewModel.Email,
                    Tag = viewModel.Tag,
                };

                var saveResult = await _userManager.CreateAsync(user, viewModel.Password);

                if (saveResult.Succeeded)
                    result.Succeeded(user);
                else
                    result.Failed("There was a problem while saving this user", $"{saveResult.Errors.First().Code}: {saveResult.Errors.First().Description}");
            }

            return Json(result);
        }

    }
}
