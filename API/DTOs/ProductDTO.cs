﻿using API.Entities;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.DTOs
{
    public class ProductDTO
    {
        [JsonIgnore]
        public int ProductId { get; set; }

        [Required]
        public int ForAgeId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        public int BrandId { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        public string? Description { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int Stock { get; set; }

        public List<ImageProductDTO>? ImageProducts { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
