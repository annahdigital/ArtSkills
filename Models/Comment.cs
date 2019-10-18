using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace ArtSkills.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentDate { get; set; }
        public ApplicationUser User { get; set; }
        public Art PostedArt { get; set; }

        public Comment(ApplicationUser user, Art postedArt, string text)
        {
            this.CommentText = text;
            this.User = user;
            this.PostedArt = postedArt;
            this.CommentDate = DateTime.Now;
        }
    }
}
