using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtSkills.Data;
using ArtSkills.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace ArtSkills.Controllers
{
    public class UserWallController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserWallController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        //[HttpGet]
        /*public async Task<string> GetCurrentUserId()
        {
            ApplicationUser usr = await GetCurrentUserAsync();
            return usr?.Id;
        }*/

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            return View(user);
        }

        public async Task<IActionResult> About()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            return View(user);
        }

    }
}