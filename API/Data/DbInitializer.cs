using API.Entities;

namespace API.Data
{
    public static class DbInitializer
    {

        public static void Initialize(StoreContext context)
        {
            //insert category
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
                {
                    new Category
                    {
                        Name = "Sữa bột",
                        Description = "",
                        IsActive = true,
                    },
                    new Category
                    {
                        Name = "Sữa hạt",
                        Description = "",
                        IsActive = true,
                    },
                    new Category
                    {
                        Name = "Thức uống dinh dưỡng",
                        Description = "",
                        IsActive = true,
                    },
                    new Category
                    {
                        Name = "Sữa tươi, sữa chua",
                        Description = "",
                        IsActive = true,
                    }
                };
                foreach (var category in categories)
                {
                    context.Categories.Add(category);
                }
                context.SaveChanges();
            }

            //insert forAge
            if (!context.ForAges.Any())
            {
                var forAges = new List<ForAge>
                {
                    new ForAge
                    {
                        Name = "0 - 6 Tháng tuổi"
                    },
                    new ForAge
                    {
                        Name = "6 - 12 Tháng tuổi"
                    },
                    new ForAge
                    {
                        Name = "0 - 1 Tuổi"
                    },
                    new ForAge
                    {
                        Name = "1 - 2 Tuổi"
                    },
                    new ForAge
                    {
                        Name = "Từ 2 Tuổi trờ lên"
                    }
                };
                foreach (var forAge in forAges)
                {
                    context.ForAges.Add(forAge);
                }
                context.SaveChanges();
            }

            //insert brand
            if (!context.Brands.Any())
            {
                var brands = new List<Brand>
                {
                    new Brand
                    {
                        Name = "Meiji",
                        ImageBrandUrl = "/images/brands/meiji.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "TH True Milk",
                        ImageBrandUrl = "/images/brands/thtruemilk.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Fruto Nyanyan",
                        ImageBrandUrl = "/images/brands/FrutoNyanya.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Sahmyook",
                        ImageBrandUrl = "/images/brands/Sahmyook.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "137 Degrees",
                        ImageBrandUrl = "/images/brands/137degree.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Nestle",
                        ImageBrandUrl = "/images/brands/Nestle.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "HOFF",
                        ImageBrandUrl = "/images/brands/HOFF.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Yakult",
                        ImageBrandUrl = "/images/brands/Yakult.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Vinamilk",
                        ImageBrandUrl = "/images/brands/Vinamilk.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "PediaSure",
                        ImageBrandUrl = "/images/brands/PediaSure.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Similac",
                        ImageBrandUrl = "/images/brands/Similac.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "NAN Optipro",
                        ImageBrandUrl = "/images/brands/nan.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "a2 Milk",
                        ImageBrandUrl = "/images/brands/a2milk.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Famna",
                        ImageBrandUrl = "/images/brands/famna.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Aptamil Uc",
                        ImageBrandUrl = "/images/brands/aptamil.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Nuvi Grow",
                        ImageBrandUrl = "/images/brands/nuvigrow.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Kenko",
                        ImageBrandUrl = "/images/brands/kenko.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Provital",
                        ImageBrandUrl = "/images/brands/provital.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Wakodo",
                        ImageBrandUrl = "/images/brands/wakodo.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Hikid",
                        ImageBrandUrl = "/images/brands/hikid.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Varna",
                        ImageBrandUrl = "/images/brands/varna.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Purelac",
                        ImageBrandUrl = "/images/brands/purelac.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Colos Gain",
                        ImageBrandUrl = "/images/brands/colosgain.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Glucerna",
                        ImageBrandUrl = "/images/brands/glucerna.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "HiPP",
                        ImageBrandUrl = "/images/brands/hipp.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Humana",
                        ImageBrandUrl = "/images/brands/humana.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Bellamy's Organic",
                        ImageBrandUrl = "/images/brands/organic.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Friso Gold Pro",
                        ImageBrandUrl = "/images/brands/friso.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "Morinaga",
                        ImageBrandUrl = "/images/brands/morinaga.jpg",
                        IsActive = true,
                    },
                    new Brand
                    {
                        Name = "YOKOGOLD",
                        ImageBrandUrl = "/images/brands/yokogold.jpg",
                        IsActive = true,
                    },

                };
                foreach (var brand in brands)
                {
                    context.Brands.Add(brand);
                }
                context.SaveChanges();
            }

