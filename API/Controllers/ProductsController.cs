using API.Data;
using API.DTOs;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly StoreContext _context;
        private readonly IConfiguration _configuration;

        public ProductsController(StoreContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetProducts(
                        string? orderBy,
                        string? search,
                        int categoryId,
                        int brandId,
                        int forAgeId)
        {
            var querry = _context.Products
                .Sort(orderBy)
                .Search(search)
                .FilterCategory(categoryId, brandId)
                .FilterAge(forAgeId)
                .AsQueryable();

            var list = await querry.ToListAsync();
            var listImage = await _context.ImageProducts.ToListAsync();

            var Images = new List<ImageProductDTO>();
            foreach (var image in listImage)
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


        [HttpPost("Create")]
        public async Task<ActionResult<CreateProductDTO>> CreateProduct([FromForm] CreateProductDTO productDto)
        {
            try
            {
                var imageProducts = await toCreateImage(productDto.ImageProducts);

                var product = new Product
                {
                    ForAgeId = productDto.ForAgeId,
                    CategoryId = productDto.CategoryId,
                    BrandId = productDto.BrandId,
                    Name = productDto.Name,
                    Description = productDto.Description,
                    Price = productDto.Price,
                    Stock = productDto.Stock,
                    IsActive = true,
                    ImageProducts = imageProducts
                };

                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetProduct), new { id = product.ProductId }, product); ;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ProductDTO>> UpdateProduct(int id, [FromForm] ProductDTO productDto)
        {
            var product = await _context.Products.Include(p => p.ImageProducts).FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null)
            {
                return NotFound();
            }

            product.ForAgeId = productDto.ForAgeId;
            product.CategoryId = productDto.CategoryId;
            product.BrandId = productDto.BrandId;
            product.Name = productDto.Name;
            product.Description = productDto.Description;
            product.Price = productDto.Price;
            product.Stock = productDto.Stock;
            product.IsActive = productDto.IsActive;
            product.ImageProducts = await toImage(productDto.ImageProducts, product.ImageProducts);

            await _context.SaveChangesAsync();

            return productDto;
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.IsActive = false;
            var result = await _context.SaveChangesAsync() > 0;

            if (result) return Ok();

            return BadRequest(new ProblemDetails { Title = "Problem deleting product" });
        }





        public static ProductDTO toProductDTO(Product? product, List<ImageProductDTO> listImages)
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

        public static ImageProductDTO toImageDTO(ImageProduct? image)
        {
            ImageProductDTO imageProductDTO = new ImageProductDTO();
            imageProductDTO.ImageId = image.ImageId;
            imageProductDTO.ProductId = image.ProductId;
            imageProductDTO.ImageUrl = image.ImageUrl;
            return imageProductDTO;
        }

        private async Task<List<ImageProduct>> toImage(List<ImageProductDTO>? image, List<ImageProduct> currentImages)
        {
            //var list = new List<ImageProduct>();
            foreach (var imgInList in image)
            {
                var existingimage = currentImages.FirstOrDefault(img => img.ImageId == imgInList.ImageId);
                if (existingimage != null)
                {
                    if (imgInList.ImageFile != null)
                        existingimage.ImageUrl = await SaveImage(imgInList.ImageFile, existingimage.ProductId, existingimage.ImageId);
                }
            }
            return currentImages;
        }

        private async Task<List<ImageProduct>> toCreateImage(List<CreateImageProductDTO>? images)
        {
            var list = new List<ImageProduct>();
            int maxImg = _context.ImageProducts.Count();
            int count = 1;
            foreach (var imgInList in images)
            {
                ImageProduct imageProduct = new ImageProduct();
                imageProduct.ProductId = imgInList.ProductId;
                imageProduct.ImageUrl = await SaveImage(imgInList.ImageFile, imgInList.ProductId, maxImg + count);
                list.Add(imageProduct);
                count++;
            }
            return list;
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile, int ProductId, int ImageId)
        {
            string imageUrl = Path.GetFileNameWithoutExtension(imageFile.FileName).Replace(" ", "-");
            imageUrl = $"{ProductId}-{ImageId}-" + imageUrl + "-" + DateTime.Now.ToString("ssfff") + Path.GetExtension(imageFile.FileName);

            //thay đổi đường dẫn cho phù hợp
            var rootDirectory = _configuration["UploadSettings:UploadDirectory"];
            var targetDirectory = Path.Combine(rootDirectory, "products");

            // Đảm bảo thư mục tồn tại
            if (!Directory.Exists(targetDirectory))
            {
                throw new DirectoryNotFoundException($"The directory '{targetDirectory}' does not exist.");
            }

            // Tìm và xóa hình ảnh cũ
            var oldImagePattern = $"{ProductId}-{ImageId}-*";
            var oldImages = Directory.GetFiles(targetDirectory, oldImagePattern);
            foreach (var oldImage in oldImages)
            {
                System.IO.File.Delete(oldImage);
            }

            var imagePath = Path.Combine(targetDirectory, imageUrl);
            try
            {
                using (var fileStream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fileStream);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or log it as needed
                throw new Exception("An error occurred while saving the image.", ex);
            }
            return $"/images/products/" + imageUrl;
        }

    }
}
