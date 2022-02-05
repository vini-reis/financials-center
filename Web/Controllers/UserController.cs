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
    public class UserController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public UserController(ILogger<HomeController> logger, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Authorize]
        [Route("{controller}")]
        public ActionResult Users ()
        {
            var viewModel = new UserViewModel()
            {
                Users = _userManager.Users.ToList(),
            };

            return View("Users", viewModel);
        }

        [HttpGet]
        [Authorize]
        [Route("{controller}/{id}")]
        public async Task<ActionResult> Users(string id)
        {
            var result = new Result<User>();
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                result.Failed("User not found");
            else
                result.Succeeded(user);

            return Json(result);
        }

        [HttpPost]
        [Authorize]
        [Route("{controller}/{id}")]
        public async Task<ActionResult> Update(UserViewModel viewModel)
        {
            var result = new Result<User>();
            var user = await _userManager.FindByIdAsync(viewModel.Id) ?? await _userManager.FindByIdAsync(viewModel.Email);

            if (user == null)
                result.Failed("User not found");
            else if (!ModelState.IsValid)
                result.Failed(ModelState.Values.First(e => e.Errors.Any()).Errors.First().ErrorMessage, null);
            else
            {
                user.UserName = viewModel.FirstName;
                user.LastName = viewModel.LastName;
                user.Email = viewModel.Email;
                user.Tag = viewModel.Tag;

                if (viewModel.ResetPassword)
                {
                    var resetResult = await _userManager.RemovePasswordAsync(user);

                    if (!resetResult.Succeeded)
                        result.Failed(resetResult.Errors.First().Description);
                }

                if (string.IsNullOrEmpty(result.Message))
                {
                    var updateResult = await _userManager.UpdateAsync(user);

                    if (!updateResult.Succeeded)
                        result.Failed(updateResult.Errors.First().Description);
                    else
                        result.Succeeded(user);
                }
            }

            return Json(result);
        }

        [HttpGet]
        [Authorize]
        [Route("Role/")]
        public ActionResult Roles()
        {
            var viewModel = new RoleViewModel()
            {
                Roles = _roleManager.Roles.ToList(),
            };

            return View("Roles", viewModel);
        }

        [HttpPost]
        [Authorize]
        [Route("Role/")]
        public async Task<ActionResult> CreateRole (RoleViewModel viewModel)
        {
            var result = new Result<object>();

            if (!ModelState.IsValid)
                result.Failed(ModelState.Values.First().Errors.First().ErrorMessage);
            else
            {
                var newRole = new Role() { Name = viewModel.Name };
                var roleResult = await _roleManager.CreateAsync(newRole);

                if (roleResult.Succeeded)
                    result.Succeeded(newRole);
                else
                    result.Failed(roleResult.Errors.First().Description);
            }

            return Json(result);
        }

    }
}
