﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
    [Table("Review")]
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        public int UserId { get; set; }

        public int? OrderDetailId { get; set; }

        [Required]
        [ForeignKey("ProductId")]
        public int ProductId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Rating { get; set; }

        public string Comment { get; set; }

        public bool IsRated { get; set; }

        public User? User { get; set; }
        public OrderDetail OrderDetail { get; set; }

        public Product Product { get; set; }
    }
}
