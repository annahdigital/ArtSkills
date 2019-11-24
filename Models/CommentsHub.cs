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
    public class CommentsHub : Hub
    {
        public ApplicationDbContext _context { get; set; }
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsHub(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private string GetUserName()
        {
            return Context.User?.Identity.Name;
        }

        public async System.Threading.Tasks.Task Send(string artId, string comment)
        {
            Art art = _context.Arts.ToList().Find(x => x.Id == artId);
            ApplicationUser user = art.ApplicationUser;
            var userName = GetUserName();
            await Clients.Caller.SendAsync("Receive", "Successfully commented art " + art.Name + "!");
            await Clients.User(user.Id).SendAsync("Notify", userName + " commented your art "
                + art.Name + "!" + "\n Text is: " + comment);
        }
    }
}
