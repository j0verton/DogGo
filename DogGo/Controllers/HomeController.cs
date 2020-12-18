using DogGo.Models;
using DogGo.Repositories.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DogGo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (!User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Owners");
            };

            int userId = ControllerUtils.GetCurrentUserId(User);
            string role = ControllerUtils.GetCurrentUserRole(User);
            if (role == "DogWalker")
            { 
             //redirect to a walker page
            }
            if (role == "DogOwner")
            {
                return RedirectToAction("Details", "Owners", new { id =userId });
            }

            //THis lne should never execute
            return RedirectToAction("Index", "Walkers");
        }

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
