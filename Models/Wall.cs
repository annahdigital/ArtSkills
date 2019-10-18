using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtSkills.Models
{
    public class Wall
    {
        public ApplicationUser Artist { get; set; }
        public ICollection<Art> Arts { get; set; }

        public Wall()
        {
            Arts = new List<Art>();
        }
    }
}
