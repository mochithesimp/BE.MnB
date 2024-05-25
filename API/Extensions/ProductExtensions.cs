using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Extensions
{

    public static class ProductExtensions
    {


        public static IQueryable<Product> Sort(this IQueryable<Product> querry, string? orderBy)
        {
            if(string.IsNullOrEmpty(orderBy)) return querry;

            querry = orderBy switch
            {
                "price" => querry.OrderBy(p => p.Price),
                "priceDesc" => querry.OrderByDescending(p => p.Price),
                _ => querry.OrderBy(p => p.Name),
            };

            return querry;
        }

        public static IQueryable<Product> Search(this IQueryable<Product> querry, string? search)
        {
            if (string.IsNullOrEmpty(search)) return querry;

            var lowerCaseSearchTerm = search.Trim().ToLower();

            return querry.Where(p => p.Name.ToLower().Contains(lowerCaseSearchTerm));  
        }

        public static IQueryable<Product> FilterCategory(this IQueryable<Product> querry, int categoryId, int brandId)
        {

            querry =  querry.Where(p => categoryId == 0 || p.CategoryId == categoryId);
            querry = querry.Where(p => brandId == 0 || p.BrandId == brandId);

            return querry;
        }

        public static IQueryable<Product> FilterAge(this IQueryable<Product> querry, int forAgeId)
        {

            querry = querry.Where(p => forAgeId == 0 || p.ForAgeId == forAgeId);
            return querry;
        }

    }
}
