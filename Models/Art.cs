using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtSkills.Models
{
    public class Art
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }
        public string AuthorId { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public List<ApplicationUser> UserLikes;
        public List<Comment> Comments;

        public Art()
        {

        }

        public Art(string name, ApplicationUser author, string description, string pictureUrl)
        {
            this.Name = name;
            this.AuthorId = author.Id;
            this.ImagePath = pictureUrl;
            this.Description = description;
            this.PublishDate = DateTime.Now;
            this.UserLikes = new List<ApplicationUser>();
            this.Comments = new List<Comment>();
        }

        public void LikeArt(ApplicationUser user)
        {
            UserLikes.Add(user);
        }

        public void UnlikeArt(ApplicationUser user)
        {
            UserLikes.Remove(user);
        }

        public void EditInfo(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
