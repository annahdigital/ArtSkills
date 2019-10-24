using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using ArtSkills.Data;
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

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime RegistrationDate { get; set; } = DateTime.Now;
        public DateTime DateOfBirth {get; set;}
        public string About {get; set;}
        public string UserPic {get; set;}
        public virtual ICollection<TaskList> TaskLists { get; set; }
        public virtual ICollection<Art> Arts { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        [NotMapped]
        public double Statistics
        {
            get
            {
                return 0;
            }
        }
        //InProgress.Count == 0 ? 0 : Completed.Count / InProgress.Count * 100; 

        public virtual ICollection<FollowArtist> FollowedBy { get; set; }
        public virtual ICollection<FollowArtist> Following { get; set; }

        public ApplicationUser()
        {
            //this.RegistrationDate = DateTime.Now;
            /*this.TaskLists = new List<TaskList>();
            this.Arts = new List<Art>();
            this.FollowedBy = new List<FollowArtist>();
            this.Following = new List<FollowArtist>();*/

        }
        public ApplicationUser(string name)
        {
            this.UserName = name;
            this.RegistrationDate = DateTime.Now;
            /*this.TaskLists = new List<TaskList>();
            this.Arts = new List<Art>();
            this.FollowedBy = new List<FollowArtist>();
            this.Following = new List<FollowArtist>();*/
        }

        public Art PostArt(string name, string description, string pictureUrl)
        {
            var art = new Art(name, this, description, pictureUrl);
            this.Arts.Add(art);
            return art;
        }


    }
}
