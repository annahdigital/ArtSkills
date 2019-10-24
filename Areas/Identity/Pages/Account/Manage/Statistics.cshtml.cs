using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using ArtSkills.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ArtSkills.Areas.Identity.Pages.Account.Manage
{
    public partial class StatisticsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public StatisticsModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [DataType(DataType.Date)]
            [Display(Name = "Registration date")]
            public DateTime RegistrationDate { get; set; }

            /*[Display(Name = "Completed tasks count")]
            public int Completed { get; set; }

            [Display(Name = "Tasks in progress count")]
            public int InProgress { get; set; }*/

            [Display(Name = "Tasks")]
            public int Tasklists { get; set; }

            [Display(Name = "Arts count")]
            public int Arts { get; set; }

            [Display(Name = "Percentage")]
            public double Statistics { get; set; }

            [Display(Name = "Followers count")]
            public int Followers { get; set; }

            [Display(Name = "Following count")]
            public int Following { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Input = new InputModel
            {
                RegistrationDate = user.RegistrationDate,
                /*Completed = user.TaskLists.Count,
                InProgress = user.TaskLists.Count,*/
                Tasklists = user.TaskLists.Count,
                Arts = user.Arts.Count,
                Statistics = user.Statistics,
                Followers = user.FollowedBy.Count,
                Following = user.Following.Count
            };

            return Page();
        }
    }
}
