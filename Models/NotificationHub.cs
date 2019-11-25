using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ArtSkills.Data;
using System.Net;
using Microsoft.AspNetCore.Identity;

namespace ArtSkills.Models
{
    [Authorize]
    public class NotificationHub : Hub
    {
        public ApplicationDbContext _context { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;

        public NotificationHub(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private string GetUserName()
        {
            return Context.User?.Identity.Name;
        }

        public async System.Threading.Tasks.Task Like(string artId)
        {
            Art art = _context.Arts.ToList().Find(x => x.Id == artId);
            ApplicationUser user = art.ApplicationUser;
            var userName = GetUserName();
            await Clients.User(user.Id).SendAsync("Notify", userName + " liked your art "
               + art.Name + "!");
        }

        public async System.Threading.Tasks.Task Follow(string Id)
        {
            var userName = GetUserName();
            await Clients.User(Id).SendAsync("Notify", userName + " just started following you!");
        }
    }
}
