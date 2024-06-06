using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Entities
{
    [Table("UserBlogView")]
    public class UserBlogView
    {
        [Key]
        public int UserBlogViewId { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User User { get; set; }

        [ForeignKey("BlogId")]
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
        public int Like { get; set; }
    }
}
