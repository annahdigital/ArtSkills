using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ArtSkills.Data;
using Microsoft.AspNetCore.Mvc;
using ArtSkills.Models;
using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;

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
            return View(applicationDbContext.Arts.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
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
