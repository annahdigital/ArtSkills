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
    public partial class InfoSettingsModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public InfoSettingsModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "Nickname")]
            public string ArtistNickName { get; set; }

            [Display(Name = "Name")]
            public string Name { get; set; }

            [Display(Name = "Surname")]
            public string Surname { get; set; }

            [Display(Name = "About me")]
            public string About { get; set; }

            [DataType(DataType.Date)]
            [Display(Name = "Birth date")]
            public DateTime BirthDate { get; set; }
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
                ArtistNickName = user.artistNickname,
                Name = user.Name,
                Surname = user.Surname,
                About = user.About,
                BirthDate = user.DateOfBirth
            };

            return Page();
        }

        public async Task<IActionResult> updateUser(ApplicationUser user)
        {
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var userId = await _userManager.GetUserIdAsync(user);
                throw new InvalidOperationException($"Unexpected error occurred for user with ID '{userId}'.");
            }
            return null;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (Input.ArtistNickName != user.artistNickname)
            {
                user.artistNickname = Input.ArtistNickName;
                await updateUser(user);
            }

            if (Input.About != user.About)
            {
                user.About = Input.About;
                await updateUser(user);
            }

            if (Input.Name != user.Name)
            {
                user.Name = Input.Name;
                await updateUser(user);
            }

            if (Input.Surname != user.Surname)
            {
                user.Surname = Input.Surname;
                await updateUser(user);
            }

            if (Input.BirthDate != user.DateOfBirth)
            {
                user.DateOfBirth = Input.BirthDate;
                await updateUser(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

    }
}
