using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ArtSkills.Models
{
    public class Art
    {
        public string Id { get; set; }
        public string ImagePath { get; set; }
        public string Name { get; set; }        
        public string UserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public string Description { get; set; }
        public DateTime PublishDate { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }

        public Art()
        {

        }

        public Art(string name, ApplicationUser author, string description, string pictureUrl)
        {
            this.Name = name;
            this.UserId = author.Id;
            this.ImagePath = pictureUrl;
            this.Description = description;
            this.PublishDate = DateTime.Now;
            this.Comments = new List<Comment>();
        }

        public void EditInfo(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
