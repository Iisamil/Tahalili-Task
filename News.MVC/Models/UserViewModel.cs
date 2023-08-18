using System.ComponentModel.DataAnnotations;

namespace News.MVC.Models
{
    public class UserViewModel

    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
