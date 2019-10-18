using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtSkills.Models
{
    public class AppMessage
    {
        public string MessagePicture { get; set; }
        public string MessageText { get; set; }

        public AppMessage(string url, string text)
        {
            MessagePicture = url;
            MessageText = text;
        }
    }
}
