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
        const int artsCount = 5;

        public HomeController(ApplicationDbContext context)
        {
            applicationDbContext = context;
        }

        public IActionResult Index(int pageNumber = 0)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("~/Views/Art/ArtsCollection.cshtml", artsForThePage(pageNumber));
            }
            return View(artsForThePage(pageNumber));
        }

        public List<Art> artsForThePage(int pageNumber)
        {
            var arts = applicationDbContext.Arts.OrderByDescending(a => a.PublishDate).Skip(pageNumber * artsCount).Take(artsCount).ToList();
            return arts;
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
            return View();
        }
    }
}
