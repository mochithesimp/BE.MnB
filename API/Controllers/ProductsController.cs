﻿using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Validations;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly StoreContext _context;

        public ProductsController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(
                        string? orderBy,
                        string? search,
                        int categoryId,
                        int brandId)
        {
            var querry = _context.Products
                .Sort(orderBy)
                .Search(search)
                .FilterCategory(categoryId, brandId)
                //.FilterBrand(brandId)
                .AsQueryable();

            var list = await querry.ToListAsync();
            var listImage = await _context.ImageProducts.ToListAsync();

            var Images = new List<ImageProductDTO>();
            foreach(var image in listImage)
            {
                ImageProductDTO imageProductDTO = toImageDTO(image);
                Images.Add(imageProductDTO);
            }

            var products = new List<ProductDTO>();
            foreach (var product in list.Where(product => product.IsActive))
            {
                ProductDTO productDTO = toProductDTO(product, Images);
                products.Add(productDTO);
            }
            return Ok(products);
        }

        

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            var listImage = await _context.ImageProducts.ToListAsync();

            var Images = new List<ImageProductDTO>();
            foreach (var image in listImage)
            {
                ImageProductDTO imageProductDTO = toImageDTO(image);
                Images.Add(imageProductDTO);
            }
            if (product == null || !product.IsActive) return NotFound();

            var productDTO = toProductDTO(product, Images);

            return Ok(productDTO);
        }

        [HttpGet("byCategoryID/{CategoryId}")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductByCategory(int CategoryId)
        {
            var list = await _context.Products.ToListAsync();
            var listImage = await _context.ImageProducts.ToListAsync();

            var Images = new List<ImageProductDTO>();
            foreach (var image in listImage)
            {
                ImageProductDTO imageProductDTO = toImageDTO(image);
                Images.Add(imageProductDTO);
            }
            var products = new List<ProductDTO>();

            foreach (var product in list.Where(p => p.CategoryId == CategoryId))
            {
                ProductDTO productDTO = toProductDTO(product, Images);
                products.Add(productDTO);
            }
            if (products.Count > 0) return Ok(products);
            return NotFound();
        }

        [HttpGet("byBrandID/{BrandId}")]
        public async Task<ActionResult<List<ProductDTO>>> GetProductByBrand(int BrandId)
        {
            var list = await _context.Products.ToListAsync();
            var listImage = await _context.ImageProducts.ToListAsync();

            var Images = new List<ImageProductDTO>();
            foreach (var image in listImage)
            {
                ImageProductDTO imageProductDTO = toImageDTO(image);
                Images.Add(imageProductDTO);
            }
            var products = new List<ProductDTO>();

            foreach (var product in list.Where(p => p.BrandId == BrandId))
            {
                ProductDTO productDTO = toProductDTO(product, Images);
                products.Add(productDTO);
            }
            if (products.Count > 0) return Ok(products);
            return NotFound();
        }

        private static ProductDTO toProductDTO(Product? product, List<ImageProductDTO> listImages)
        {
            ProductDTO productDTO = new ProductDTO();
            productDTO.ProductId = product.ProductId;
            productDTO.Name = product.Name;
            productDTO.Description = product.Description;
            productDTO.Price = product.Price;
            productDTO.Stock = product.Stock;
            productDTO.CategoryId = product.CategoryId;
            productDTO.BrandId = product.BrandId;
            productDTO.ForAgeId = product.ForAgeId;
            productDTO.IsActive = product.IsActive;
            productDTO.ImageProducts = new List<ImageProductDTO>();
            foreach (var image in listImages)
            {
                if (product.ProductId == image.ProductId)
                {
                    productDTO.ImageProducts.Add(image);
                }
            }
            return productDTO;
        }

        private static ImageProductDTO toImageDTO(ImageProduct? image)
        {
            ImageProductDTO imageProductDTO = new ImageProductDTO();
            imageProductDTO.ImageId = image.ImageId;
            imageProductDTO.ProductId = image.ProductId;
            imageProductDTO.ImageUrl = image.ImageUrl;
            return imageProductDTO;
        }

    }
}
