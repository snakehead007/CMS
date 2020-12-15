using CMS.Models;
using CMS.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserService _userService;


        public AuthController(ILogger<HomeController> logger, UserService userService)
        {
            _logger = logger;
            this._userService = userService;
        }

        [HttpGet]
        public IActionResult Login() {

            if (_userService.IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            if (_userService.IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (_userService.IsUserLoggedIn()) 
            {
                await _userService.LogoutAsync();
            }

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (_userService.IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            if (!ModelState.IsValid) {
                return View(model);
            }

            if (!await _userService.LoginAsync(model.Username, model.Password)) 
            {
                ModelState.AddModelError(nameof(model.Username), "Username or password is incorrect.");
                return View(model);
            }

            //if everything goes according to plan
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (_userService.IsUserLoggedIn())
            {
                return RedirectToAction("Index", "Home");
            }

            if (model.Password != model.RepeatPassword){
                ModelState.AddModelError(nameof(model.Password), "Passwords do not match.");
            }

            if (!ModelState.IsValid){
                return View(model);
            }

            if (await _userService.DoesUserAlreadyExistAsync(model.Username)) 
            {
                ModelState.AddModelError(nameof(model.Username), "Username already exists.");
                return View(model);
            }

            if (!await _userService.RegisterAsync(model.Username, model.Password))
            {
                ModelState.AddModelError(nameof(model.Username), "User could not be registered.");
                return View(model);
            }
            //if everything goes according to plan
            return RedirectToAction("Index", "Home");
        }
    }
}
