using GestiuneSaliNET7.Data;
using GestiuneSaliNET7.Models;
using GestiuneSaliNET7.Repository;
using GestiuneSaliNET7.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace GestiuneSaliNET7.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly ILogin _loguser;
        public LoginController(ApplicationDBContext context,
                               ILogin loguser)
        {
            _context = context;
            _loguser = loguser;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string email, string password)
        {
            var issuccess = _loguser.AuthenticateUser(email, password.Hash());

            if (issuccess.Result != null)
            {
                ViewBag.username = string.Format("Successfully logged-in", email);

                TempData["email"] = email;
                return RedirectToAction("Index", "Layout", new { area = "Home"});
            }
            else
            {
                ViewBag.username = string.Format("Login Failed ", email);
                return View();
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([Bind("Name, Email, Password")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                userModel.Password = userModel.Password.Hash();
                userModel.Role = (int)Roles.User;
                _context.Add(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email, Password")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                var issuccess = _loguser.AuthenticateUser(userModel.Email, userModel.Password.Hash());


                if (issuccess.Result != null)
                {
                    ViewBag.username = string.Format("Successfully logged-in", userModel.Email);

                    TempData["email"] = userModel.Email;
                    return RedirectToAction("Index", "Layout");
                }
                else
                {
                    ViewBag.username = string.Format("Login Failed ", userModel.Email);
                    return View();
                }
            }
            return View();
        }
    }
}