            //insert product
            if (!context.Products.Any())
            {
                var products = new List<Product>
            {
                new Product
                {
                    ForAgeId = 3,   // 0 - 1 tuoi
                    CategoryId = 1, //Sua bot
                    BrandId = 1,    // Meiji
                    Name = "Meiji Infant Formula",
                    Description = "Thực phẩm bổ sung sản phẩm dinh dưỡng công thức cho trẻ từ 0 đến 12 tháng tuổi: Meiji 0-1 years old Infant Formula",
                    Price = 529000,
                    Stock = 30,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 4,   // 1 - 3 tuoi
                    CategoryId = 1, //Sua bot
                    BrandId = 1,    // Meiji
                    Name = "Meiji Growing up Formula",
                    Description = "Thực phẩm bổ sung sản phẩm dinh dưỡng công thức cho trẻ từ 1 đến 3 tuổi: Meiji 1-3 years old Growing up Formula",
                    Price = 465000,
                    Stock = 20,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 3,   // 0 - 1 tuoi
                    CategoryId = 1, //Sua bot
                    BrandId = 1,    // Meiji
                    Name = "Meiji thanh Infant Formula Ezcube",
                    Description = "Sản phẩm dinh dưỡng công thức Meiji Infant Formula Ezcube 540g",
                    Price = 455000,
                    Stock = 35,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 3,   // 0 - 1 tuoi
                    CategoryId = 1, //Sua bot
                    BrandId = 1,    // Meiji
                    Name = "Meiji nội địa Hohoemi",
                    Description = "Sữa Meiji nội địa Hohoemi, 0 - 1 tuổi, 800G",
                    Price = 565000,
                    Stock = 50,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 4,   // 1 - 3 tuoi
                    CategoryId = 1, //Sua bot
                    BrandId = 1,    // Meiji
                    Name = "Meiji Growing up Formula Ezcube",
                    Description = "Sản phẩm dinh dưỡng công thức cho trẻ từ 1-3 tuổi: Meiji 1-3 years old Growing up Formula Ezcube 560g",
                    Price = 399000,
                    Stock = 10,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 5,   // 1+ tuoi
                    CategoryId = 2, //Sua hat
                    BrandId = 2,    // TH Milk
                    Name = "Sữa tươi tiệt trùng TH true Milk",
                    Description = "﻿Sữa tươi tiệt trùng TH true Milk ít đường với hương vị thơm ngon thanh mát, giữ trọn vẹn dinh dưỡng từ sữa tươi sạch cho cả nhà vui khỏe mỗi ngày",
                    Price = 25000,
                    Stock = 70,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 5,   // 1+ tuoi
                    CategoryId = 2, //Sua hat
                    BrandId = 3,    // Fruto Nyanya
                    Name = "Sữa đêm gạo vị mâm xôi Fruto Nyanya (lốc 3 hộp)",
                    Description = "Thực phẩm bổ sung sản phẩm dinh dưỡng công thức cho trẻ từ 1 đến 3 tuổi: Meiji 1-3 years old Growing up Formula",
                    Price = 108000,
                    Stock = 20,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 5,   // 1+ tuoi
                    CategoryId = 2, //Sua hat
                    BrandId = 4,    //Sahmyook
                    Name = "Sữa đậu đen, óc chó hạnh nhân (Lốc 3)",
                    Description = "Sản phẩm dinh dưỡng công thức Meiji Infant Formula Ezcube 540g",
                    Price = 99000,
                    Stock = 40,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 5,   // 1+ tuoi
                    CategoryId = 2, //Sua hat
                    BrandId = 5,    // 137 Degrees
                    Name = "Sữa hạt óc chó nguyên chất hiệu 137oC Degrees",
                    Description = "﻿﻿﻿Sữa hạt óc chó nguyên chất hiệu 137oC Degrees (137oC Degrees Walnut Milk Original)",
                    Price = 95000,
                    Stock = 50,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 5,   // 1+ tuoi
                    CategoryId = 2, //Sua hat
                    BrandId = 5,    // 137 Degrees
                    Name = "Sữa hạt hạnh nhân nguyên chất Almond Milk hiệu 137oC Degrees",
                    Description = "﻿﻿Sữa hạt hạnh nhân nguyên chất hiệu  137oC Degrees (137oC Degrees Almond Milk Original)",
                    Price = 85000,
                    Stock = 15,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 5,   // 3+ tuoi
                    CategoryId = 3, //Thuc uong dinh duong
                    BrandId = 6,    // Nestle Milo
                    Name = "Thức uống lúa mạch uống liền Nestle Milo 180ml - Lốc 4 hộp",
                    Description = "Thực phẩm bổ sung sữa lúa mạch Nestlé Milo bổ sung các vi dưỡng chất thiết yếu cho bé, giúp bé bền bỉ hơn.",
                    Price = 33000,
                    Stock = 100,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 5,   // 3+ tuoi
                    CategoryId = 3, //Thuc uong dinh duong
                    BrandId = 7,    // HOFF
                    Name = "Sữa chua có đường Hoff (Lốc 4 hủ)",
                    Description = "﻿﻿﻿﻿Sữa chua trẻ em vị nguyên bản Hoff là sản phẩm phù hợp với các bé từ 6 tháng tuổi. Sản phẩm giúp cải thiện hệ thống miễn dịch cho trẻ, giúp bé ăn ngon miệng & tăng cường hấp thu các chất dinh dưỡng",
                    Price = 49000,
                    Stock = 50,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 5,   // 3+ tuoi
                    CategoryId = 3, //Thuc uong dinh duong
                    BrandId = 8,    //Yakult
                    Name = "Sữa uống lên men Yakult (lốc 5 hộp 65ml)",
                    Description = "﻿﻿﻿Sữa chua uống lên men Yakult là sản phẩm phù hợp với bé trên 6 tháng tuổi và được sản xuất theo công nghệ Nhật Bản. Sản phẩm bổ sung lợi khuẩn đường ruột giúp hệ tiêu hóa của bé tốt hơn và tăng cường hệ miễn dịch hiệu quả.",
                    Price = 26000,
                    Stock = 55,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 5,   // 3+ tuoi
                    CategoryId = 4, //Sữa tươi, sữa chua
                    BrandId = 9,    // Vinamilk
                    Name = "Sữa chua Vinamilk có đường",
                    Description = "﻿﻿﻿﻿Sữa chua Vinamilk với hương vị thơm ngon thanh mát cung cấp nguồn dưỡng chất cho gia đình vui khỏe mỗi ngày.",
                    Price = 25000,
                    Stock = 70,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 5,   // 3+ tuoi
                    CategoryId = 4, //Sữa tươi, sữa chua
                    BrandId = 9,    // Vinamilk
                    Name = "Sữa chua Vinamilk Dâu",
                    Description = "﻿﻿Sữa hạt hạnh nhân nguyên chất hiệu  137oC Degrees (137oC Degrees Almond Milk Original)",
                    Price = 30000,
                    Stock = 55,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 5,   // 1-10 tuoi
                    CategoryId = 1, //Sua bot
                    BrandId = 10,    // PediaSure
                    Name = "Thực phẩm dinh dưỡng y học cho trẻ 1-10 tuổi: Pediasure vani",
                    Description = "Thực phẩm dinh dưỡng y học Abbott Pediasure hương Vani 1,6kg (1-10 tuổi)",
                    Price = 1085000,
                    Stock = 50,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 1,   // 0 - 6 Month
                    CategoryId = 1, //Sua bot
                    BrandId = 11,    //Similac 5G
                    Name = "Sữa Similac 5G số 1 400g (0-6 tháng)",
                    Description = "﻿﻿﻿﻿Sản phẩm dinh dưỡng công thức cho tẻ 0-6 tháng tuổi: Similac 1",
                    Price = 305000,
                    Stock = 40,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 2,   // 6 - 12 Month
                    CategoryId = 1, //Sua bot
                    BrandId = 11,    //Similac 5G
                    Name = "Sữa Similac 5G số 2 900g (6-12 tháng)",
                    Description = "﻿﻿﻿Sản phẩm dinh dưỡng công thức cho trẻ 6 - 12 tháng tuổi: Similac 2",
                    Price = 599000,
                    Stock = 55,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 4,   // 1 - 2 tuoi
                    CategoryId = 1, //Sua bot
                    BrandId = 11,    //Similac 5G
                    Name = "Sữa Similac 5G số 3 900g (1-2 tuổi)",
                    Description = "﻿﻿﻿﻿Sữa chua Vinamilk với hương vị thơm ngon thanh mát cung cấp nguồn dưỡng chất cho gia đình vui khỏe mỗi ngày.",
                    Price = 535000,
                    Stock = 60,
                    IsActive = true,
                },

                new Product
                {
                    ForAgeId = 5,   // 2-6 tuoi
                    CategoryId = 1, //Sua bot
                    BrandId = 11,    //Similac 5G
                    Name = "Sữa Similac 5G số 4 900g (2-6 tuổi)",
                    Description = "﻿﻿Thực phẩm bổ sung cho trẻ 2-6 tuổi: Similac 4",
                    Price = 519000,
                    Stock = 55,
                    IsActive = true,
                },

                new Product
                { //21
                    ForAgeId = 1,   
                    CategoryId = 1, 
                    BrandId = 12,    
                    Name = "Sữa Nan Optipro PLUS 1 800g",
                    Description = "Sữa Nan Optipro PLUS 1 800g, với 5HMO, sản xuất tại Thụy Sỹ (0-6 tháng)",
                    Price = 549000,
                    Stock = 50,
                    IsActive = true,
                },

                new Product
                { //22
                    ForAgeId = 2,
                    CategoryId = 1,
                    BrandId = 12,
                    Name = "Sữa Nan Optipro PLUS 2 800g",
                    Description = "Sữa Nan Optipro PLUS 2 800g, với 5HMO, sản xuất tại Thụy Sỹ (6-12 tháng)",
                    Price = 539000,
                    Stock = 50,
                    IsActive = true,
                },

                new Product
                { //23
                    ForAgeId = 4,
                    CategoryId = 1,
                    BrandId = 12,
                    Name = "Sữa Nan Optipro PLUS 3 1.5kg",
                    Description = "Sữa Nan Optipro PLUS 3 1.5kg, với 5HMO, công thức từ Thụy Sĩ (1-2 tuổi)",
                    Price = 805000,
                    Stock = 50,
                    IsActive = true,
                },

                new Product
                { //24
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 12,
                    Name = "Sữa Nan Optipro PLUS 4 850g",
                    Description = "Sữa Nan Optipro PLUS 4 850g, với 5HMO, công thức từ Thụy Sĩ (2-6 tuổi)",
                    Price = 469000,
                    Stock = 50,
                    IsActive = true,
                },

                new Product
                { //25
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 13,
                    Name = "Sữa A2 Full Cream Milk 1kg",
                    Description = "Sữa A2 Full Cream Milk 1kg",
                    Price = 365000,
                    Stock = 50,
                    IsActive = true,
                },

                new Product
                { //26
                    ForAgeId = 1,
                    CategoryId = 1,
                    BrandId = 14,
                    Name = "Sữa Famna Số 1 850g",
                    Description = "Sữa Famna Số 1 850g (0-6 tháng tuổi)",
                    Price = 469000,
                    Stock = 30,
                    IsActive = true,
                },

                new Product
                { //27
                    ForAgeId = 2,
                    CategoryId = 1,
                    BrandId = 14,
                    Name = "Sữa Famna Số 2 850g",
                    Description = "Sữa Famna Số 2 850g (6-12 tháng tuổi)",
                    Price = 469000,
                    Stock = 30,
                    IsActive = true,
                },

                new Product
                { //28
                    ForAgeId = 4,
                    CategoryId = 1,
                    BrandId = 14,
                    Name = "Sữa Famna Số 3 850g",
                    Description = "Sữa Famna Số 3 850g (1-2 tuổi)",
                    Price = 419000,
                    Stock = 30,
                    IsActive = true,
                },

                new Product
                { //29
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 14,
                    Name = "Sữa Famna Số 4 850g",
                    Description = "Sữa Famna Số 4 850g (từ 2 tuổi)",
                    Price = 379000,
                    Stock = 30,
                    IsActive = true,
                },

                new Product
                { //30
                    ForAgeId = 1,
                    CategoryId = 1,
                    BrandId = 15,
                    Name = "Sữa Aptamil Profutura Úc số 1 900g",
                    Description = "Sữa Aptamil Profutura Úc số 1 900g (0-6 tháng)",
                    Price = 925000,
                    Stock = 40,
                    IsActive = true,
                },

                new Product
                { //31
                    ForAgeId = 2,
                    CategoryId = 1,
                    BrandId = 15,
                    Name = "Sữa Aptamil Profutura Úc số 2 900g",
                    Description = "Sữa Aptamil Profutura Úc số 2 900g (6-12 tháng)",
                    Price = 925000,
                    Stock = 40,
                    IsActive = true,
                },

                new Product
                { //32
                    ForAgeId = 4,
                    CategoryId = 1,
                    BrandId = 15,
                    Name = "Sữa Aptamil Profutura Úc số 3 900g",
                    Description = "Sữa Aptamil Profutura Úc số 3 900g (từ 1 tuổi)",
                    Price = 850000,
                    Stock = 40,
                    IsActive = true,
                },

                new Product
                { //33
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 15,
                    Name = "Sữa Aptamil Profutura Úc số 4 900g",
                    Description = "Sữa Aptamil Profutura Úc số 4 900g (từ 2 tuổi)",
                    Price = 850000,
                    Stock = 40,
                    IsActive = true,
                },

                new Product
                { //34
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 16,
                    Name = "Sữa Nuvi Grow 2+ 900g",
                    Description = "Sữa Nuvi Grow 2+ 900g (Trên 2 tuổi)",
                    Price = 269000,
                    Stock = 20,
                    IsActive = true,
                },

                new Product
                { //35
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 17,
                    Name = "Sữa bột Vinamilk Kenko Haru hộp 350g",
                    Description = "Sữa bột Vinamilk Kenko Haru hộp 350g",
                    Price = 259000,
                    Stock = 15,
                    IsActive = true,
                },

                new Product
                { //36
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 17,
                    Name = "Sữa bột Vinamilk Kenko Haru hộp 850g",
                    Description = "Sữa bột Vinamilk Kenko Haru hộp 850g",
                    Price = 605000,
                    Stock = 15,
                    IsActive = true,
                },

                new Product
                { //37
                    ForAgeId = 1,
                    CategoryId = 1,
                    BrandId = 18,
                    Name = "Thực phẩm bổ sung Provital Immuna Plus",
                    Description = "Thực phẩm bổ sung Provital Immuna Plus 960G",
                    Price = 659000,
                    Stock = 17,
                    IsActive = true,
                },

                new Product
                { //38
                    ForAgeId = 1,
                    CategoryId = 1,
                    BrandId = 19,
                    Name = "Sữa Wakodo MOM",
                    Description = "Sữa Wakodo MOM 830g",
                    Price = 455000,
                    Stock = 20,
                    IsActive = true,
                },

                new Product
                { //39
                    ForAgeId = 3,
                    CategoryId = 3,
                    BrandId = 19,
                    Name = "Mì udon không muối Easy Manma Wakodo",
                    Description = "Mì udon không muối Easy Manma Wakodo cho bé",
                    Price = 85000,
                    Stock = 25,
                    IsActive = true,
                },

                new Product
                { //40
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 20,
                    Name = "Sữa Hikid vị Vani 600g",
                    Description = "Sữa Hikid vị Vani 600g (1-9 tuổi)",
                    Price = 533000,
                    Stock = 25,
                    IsActive = true,
                },

                new Product
                { //41
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 21,
                    Name = "Sữa Nutifood Varna Complete",
                    Description = "Sữa Nutifood Varna Complete 850g",
                    Price = 531000,
                    Stock = 25,
                    IsActive = true,
                },

                new Product
                { //42
                    ForAgeId = 5,
                    CategoryId = 4,
                    BrandId = 21,
                    Name = "Sữa Nutifood Varna Complete 237ml",
                    Description = "Sữa Nutifood Varna Complete 237ml (lốc 6 chai)",
                    Price = 195000,
                    Stock = 30,
                    IsActive = true,
                },

                new Product
                { //43
                    ForAgeId = 1,
                    CategoryId = 1,
                    BrandId = 22,
                    Name = "Sữa Purelac số 1 800g",
                    Description = "Sữa Purelac số 1 800g (0-6 tháng)",
                    Price = 849000,
                    Stock = 40,
                    IsActive = true,
                },

                new Product
                { //44
                    ForAgeId = 2,
                    CategoryId = 1,
                    BrandId = 22,
                    Name = "Sữa Purelac số 2 800g",
                    Description = "Sữa Purelac số 2 800g (6-12 tháng)",
                    Price = 849000,
                    Stock = 40,
                    IsActive = true,
                },

                new Product
                { //45
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 22,
                    Name = "Sữa Purelac số 3 800g",
                    Description = "Sữa Purelac số 3 800g (2 tuổi)",
                    Price = 849000,
                    Stock = 40,
                    IsActive = true,
                },

                new Product
                { //46
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 23,
                    Name = "Sữa Colos Gain 1+ 800g",
                    Description = "Sữa Colos Gain 1+ 800g",
                    Price = 379000,
                    Stock = 45,
                    IsActive = true,
                },

                new Product
                { //47
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 24,
                    Name = "Thực phẩm dinh dưỡng y học Glucerna hương vani",
                    Description = "Thực phẩm dinh dưỡng y học Glucerna hương vani 850g",
                    Price = 869000,
                    Stock = 55,
                    IsActive = true,
                },

                new Product
                { //48
                    ForAgeId = 1,
                    CategoryId = 1,
                    BrandId = 25,
                    Name = "Sữa HiPP Organic Combiotic số 1 800g",
                    Description = "Sữa HiPP Organic Combiotic số 1 800g (0-6 tháng tuổi)",
                    Price = 730000,
                    Stock = 25,
                    IsActive = true,
                },

                new Product
                { //49
                    ForAgeId = 2,
                    CategoryId = 1,
                    BrandId = 25,
                    Name = "Sữa HiPP Organic Combiotic số 2 800g",
                    Description = "Sữa HiPP Organic Combiotic số 2 800g (6-12 tháng tuổi)",
                    Price = 720000,
                    Stock = 25,
                    IsActive = true,
                },

                new Product
                { //50
                    ForAgeId = 4,
                    CategoryId = 1,
                    BrandId = 25,
                    Name = "Sữa HiPP Organic Combiotic số 3",
                    Description = "Sữa HiPP Organic Combiotic số 3 800g",
                    Price = 720000,
                    Stock = 25,
                    IsActive = true,
                },

                new Product
                { //51
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 25,
                    Name = "Sữa HiPP Organic Combiotic số 4",
                    Description = "Sữa HiPP Organic Combiotic số 4 800g (từ 2 tuổi)",
                    Price = 635000,
                    Stock = 25,
                    IsActive = true,
                },

                new Product
                { //52
                    ForAgeId = 1,
                    CategoryId = 1,
                    BrandId = 26,
                    Name = "Sữa Humana Gold Plus 1",
                    Description = "Sữa Humana Gold Plus 1 800g (0-6 tháng)",
                    Price = 665000,
                    Stock = 35,
                    IsActive = true,
                },

                new Product
                { //53
                    ForAgeId = 1,
                    CategoryId = 1,
                    BrandId = 27,
                    Name = "Sữa Bellamy's Organic Infant Formula số 1 900g",
                    Description = "Sữa Bellamy's Organic Infant Formula số 1 900g (0-6 tháng)",
                    Price = 1069000,
                    Stock = 25,
                    IsActive = true,
                },

                new Product
                { //54
                    ForAgeId = 2,
                    CategoryId = 1,
                    BrandId = 27,
                    Name = "Sữa Bellamy's Organic Follow-on Formula số 2 900g",
                    Description = "Sữa Bellamy's Organic Follow-on Formula số 2 900g (6-12 tháng)",
                    Price = 1069000,
                    Stock = 25,
                    IsActive = true,
                },

                new Product
                { //55
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 27,
                    Name = "Sữa Bellamy's Organic Junior Milk Drink số 4",
                    Description = "Sữa Bellamy's Organic Junior Milk Drink số 4 900g (trên 2 tuổi)",
                    Price = 1069000,
                    Stock = 25,
                    IsActive = true,
                },

                new Product
                { //56
                    ForAgeId = 1,
                    CategoryId = 1,
                    BrandId = 28,
                    Name = "Sữa Frisolac Gold Pro số 1 800g",
                    Description = "Sữa Frisolac Gold Pro số 1 800g (0-6 tháng)",
                    Price = 645000,
                    Stock = 55,
                    IsActive = true,
                },

                new Product
                { //57
                    ForAgeId = 2,
                    CategoryId = 1,
                    BrandId = 28,
                    Name = "Sữa Frisolac Gold Pro số 2 800g",
                    Description = "Sữa Frisolac Gold Pro số 2, 800g (6-12 tháng)",
                    Price = 639000,
                    Stock = 55,
                    IsActive = true,
                },

                new Product
                { //58
                    ForAgeId = 4,
                    CategoryId = 1,
                    BrandId = 28,
                    Name = "Sữa Frisolac Gold Pro số 3 800g",
                    Description = "Sữa Frisolac Gold Pro số 3 800g (1-2 tuổi)",
                    Price = 615000,
                    Stock = 55,
                    IsActive = true,
                },

                new Product
                { //59
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 28,
                    Name = "Sữa Friso Gold Pro số 4 800g",
                    Description = "Sữa Friso Gold Pro số 4 800g ( 2 tuổi)",
                    Price = 595000,
                    Stock = 55,
                    IsActive = true,
                },

                new Product
                { //60
                    ForAgeId = 1,
                    CategoryId = 1,
                    BrandId = 29,
                    Name = "Sữa Morinaga số 1 850g",
                    Description = "Sữa Morinaga số 1 850g (Hagukumi, 0-6 tháng)",
                    Price = 579000,
                    Stock = 40,
                    IsActive = true,
                },

                new Product
                { //61
                    ForAgeId = 2,
                    CategoryId = 1,
                    BrandId = 29,
                    Name = "Sữa Morinaga số 2 850g",
                    Description = "Sữa Morinaga số 2 850g (Chilmil, 6-12 tháng)",
                    Price = 515000,
                    Stock = 40,
                    IsActive = true,
                },

                new Product
                { //62
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 29,
                    Name = "Sữa Morinaga số 3 850g hương vani",
                    Description = "Sữa Morinaga số 3 850g hương vani (Kodomil, trên 2 tuổi)",
                    Price = 499000,
                    Stock = 40,
                    IsActive = true,
                },

                new Product
                { //63
                    ForAgeId = 1,
                    CategoryId = 1,
                    BrandId = 30,
                    Name = "Sữa Vinamilk Yoko Gold 1 850g",
                    Description = "Sữa Vinamilk Yoko Gold 1 850g (0-1 tuổi)",
                    Price = 449000,
                    Stock = 10,
                    IsActive = true,
                },

                new Product
                { //64
                    ForAgeId = 4,
                    CategoryId = 1,
                    BrandId = 30,
                    Name = "Sữa Vinamilk Yoko Gold 2 850g",
                    Description = "Sữa Vinamilk Yoko Gold 2 850g (1-2 tuổi)",
                    Price = 435000,
                    Stock = 10,
                    IsActive = true,
                },

                new Product
                { //65
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 30,
                    Name = "Sữa Vinamilk Yoko Gold 3 850g",
                    Description = "Sữa Vinamilk Yoko Gold 3 850g (2-6 tuổi)",
                    Price = 419000,
                    Stock = 10,
                    IsActive = true,
                },

            };

                foreach (var product in products)
                {
                    context.Products.Add(product);
                }
                //context.Products.AddRange(products);

                context.SaveChanges();
            }

