using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ArtSkills.Data;
using Microsoft.AspNetCore.Mvc;
using ArtSkills.Models;
using ArtSkills.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace ArtSkills.Controllers
{
    [Authorize(Roles = "admin, moderator")]
    public class AdministrationController : Controller
    {
        RoleManager<IdentityRole> _roleManager;
        UserManager<ApplicationUser> _userManager;
        private ApplicationUser currentUser;
        public ApplicationDbContext applicationDbContext;

        public AdministrationController(RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            applicationDbContext = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public IActionResult Index()
        {
            return View(_userManager.Users.ToList());
        }

        public async Task<IActionResult> EditUserRoles(string userId)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    UserRoles = userRoles,
                    AllRoles = allRoles
                };
                return View(model);
            }
            else return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> EditUserRoles(string userId, List<string> roles)
        {
            currentUser = await GetCurrentUserAsync();
            ApplicationUser user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                if (currentUser.UserRole == "moderator" && roles[0] == "admin")
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var userRoles = await _userManager.GetRolesAsync(user);
                    await _userManager.RemoveFromRolesAsync(user, new List<string>() {user.UserRole});
                    await _userManager.AddToRolesAsync(user, new List<string>() {roles[0]});
                    user.UserRole = roles[0];
                    await _userManager.UpdateAsync(user);
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}

