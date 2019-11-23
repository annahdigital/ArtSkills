using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ArtSkills.Models;

namespace ArtSkills.ViewModels
{
    public class UserAndArtsViewModel
    {
        public List<Art> Arts { get; set; }
        public ApplicationUser user { get; set; }
        public string Id { get; set; }
    }
}