            //insert ImageProduct
            if (!context.ImageProducts.Any())
            {
                var imageProdcucts = new List<ImageProduct>
                {
                    new ImageProduct
                    {
                        ProductId = 1,
                        ImageUrl = "/images/products/sua1.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 2,
                        ImageUrl = "/images/products/sua2.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 3,
                        ImageUrl = "/images/products/sua3.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 4,
                        ImageUrl = "/images/products/sua4.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 5,
                        ImageUrl = "/images/products/sua5.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 6,
                        ImageUrl = "/images/products/sua6.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 7,
                        ImageUrl = "/images/products/sua7.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 8,
                        ImageUrl = "/images/products/sua8.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 9,
                        ImageUrl = "/images/products/sua9.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 10,
                        ImageUrl = "/images/products/sua10.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 11,
                        ImageUrl = "/images/products/sua11.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 12,
                        ImageUrl = "/images/products/sua12.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 13,
                        ImageUrl = "/images/products/sua13.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 14,
                        ImageUrl = "/images/products/sua14.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 15,
                        ImageUrl = "/images/products/sua15.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 16,
                        ImageUrl = "/images/products/sua16.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 17,
                        ImageUrl = "/images/products/sua17.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 18,
                        ImageUrl = "/images/products/sua18.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 19,
                        ImageUrl = "/images/products/sua19.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 20,
                        ImageUrl = "/images/products/sua20.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 21,
                        ImageUrl = "/images/products/sua21.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 22,
                        ImageUrl = "/images/products/sua22.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 23,
                        ImageUrl = "/images/products/sua23.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 24,
                        ImageUrl = "/images/products/sua24.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 25,
                        ImageUrl = "/images/products/sua25.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 26,
                        ImageUrl = "/images/products/sua26.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 27,
                        ImageUrl = "/images/products/sua27.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 28,
                        ImageUrl = "/images/products/sua28.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 29,
                        ImageUrl = "/images/products/sua29.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 30,
                        ImageUrl = "/images/products/sua30.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 31,
                        ImageUrl = "/images/products/sua31.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 32,
                        ImageUrl = "/images/products/sua32.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 33,
                        ImageUrl = "/images/products/sua33.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 34,
                        ImageUrl = "/images/products/sua34.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 35,
                        ImageUrl = "/images/products/sua35.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 36,
                        ImageUrl = "/images/products/sua36.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 37,
                        ImageUrl = "/images/products/sua37.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 38,
                        ImageUrl = "/images/products/sua38.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 39,
                        ImageUrl = "/images/products/sua39.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 40,
                        ImageUrl = "/images/products/sua40.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 41,
                        ImageUrl = "/images/products/sua41.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 42,
                        ImageUrl = "/images/products/sua42.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 43,
                        ImageUrl = "/images/products/sua43.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 44,
                        ImageUrl = "/images/products/sua44.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 45,
                        ImageUrl = "/images/products/sua45.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 46,
                        ImageUrl = "/images/products/sua46.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 47,
                        ImageUrl = "/images/products/sua47.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 48,
                        ImageUrl = "/images/products/sua48.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 49,
                        ImageUrl = "/images/products/sua49.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 50,
                        ImageUrl = "/images/products/sua50.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 51,
                        ImageUrl = "/images/products/sua51.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 52,
                        ImageUrl = "/images/products/sua52.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 53,
                        ImageUrl = "/images/products/sua53.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 54,
                        ImageUrl = "/images/products/sua54.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 55,
                        ImageUrl = "/images/products/sua55.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 56,
                        ImageUrl = "/images/products/sua56.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 57,
                        ImageUrl = "/images/products/sua57.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 58,
                        ImageUrl = "/images/products/sua58.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 59,
                        ImageUrl = "/images/products/sua59.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 60,
                        ImageUrl = "/images/products/sua60.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 61,
                        ImageUrl = "/images/products/sua61.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 62,
                        ImageUrl = "/images/products/sua62.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 63,
                        ImageUrl = "/images/products/sua63.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 64,
                        ImageUrl = "/images/products/sua64.jpg"
                    },
                    new ImageProduct
                    {
                        ProductId = 65,
                        ImageUrl = "/images/products/sua65.jpg"
                    },

                };

                foreach (var image in imageProdcucts)
                {
                    context.ImageProducts.Add(image);
                }

                context.SaveChanges();

            }

