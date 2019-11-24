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
    public class SearchController : Controller
    {
        private ApplicationDbContext applicationDbContext { get; }

        public SearchController(ApplicationDbContext context)
        {
            applicationDbContext = context;
        }

        [HttpPost]
        public IActionResult ArtistSearch(string name)
        {
            var artists = applicationDbContext.Users.Where(a => a.artistNickname.Contains(name) || a.UserName.Contains(name)).ToList();
            return View(artists);
        }
    }
}
