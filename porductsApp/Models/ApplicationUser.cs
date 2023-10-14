using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace porductsApp.Models
{
    public class ApplicationUser : IdentityUser
    {

        [Required, MaxLength(20)]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [NotMapped]
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
