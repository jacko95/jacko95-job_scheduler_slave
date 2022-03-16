using System.ComponentModel.DataAnnotations;

namespace Master.ViewModels
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string Password { get; set; }
    }
}
