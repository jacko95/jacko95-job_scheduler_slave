using System.ComponentModel.DataAnnotations;

namespace Master.ViewModels
{
    public class SiteUserViewModel
    {
        public string Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Roles { get; set; }
    }
}
