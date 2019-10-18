using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;

namespace ArtSkills.Models
{
    public class ApplicationUser: Microsoft.AspNetCore.Identity.IdentityUser
    {
        public string artistNickname { get; set; }
        public string Name {get; set;}
        public string Surname {get; set;}
        public DateTime RegistrationDate {get; set;}
        public DateTime DateOfBirth {get; set;}
        public string About {get; set;}
        public string UserPic {get; set;}
        public ICollection<TaskList> Completed;
        public ICollection<TaskList> InProgress;
        public ICollection<Art> Arts;

        public double Statistics => InProgress.Count == 0 ? 0 : Completed.Count / InProgress.Count * 100; 

        public ICollection<FollowArtist> FollowedBy;
        public ICollection<FollowArtist> Following;

        public ApplicationUser()
        {
            this.RegistrationDate = DateTime.Now;
            this.Completed = new List<TaskList>();
            this.InProgress = new List<TaskList>();
            this.Arts = new List<Art>();
            this.FollowedBy = new List<FollowArtist>();
            this.Following = new List<FollowArtist>();
        }
        public ApplicationUser(string name)
        {
            this.UserName = name;
            this.RegistrationDate = DateTime.Now;
            this.Completed = new List<TaskList>();
            this.InProgress = new List<TaskList>();
            this.Arts = new List<Art>();
            this.FollowedBy = new List<FollowArtist>();
            this.Following = new List<FollowArtist>();
        }

        public void PostArt(string name, string description)
        {
            this.Arts.Add(new Art(name, this, description));
        }


    }
}
