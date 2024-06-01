using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    [Table("Blog")]
    public class Blog
    {
        [Key]
        public int BlogId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public string Author { get; set; }

        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        public DateTime UploadDate { get; set; }

        public DateTime UpdateDate { get; set; }

        public int View {  get; set; }

        public int Like { get; set; }

        public Product Product { get; set; }
    }
}
