using Microsoft.AspNetCore.Identity;

namespace Master.Models
{
    public class SiteUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
