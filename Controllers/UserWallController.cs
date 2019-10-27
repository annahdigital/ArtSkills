using System;
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
    public class UserWallController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext applicationDbContext { get; set; }
        IHostingEnvironment _environment;

        public UserWallController(UserManager<ApplicationUser> userManager, ApplicationDbContext context,
            IHostingEnvironment environment)
        {
            _userManager = userManager;
            applicationDbContext = context;
            _environment = environment;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        /*public async Task<IActionResult> Index()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            return View(user);
        }*/

        public async Task<IActionResult> Index(string Id)
        {
            if (Id == null)
            {
                ApplicationUser user = await GetCurrentUserAsync();
                return View(user);
            }
            else
            {
                ApplicationUser user = applicationDbContext.Users.ToList().Find(userr => userr.Id == Id);
                return View(user);
            }
        }

        public async Task<IActionResult> About()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            return View(user);
        }

        public async Task<IActionResult> UploadUserPic()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UploadUserPic(IFormFile pictureUrl)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            string path = "/Userpics/usr" + user.Id + ".jpg";
            using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
            {
                await pictureUrl.CopyToAsync(fileStream);
            }
            user.UserPic = path;  
            await _userManager.UpdateAsync(user);
            applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddArt()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddArt(string name, IFormFile pictureUrl, string description)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            string path = "/Arts/art" + user.Id + applicationDbContext.Arts.Count().ToString() + ".jpg";
            using (var fileStream = new FileStream(_environment.WebRootPath + path, FileMode.Create))
            {
                await pictureUrl.CopyToAsync(fileStream);
            }
            var art = user.PostArt(name, description, path);
            await _userManager.UpdateAsync(user);
            //applicationDbContext.Arts.Add(art);
            applicationDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        /*public async Task<IActionResult> DeleteArt(String artID)
        {
            Art art = applicationDbContext.Arts.Find(artID);
            applicationDbContext.Remove(art);
            await applicationDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }*/


    }
}