            //insert Method
            if(!context.shippingMethods.Any())
            {
                var shippingMethods = new List<ShippingMethod>
                {
                    new ShippingMethod
                    {
                        Name = "Giao hàng tiết kiệm",
                        ShippingPrice = 30000,
                    },
                    new ShippingMethod
                    {
                        Name = "Giao hàng thường",
                        ShippingPrice = 50000,
                    },
                    new ShippingMethod
                    {
                        Name = "Giao hỏa tốc",
                        ShippingPrice = 120000,
                    },
                };
                foreach(var shippingMethod in shippingMethods)
                {
                    context.shippingMethods.Add(shippingMethod);
                }
                context.SaveChanges();
            }

            //insert role
            if (!context.Roles.Any())
            {
                var roles = new List<Role>
                {
                    new Role
                    {
                        RoleName = "User"
                    },
                    new Role
                    {
                        RoleName = "Staff"
                    },
                    new Role
                    {
                        RoleName = "Admin"
                    },
                };
                foreach (var role in roles)
                {
                    context.Roles.Add(role);
                }
                context.SaveChanges();
            }

            //insert user
            if (!context.Users.Any())
            {
                var users = new List<User>
                {
                    new User
                    {
                        RoleId = 1,
                        Email = "vu@vu.com",
                        Name = "Thanh Vũ",
                        Password = "123",
                        PhoneNumber = "1234567890",
                        Address = "Viet Nam",
                        IsActive = true,
                    },
                    new User
                    {
                        RoleId = 2,
                        Email = "tin@tin.com",
                        Name = "Quốc Tín",
                        Password = "123",
                        PhoneNumber = "0909090909",
                        Address = "Hà Nội",
                        IsActive = true,
                    },
                    new User
                    {
                        RoleId = 3,
                        Email = "admin@admin.com",
                        Name = "admin",
                        Password = "123",
                        PhoneNumber = "0808080808",
                        Address = "Viet Nam",
                        IsActive = true,
                    },
                };
                foreach (var user in users)
                {
                    context.Users.Add(user);
                }
                context.SaveChanges();
            }
        }
    }
}

