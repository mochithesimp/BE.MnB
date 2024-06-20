using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.DTOs
{
    public class BlogDTO
    {
        public int BlogId { get; set; }
        public string Title { get; set; }

        public string Content { get; set; }

        public string? ImageUrl { get; set; }

        public IFormFile? ImageFile { get; set; }

        public string Author { get; set; }

        public int ProductId { get; set; }

        public DateTime? UploadDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? View { get; set; }

        public int? Like { get; set; }
    }
}
