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
    public class CommentController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private ApplicationDbContext applicationDbContext { get; }

        public CommentController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            applicationDbContext = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public IActionResult Comment(string Id)
        {
            Comment comment = applicationDbContext.Comments.Find(Id);
            return View(comment);
        }

        public IActionResult DeleteComment(String artID, String commentId)
        {
            Comment comment = applicationDbContext.Comments.Find(commentId);
            applicationDbContext.Remove(comment);
            applicationDbContext.SaveChanges();
            return RedirectToAction("Art", "Art", new { artID });
        }


    }
}
