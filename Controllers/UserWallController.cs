using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArtSkills.Data;
using ArtSkills.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Task = System.Threading.Tasks.Task;


namespace ArtSkills.Controllers
{
    public class UserWallController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext applicationDbContext { get; set; }
        public UserWallController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            applicationDbContext = context;
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

        public async Task<IActionResult> AddArt()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddArt(string name, string pictureUrl, string description)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var art = user.PostArt(name, description, pictureUrl);
            await _userManager.UpdateAsync(user);
            applicationDbContext.Arts.Add(art);
            applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}