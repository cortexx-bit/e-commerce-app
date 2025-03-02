using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppAss.Models
{
    public abstract class MenuItem
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
        public string Name { get; set; }

        [StringLength(255, ErrorMessage = "Description cannot exceed 255 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 100, ErrorMessage = "Price must be between £0.01 and £100")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }

        [StringLength(100, ErrorMessage = "Image description cannot exceed 100 characters")]
        public string ImageDescription { get; set; }

        public byte[] ImageData { get; set; }

        [Display(Name="Available")]
        public bool IsAvailable { get; set; }

        [Timestamp]
        public byte[] ConcurrencyToken { get; set; }

        public string Slug { get; set; }

        public string GenerateSlug()
        {
            return Name?.ToLower()
                .Replace(" ", "-")
                .Replace("/", "")
                .Replace("'", "")
                .Replace("&", "and")
                .Replace(",", "")
                .Trim();
        }
    }

}
