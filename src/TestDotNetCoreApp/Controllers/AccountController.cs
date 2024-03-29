﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestDotNetCoreApp.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TestDotNetCoreApp.Controllers
{
    [Produces("application/json")]
    [Route("api/account")]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;


        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            this._signInManager = signInManager;
            _userManager = userManager;
        }

        [Route("login"), HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginModel loginInfo)
        {
            if (ModelState.IsValid)
            {
                var result = loginInfo.UserName.Equals("test1") && loginInfo.Password.Equals("test2");
                if (result)
                {
                    var user = await _userManager.FindByNameAsync(loginInfo.UserName);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    await _signInManager.PasswordSignInAsync(loginInfo.UserName, loginInfo.Password, isPersistent: false, lockoutOnFailure: false);
                    return Json("OK");
                }
                
            }
            ModelState.AddModelError("login", "Неверная комбинация логина и пароля");
            return new BadRequestObjectResult(ModelState);
        }

        [Route("logoff"), HttpPost]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();

            return Json("OK");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel login, string returnUrl = null)
        {
            var signInStatus = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, false, false);
            if (signInStatus.Succeeded)
            {
                return Redirect("/main");
            }
            ModelState.AddModelError("", "Неверная комбинация логина и пароля");
            return View();
        }
        
    }
}
