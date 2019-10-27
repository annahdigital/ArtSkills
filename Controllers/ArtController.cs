﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ArtSkills.Data;
using ArtSkills.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Task = System.Threading.Tasks.Task;


namespace ArtSkills.Controllers
{
    public class ArtController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext applicationDbContext { get; }
        IHostingEnvironment _environment;

        public ArtController(UserManager<ApplicationUser> userManager, ApplicationDbContext context,
IHostingEnvironment environment)
        {
            _userManager = userManager;
            applicationDbContext = context;
            _environment = environment;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public IActionResult Index(string artID)
        {
            return View(applicationDbContext.Arts.ToList());
        }

        public IActionResult Art(string artID)
        {
            Art art = applicationDbContext.Arts.Find(artID);
            return View(art);
        }

        public async Task<IActionResult> ArtsCollection()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            if (user != null)
                return PartialView(user.Arts);
            else
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
        }


        public async Task<IActionResult> DeleteArt(String artID)
        {
            Art art = applicationDbContext.Arts.Find(artID);
            applicationDbContext.Remove(art);
            await applicationDbContext.SaveChangesAsync();
            ApplicationUser user = await GetCurrentUserAsync();
            return RedirectToAction("Index", "UserWall");
        }

        public async Task<IActionResult> LikeArt(String artID)
        {
            Art art = applicationDbContext.Arts.Find(artID);
            ApplicationUser user = await GetCurrentUserAsync();
            var like = new Like(user, art);
            if (user.Likes.ToList().Find(x => x.Art.Id == artID) == null)
            {
                applicationDbContext.Likes.Add(like);
                art.Likes.Add(like);
                user.Likes.Add(like);
                await applicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Art", "Art", new { artID });
        }

        public async Task<IActionResult> UnlikeArt(String artID)
        {
            Art art = applicationDbContext.Arts.Find(artID);
            ApplicationUser user = await GetCurrentUserAsync();
            if (user.Likes.ToList().Find(x => x.Art.Id == artID) != null)
            {
                applicationDbContext.Remove(user.Likes.ToList().Find(x => x.Art.Id == artID));
                await applicationDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Art", "Art", new { artID });
        }


    }
}