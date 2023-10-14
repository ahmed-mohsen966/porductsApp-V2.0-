using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace porductsApp.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        [Required, MaxLength(100)]
        public string? Name { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public string? CreatedBy { get; set; }
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        [Required]
        public decimal Price { get; set; }

        public Guid CatalogId { get; set; }

        [ForeignKey(nameof(CatalogId))]

        public virtual Catalog? Catalog { get; set; }

        [NotMapped]
        public IEnumerable<Catalog>? Catalogs { get; set; }

        public string? UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser? User { get; set; }
    }
}
