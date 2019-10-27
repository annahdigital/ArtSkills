using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtSkills.Models
{
    public class Like
    {
        public string Id { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual Art Art { get; set; }

        public Like()
        {

        }

        public Like(ApplicationUser appUser, Art art)
        {
            ApplicationUser = appUser;
            Art = art;
        }
    }
}
