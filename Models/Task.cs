using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtSkills.Models
{
    public class Task
    {
        public bool Status { get; set; }
        public string Name { get; set; }

        public Task(string name)
        {
            this.Name = name;
        }
    }
}
