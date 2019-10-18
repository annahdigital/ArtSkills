using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtSkills.Models
{
    public class FollowArtist
    {
        public ApplicationUser Follower { get; set; }
        public ApplicationUser Artist { get; set; }
        public string FollowerId { get; set; }
        public string ArtistId { get; set; }
    }
}
