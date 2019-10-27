using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtSkills.Models
{
    public class FollowArtist
    {
        public string Id { get; set; }
        public virtual ApplicationUser Follower { get; set; }
        public virtual ApplicationUser Artist { get; set; }
        public string FollowerId { get; set; }
        public string ArtistId { get; set; }

        public FollowArtist() { }

        public FollowArtist(ApplicationUser artist, ApplicationUser follower)
        {
            Follower = follower;
            Artist = artist;
            FollowerId = follower.Id;
            ArtistId = artist.Id;
        }
    }
}
