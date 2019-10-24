using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.ComponentModel.DataAnnotations.Schema;

namespace ArtSkills.Models
{
    public class Comment
    {
        public string Id { get; set; }
        public string CommentText { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CommentDate { get; set; } = DateTime.Now;
        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Art Art { get; set; }

        public Comment(ApplicationUser user, Art postedArt, string text)
        {
            this.CommentText = text;
            this.ApplicationUser = user;
            this.Art = postedArt;
            this.CommentDate = DateTime.Now;
        }

        public Comment() { }
    }
}
