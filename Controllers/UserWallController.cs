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
using ArtSkills.ViewModels;


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

        public async Task<IActionResult> Followers(string Id)
        {
            ApplicationUser user = applicationDbContext.Users.ToList().Find(userr => userr.Id == Id);
            var model = new UserAndArtsViewModel {user = user};
            return View(model);
        }

        public async Task<IActionResult> FollowUser(string Id)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            ApplicationUser artist = applicationDbContext.Users.ToList().Find(userr => userr.Id == Id);
            var FollowForm = new FollowArtist(artist, user);
            applicationDbContext.FollowArtists.Add(FollowForm);
            applicationDbContext.SaveChanges();
            return RedirectToAction("Index", new { Id });
        }

        public async Task<IActionResult> UnfollowUser(string Id)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var FollowForm = user.Following.ToList().Find(x => x.FollowerId == user.Id);
            applicationDbContext.FollowArtists.Remove(FollowForm);
            applicationDbContext.SaveChanges();
            return RedirectToAction("Index", new { Id });
        }

        public async Task<IActionResult> Index(string Id, int pageNumber = 0)
        {
            if (Id == null)
            {
                ApplicationUser user = await GetCurrentUserAsync();
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return PartialView("~/Views/Art/ArtsCollection.cshtml", artsForThePage(pageNumber, user.Id));
                }
                var model = new UserAndArtsViewModel { Arts = artsForThePage(pageNumber, user.Id), user = user, Id = Id };
                return View(model);
            }
            else
            {
                ApplicationUser user = applicationDbContext.Users.ToList().Find(userr => userr.Id == Id);
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return PartialView("~/Views/Art/ArtsCollection.cshtml", artsForThePage(pageNumber, Id));
                }
                var model = new UserAndArtsViewModel { Arts = artsForThePage(pageNumber, user.Id), user = user, Id = Id };
                return View(model);
            }
        }

        const int artsCount = 5;

        public List<Art> artsForThePage(int pageNumber, string userId)
        {
            var arts = applicationDbContext.Users.ToList().Find(x => x.Id == userId).Arts.OrderByDescending(a => a.PublishDate).Skip(pageNumber * artsCount).Take(artsCount).ToList();
            return arts;
        }

        public async Task<IActionResult> About(string Id)
        {
            ApplicationUser user = applicationDbContext.Users.ToList().Find(userr => userr.Id == Id);
            var model = new UserAndArtsViewModel { user = user };
            return View(model);
        }

        public async Task<IActionResult> UploadUserPic()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var model = new UserAndArtsViewModel { user = user };
            return View(model);
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
            var model = new UserAndArtsViewModel { user = user };
            return View(model);
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

        public async Task<IActionResult> AddTaskList()
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var model = new UserAndArtsViewModel { user = user };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> AddTaskList(string name, string description)
        {
            ApplicationUser user = await GetCurrentUserAsync();
            var taskList = new TaskList(name, user, description);
            applicationDbContext.TaskLists.Add(taskList);
            applicationDbContext.SaveChanges();
            var list = user.TaskLists.ToList().Find(x => x.Name == name && x.Description == description);
            return RedirectToAction("TaskList", "TaskList", new { taskListId = list.Id });
        }

        public async Task<IActionResult> TaskListIndex(string Id)
        {
            if (Id == null)
            {
                ApplicationUser user = await GetCurrentUserAsync();
                var model = new UserAndArtsViewModel { user = user };
                return View(model);
            }
            else
            {
                ApplicationUser user = applicationDbContext.Users.ToList().Find(userr => userr.Id == Id);
                var model = new UserAndArtsViewModel { user = user };
                return View(model);
            }
        }
    }
}