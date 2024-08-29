using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlavorHub.Models
{
    public class ProfilePictureMessage
    {
        public string ProfilePictureUrl { get; }
        public ProfilePictureMessage(string profilePictureUrl)
        {
            ProfilePictureUrl = profilePictureUrl;
        }
    }
}
