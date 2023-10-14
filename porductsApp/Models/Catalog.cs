using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace porductsApp.Models
{
    public class Catalog
    {
        public Guid Id { get; set; }

        [Required, MaxLength(150)]
        public string? Name { get; set; }

        [NotMapped]
        public ICollection<Product> Products { get; set; } = new HashSet<Product>();
    }
}
