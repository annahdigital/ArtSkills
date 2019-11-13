using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using ArtSkills.Models;
using ArtSkills.Data;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Web;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Task = System.Threading.Tasks.Task;

namespace ArtSkills.Components
{
    public class UserLikeViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserLikeViewComponent(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public async Task<IViewComponentResult> InvokeAsync(Art art)
        {
            ApplicationUser user = await GetCurrentUserAsync();

            if (user == null)
            {
                return View("DisabledLikeButton", art);
            }
            else
            {
                if (user.Id == art.ApplicationUser.Id)
                    return View("DisabledLikeButton", art);
                else return View("LikableButton", art);
            }
        }
    }
}
