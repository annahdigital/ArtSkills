using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ArtSkills.Data;
using Microsoft.AspNetCore.Mvc;
using ArtSkills.Models;

namespace ArtSkills.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext applicationDbContext { get; }

        public HomeController(ApplicationDbContext context)
        {
            applicationDbContext = context;
        }
        public IActionResult Index()
        {
            /*if (applicationDbContext.Arts.ToList().Count == 0)
                return RedirectToAction("ErrorWhileLoading", "Home");
            else return View(applicationDbContext.Arts.ToList());*/
            return View();
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

        public IActionResult ErrorWhileLoading()
        {
            AppMessage appMessage = new AppMessage("~/Resources/sorry.gif", "Cannot load any posts");
            return View();
        }
    }
}
