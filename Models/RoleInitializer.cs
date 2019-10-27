using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ArtSkills.Models
{
    public static class RoleInitializer
    {
        public static async System.Threading.Tasks.Task InitializeAsync(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            string adminEmail = "admin@gmail.com";
            string password = "/Aa123456";

            if (await roleManager.FindByNameAsync("admin") == null)
                await roleManager.CreateAsync(new IdentityRole("admin"));
            if (await roleManager.FindByNameAsync("moderator") == null)
                await roleManager.CreateAsync(new IdentityRole("moderator"));
            if (await roleManager.FindByNameAsync("user") == null)
                await roleManager.CreateAsync(new IdentityRole("user"));

            if (await userManager.FindByNameAsync(adminEmail) == null)
            {
                ApplicationUser admin = new ApplicationUser {Email = adminEmail, UserName = adminEmail};
                await userManager.CreateAsync(admin, password);
                IdentityResult result = await userManager.CreateAsync(admin, password);
                if (result.Succeeded)
                    await userManager.AddToRoleAsync(admin, "admin");
                await userManager.UpdateAsync(admin);
            }
        }
    }
}
