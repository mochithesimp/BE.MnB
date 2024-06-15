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

                    new Brand
                    {
                        Name = "ColosBaby",
                        ImageBrandUrl = "/images/brands/colosbaby.jpg",
                        IsActive = true,
                    },

                    new Brand
                    {
                        Name = "Bubs",
                        ImageBrandUrl = "/images/brands/bubs.jpg",
                        IsActive = true,
                    }

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

                new Product
                { //66
                    ForAgeId = 5,
                    CategoryId = 1,
                    BrandId = 31,
                    Name = "Sữa ColosBaby Gold Pedia 800g",
                    Description = "Sữa ColosBaby Gold Pedia 800g (1 - 10 tuổi)",
                    Price = 615000,
                    Stock = 40,
                    IsActive = true,
                },

                new Product
                { //67
                    ForAgeId = 1,
                    CategoryId = 1,
                    BrandId = 32,
                    Name = "Sữa Bubs Organic Bovine số 1 800g",
                    Description = "Sữa Bubs Organic Bovine số 1 800g (0-6 tháng)",
                    Price = 695000,
                    Stock = 30,
                    IsActive = true,
                },

                new Product
                { //68
                    ForAgeId = 2,
                    CategoryId = 1,
                    BrandId = 32,
                    Name = "Sữa Bubs Organic Bovine số 2 800g",
                    Description = "Sữa Bubs Organic Bovine số 2 800g (6-12 tháng)",
                    Price = 695000,
                    Stock = 50,
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
                        ProductId = 1,
                        ImageUrl = "/images/products/sua1-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 1,
                        ImageUrl = "/images/products/sua1-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 1,
                        ImageUrl = "/images/products/sua1-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 2,
                        ImageUrl = "/images/products/sua2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 2,
                        ImageUrl = "/images/products/sua2-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 2,
                        ImageUrl = "/images/products/sua2-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 2,
                        ImageUrl = "/images/products/sua2-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 3,
                        ImageUrl = "/images/products/sua3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 3,
                        ImageUrl = "/images/products/sua3-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 3,
                        ImageUrl = "/images/products/sua3-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 3,
                        ImageUrl = "/images/products/sua3-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 4,
                        ImageUrl = "/images/products/sua4.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 4,
                        ImageUrl = "/images/products/sua4-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 4,
                        ImageUrl = "/images/products/sua4-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 4,
                        ImageUrl = "/images/products/sua4-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 5,
                        ImageUrl = "/images/products/sua5.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 5,
                        ImageUrl = "/images/products/sua5-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 5,
                        ImageUrl = "/images/products/sua5-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 5,
                        ImageUrl = "/images/products/sua5-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 6,
                        ImageUrl = "/images/products/sua6.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 6,
                        ImageUrl = "/images/products/sua6-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 6,
                        ImageUrl = "/images/products/sua6-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 6,
                        ImageUrl = "/images/products/sua6-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 7,
                        ImageUrl = "/images/products/sua7.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 7,
                        ImageUrl = "/images/products/sua7-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 7,
                        ImageUrl = "/images/products/sua7-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 7,
                        ImageUrl = "/images/products/sua7-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 8,
                        ImageUrl = "/images/products/sua8.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 8,
                        ImageUrl = "/images/products/sua8-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 8,
                        ImageUrl = "/images/products/sua8-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 8,
                        ImageUrl = "/images/products/sua8-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 9,
                        ImageUrl = "/images/products/sua9.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 9,
                        ImageUrl = "/images/products/sua9-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 9,
                        ImageUrl = "/images/products/sua9-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 9,
                        ImageUrl = "/images/products/sua9-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 10,
                        ImageUrl = "/images/products/sua10.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 10,
                        ImageUrl = "/images/products/sua10-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 10,
                        ImageUrl = "/images/products/sua10-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 10,
                        ImageUrl = "/images/products/sua10-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 11,
                        ImageUrl = "/images/products/sua11.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 11,
                        ImageUrl = "/images/products/sua11-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 11,
                        ImageUrl = "/images/products/sua11-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 11,
                        ImageUrl = "/images/products/sua11-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 12,
                        ImageUrl = "/images/products/sua12.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 12,
                        ImageUrl = "/images/products/sua12-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 12,
                        ImageUrl = "/images/products/sua12-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 12,
                        ImageUrl = "/images/products/sua12-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 13,
                        ImageUrl = "/images/products/sua13.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 13,
                        ImageUrl = "/images/products/sua13-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 13,
                        ImageUrl = "/images/products/sua13-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 13,
                        ImageUrl = "/images/products/sua13-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 14,
                        ImageUrl = "/images/products/sua14.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 14,
                        ImageUrl = "/images/products/sua14-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 14,
                        ImageUrl = "/images/products/sua14-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 14,
                        ImageUrl = "/images/products/sua14-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 15,
                        ImageUrl = "/images/products/sua15.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 15,
                        ImageUrl = "/images/products/sua15-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 15,
                        ImageUrl = "/images/products/sua15-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 15,
                        ImageUrl = "/images/products/sua15-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 16,
                        ImageUrl = "/images/products/sua16.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 16,
                        ImageUrl = "/images/products/sua16-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 16,
                        ImageUrl = "/images/products/sua16-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 16,
                        ImageUrl = "/images/products/sua16-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 17,
                        ImageUrl = "/images/products/sua17.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 17,
                        ImageUrl = "/images/products/sua17-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 17,
                        ImageUrl = "/images/products/sua17-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 17,
                        ImageUrl = "/images/products/sua17-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 18,
                        ImageUrl = "/images/products/sua18.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 18,
                        ImageUrl = "/images/products/sua18-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 18,
                        ImageUrl = "/images/products/sua18-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 18,
                        ImageUrl = "/images/products/sua18-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 19,
                        ImageUrl = "/images/products/sua19.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 19,
                        ImageUrl = "/images/products/sua19-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 19,
                        ImageUrl = "/images/products/sua19-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 19,
                        ImageUrl = "/images/products/sua19-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 20,
                        ImageUrl = "/images/products/sua20.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 20,
                        ImageUrl = "/images/products/sua20-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 20,
                        ImageUrl = "/images/products/sua20-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 20,
                        ImageUrl = "/images/products/sua20-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 21,
                        ImageUrl = "/images/products/sua21.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 21,
                        ImageUrl = "/images/products/sua21-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 21,
                        ImageUrl = "/images/products/sua21-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 21,
                        ImageUrl = "/images/products/sua21-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 22,
                        ImageUrl = "/images/products/sua22.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 22,
                        ImageUrl = "/images/products/sua22-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 22,
                        ImageUrl = "/images/products/sua22-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 22,
                        ImageUrl = "/images/products/sua22-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 23,
                        ImageUrl = "/images/products/sua23.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 23,
                        ImageUrl = "/images/products/sua23-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 23,
                        ImageUrl = "/images/products/sua23-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 23,
                        ImageUrl = "/images/products/sua23-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 24,
                        ImageUrl = "/images/products/sua24.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 24,
                        ImageUrl = "/images/products/sua24-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 24,
                        ImageUrl = "/images/products/sua24-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 24,
                        ImageUrl = "/images/products/sua24-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 25,
                        ImageUrl = "/images/products/sua25.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 25,
                        ImageUrl = "/images/products/sua25-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 25,
                        ImageUrl = "/images/products/sua25-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 25,
                        ImageUrl = "/images/products/sua25-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 26,
                        ImageUrl = "/images/products/sua26.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 26,
                        ImageUrl = "/images/products/sua26-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 26,
                        ImageUrl = "/images/products/sua26-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 26,
                        ImageUrl = "/images/products/sua26-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 27,
                        ImageUrl = "/images/products/sua27.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 27,
                        ImageUrl = "/images/products/sua27-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 27,
                        ImageUrl = "/images/products/sua27-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 27,
                        ImageUrl = "/images/products/sua27-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 28,
                        ImageUrl = "/images/products/sua28.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 28,
                        ImageUrl = "/images/products/sua28-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 28,
                        ImageUrl = "/images/products/sua28-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 28,
                        ImageUrl = "/images/products/sua28-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 29,
                        ImageUrl = "/images/products/sua29.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 29,
                        ImageUrl = "/images/products/sua29-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 29,
                        ImageUrl = "/images/products/sua29-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 29,
                        ImageUrl = "/images/products/sua29-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 30,
                        ImageUrl = "/images/products/sua30.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 30,
                        ImageUrl = "/images/products/sua30-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 30,
                        ImageUrl = "/images/products/sua30-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 30,
                        ImageUrl = "/images/products/sua30-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 31,
                        ImageUrl = "/images/products/sua31.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 31,
                        ImageUrl = "/images/products/sua31-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 31,
                        ImageUrl = "/images/products/sua31-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 31,
                        ImageUrl = "/images/products/sua31-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 32,
                        ImageUrl = "/images/products/sua32.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 32,
                        ImageUrl = "/images/products/sua32-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 32,
                        ImageUrl = "/images/products/sua32-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 32,
                        ImageUrl = "/images/products/sua32-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 33,
                        ImageUrl = "/images/products/sua33.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 33,
                        ImageUrl = "/images/products/sua33-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 33,
                        ImageUrl = "/images/products/sua33-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 33,
                        ImageUrl = "/images/products/sua33-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 34,
                        ImageUrl = "/images/products/sua34.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 34,
                        ImageUrl = "/images/products/sua34-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 34,
                        ImageUrl = "/images/products/sua34-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 34,
                        ImageUrl = "/images/products/sua34-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 35,
                        ImageUrl = "/images/products/sua35.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 35,
                        ImageUrl = "/images/products/sua35-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 35,
                        ImageUrl = "/images/products/sua35-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 35,
                        ImageUrl = "/images/products/sua35-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 36,
                        ImageUrl = "/images/products/sua36.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 36,
                        ImageUrl = "/images/products/sua36-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 36,
                        ImageUrl = "/images/products/sua36-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 36,
                        ImageUrl = "/images/products/sua36-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 37,
                        ImageUrl = "/images/products/sua37.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 37,
                        ImageUrl = "/images/products/sua37-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 37,
                        ImageUrl = "/images/products/sua37-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 37,
                        ImageUrl = "/images/products/sua37-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 38,
                        ImageUrl = "/images/products/sua38.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 38,
                        ImageUrl = "/images/products/sua38-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 38,
                        ImageUrl = "/images/products/sua38-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 38,
                        ImageUrl = "/images/products/sua38-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 39,
                        ImageUrl = "/images/products/sua39.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 39,
                        ImageUrl = "/images/products/sua39-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 39,
                        ImageUrl = "/images/products/sua39-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 39,
                        ImageUrl = "/images/products/sua39-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 40,
                        ImageUrl = "/images/products/sua40.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 40,
                        ImageUrl = "/images/products/sua40-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 40,
                        ImageUrl = "/images/products/sua40-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 40,
                        ImageUrl = "/images/products/sua40-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 41,
                        ImageUrl = "/images/products/sua41.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 41,
                        ImageUrl = "/images/products/sua41-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 41,
                        ImageUrl = "/images/products/sua41-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 41,
                        ImageUrl = "/images/products/sua41-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 42,
                        ImageUrl = "/images/products/sua42.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 42,
                        ImageUrl = "/images/products/sua42-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 42,
                        ImageUrl = "/images/products/sua42-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 42,
                        ImageUrl = "/images/products/sua42-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 43,
                        ImageUrl = "/images/products/sua43.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 43,
                        ImageUrl = "/images/products/sua43-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 43,
                        ImageUrl = "/images/products/sua43-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 43,
                        ImageUrl = "/images/products/sua43-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 44,
                        ImageUrl = "/images/products/sua44.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 44,
                        ImageUrl = "/images/products/sua44-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 44,
                        ImageUrl = "/images/products/sua44-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 44,
                        ImageUrl = "/images/products/sua44-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 45,
                        ImageUrl = "/images/products/sua45.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 45,
                        ImageUrl = "/images/products/sua45-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 45,
                        ImageUrl = "/images/products/sua45-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 45,
                        ImageUrl = "/images/products/sua45-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 46,
                        ImageUrl = "/images/products/sua46.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 46,
                        ImageUrl = "/images/products/sua46-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 46,
                        ImageUrl = "/images/products/sua46-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 46,
                        ImageUrl = "/images/products/sua46-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 47,
                        ImageUrl = "/images/products/sua47.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 47,
                        ImageUrl = "/images/products/sua47-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 47,
                        ImageUrl = "/images/products/sua47-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 47,
                        ImageUrl = "/images/products/sua47-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 48,
                        ImageUrl = "/images/products/sua48.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 48,
                        ImageUrl = "/images/products/sua48-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 48,
                        ImageUrl = "/images/products/sua48-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 48,
                        ImageUrl = "/images/products/sua48-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 49,
                        ImageUrl = "/images/products/sua49.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 49,
                        ImageUrl = "/images/products/sua49-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 49,
                        ImageUrl = "/images/products/sua49-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 49,
                        ImageUrl = "/images/products/sua49-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 50,
                        ImageUrl = "/images/products/sua50.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 50,
                        ImageUrl = "/images/products/sua50-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 50,
                        ImageUrl = "/images/products/sua50-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 50,
                        ImageUrl = "/images/products/sua50-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 51,
                        ImageUrl = "/images/products/sua51.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 51,
                        ImageUrl = "/images/products/sua51-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 51,
                        ImageUrl = "/images/products/sua51-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 51,
                        ImageUrl = "/images/products/sua51-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 52,
                        ImageUrl = "/images/products/sua52.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 52,
                        ImageUrl = "/images/products/sua52-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 52,
                        ImageUrl = "/images/products/sua52-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 52,
                        ImageUrl = "/images/products/sua52-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 53,
                        ImageUrl = "/images/products/sua53.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 53,
                        ImageUrl = "/images/products/sua53-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 53,
                        ImageUrl = "/images/products/sua53-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 53,
                        ImageUrl = "/images/products/sua53-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 54,
                        ImageUrl = "/images/products/sua54.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 54,
                        ImageUrl = "/images/products/sua54-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 54,
                        ImageUrl = "/images/products/sua54-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 54,
                        ImageUrl = "/images/products/sua54-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 55,
                        ImageUrl = "/images/products/sua55.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 55,
                        ImageUrl = "/images/products/sua55-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 55,
                        ImageUrl = "/images/products/sua55-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 55,
                        ImageUrl = "/images/products/sua55-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 56,
                        ImageUrl = "/images/products/sua56.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 56,
                        ImageUrl = "/images/products/sua56-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 56,
                        ImageUrl = "/images/products/sua56-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 56,
                        ImageUrl = "/images/products/sua56-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 57,
                        ImageUrl = "/images/products/sua57.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 57,
                        ImageUrl = "/images/products/sua57-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 57,
                        ImageUrl = "/images/products/sua57-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 57,
                        ImageUrl = "/images/products/sua57-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 58,
                        ImageUrl = "/images/products/sua58.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 58,
                        ImageUrl = "/images/products/sua58-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 58,
                        ImageUrl = "/images/products/sua58-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 58,
                        ImageUrl = "/images/products/sua58-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 59,
                        ImageUrl = "/images/products/sua59.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 59,
                        ImageUrl = "/images/products/sua59-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 59,
                        ImageUrl = "/images/products/sua59-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 59,
                        ImageUrl = "/images/products/sua59-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 60,
                        ImageUrl = "/images/products/sua60.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 60,
                        ImageUrl = "/images/products/sua60-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 60,
                        ImageUrl = "/images/products/sua60-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 60,
                        ImageUrl = "/images/products/sua60-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 61,
                        ImageUrl = "/images/products/sua61.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 61,
                        ImageUrl = "/images/products/sua61-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 61,
                        ImageUrl = "/images/products/sua61-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 61,
                        ImageUrl = "/images/products/sua61-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 62,
                        ImageUrl = "/images/products/sua62.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 62,
                        ImageUrl = "/images/products/sua62-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 62,
                        ImageUrl = "/images/products/sua62-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 62,
                        ImageUrl = "/images/products/sua62-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 63,
                        ImageUrl = "/images/products/sua63.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 63,
                        ImageUrl = "/images/products/sua63-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 63,
                        ImageUrl = "/images/products/sua63-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 63,
                        ImageUrl = "/images/products/sua63-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 64,
                        ImageUrl = "/images/products/sua64.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 64,
                        ImageUrl = "/images/products/sua64-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 64,
                        ImageUrl = "/images/products/sua64-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 64,
                        ImageUrl = "/images/products/sua64-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 65,
                        ImageUrl = "/images/products/sua65.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 65,
                        ImageUrl = "/images/products/sua65-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 65,
                        ImageUrl = "/images/products/sua65-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 65,
                        ImageUrl = "/images/products/sua65-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 66,
                        ImageUrl = "/images/products/sua66.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 66,
                        ImageUrl = "/images/products/sua66-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 66,
                        ImageUrl = "/images/products/sua66-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 66,
                        ImageUrl = "/images/products/sua66-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 67,
                        ImageUrl = "/images/products/sua67.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 67,
                        ImageUrl = "/images/products/sua67-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 67,
                        ImageUrl = "/images/products/sua67-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 67,
                        ImageUrl = "/images/products/sua67-3.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 68,
                        ImageUrl = "/images/products/sua68.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 68,
                        ImageUrl = "/images/products/sua68-1.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 68,
                        ImageUrl = "/images/products/sua68-2.jpg"
                    },

                    new ImageProduct
                    {
                        ProductId = 68,
                        ImageUrl = "/images/products/sua68-3.jpg"
                    },

                };

                foreach (var image in imageProdcucts)
                {
                    context.ImageProducts.Add(image);
                }

                context.SaveChanges();

            }

            //insert Method
            if (!context.shippingMethods.Any())
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
                foreach (var shippingMethod in shippingMethods)
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

            //insert voucher
            if (!context.Vouchers.Any())
            {
                var vouchers = new List<Voucher>
                {
                    new Voucher
                    {
                        Name = "Giảm giá đơn 500K",
                        Code = "500K",
                        DiscountType = "%",
                        DiscountValue = 30,
                        MinimumTotal = 300000,
                        CreatedDate = DateTime.Now,
                        ExpDate = DateTime.Now.AddDays(7),
                        IsActive = true,
                        ProductId = null,
                    },

                    new Voucher
                    {
                        Name = "Giảm giá đơn 1M",
                        Code = "1M",
                        DiscountType = "K",
                        DiscountValue = 250,
                        MinimumTotal = 1000000,
                        CreatedDate = DateTime.Now,
                        ExpDate = DateTime.Now.AddDays(7),
                        IsActive = true,
                        ProductId = null,
                    },

                    new Voucher
                    {
                        Name = "Giảm giá sữa 1",
                        Code = "MeijiS1",
                        DiscountType = "%",
                        DiscountValue = 40,
                        MinimumTotal = 300000,
                        CreatedDate = DateTime.Now,
                        ExpDate = DateTime.Now.AddDays(7),
                        IsActive = true,
                        ProductId = 1,
                    },

                    new Voucher
                    {
                        Name = "Giảm giá sữa 2",
                        Code = "MeijiS2",
                        DiscountType = "%",
                        DiscountValue = 45,
                        MinimumTotal = 300000,
                        CreatedDate = DateTime.Now,
                        ExpDate = DateTime.Now.AddDays(7),
                        IsActive = true,
                        ProductId = 2,
                    },

                };
                foreach (var voucher in vouchers)
                {
                    context.Vouchers.Add(voucher);
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

            if (!context.Blogs.Any())
            {
                var blogs = new List<Blog>
                {
                    new Blog
                    {
                        Title = "Review dòng sữa cao cấp Meiji: Chất lượng có xứng với giá tiền?",
                        Content = "Thị trường phân phối sữa bột cho bé hiện đang ngày càng trở nên đa dạng. Vậy nên, các mẹ cũng trở nên khắt khe hơn trong vấn đề chọn sữa cho bé để đảm bảo đó phải là loại sữa bột tốt nhất, đáp ứng đủ các tiêu chí mẹ đề ra như phát triển trí não, chiều cao, cân nặng, có sức đề kháng tốt, hỗ trợ tiêu hoá khoẻ, tránh táo bón,... Những tiêu chí trên cũng chính là xu hướng khi chọn sữa cho bé của bất kỳ mẹ nào.\n\nVì lẽ đó mà sữa Nhật Meiji, với thành phần dinh dưỡng cân bằng, giúp trẻ phát triển toàn diện và ổn định từ thể chất tới trí não đã trở thành lựa chọn số 1 của các mẹ.Đặc biệt, sữa Nhật Meiji còn được chia theo từng giai đoạn phát triển của bé, bao gồm:\n\nMeiji Infant Formula (dành cho bé từ 0-1 tuổi): Tập trung đến sự phát triển não bộ của trẻ, chú trọng đến khả năng miễn dịch, cân nhắc đến tình trạng tiêu hoá và hấp thu để điều chỉnh tình trạng phân của bé.\nNhìn chung, sữa Nhật Meiji trở thành xu hướng là nhờ đáp ứng đúng nhu cầu mà các mẹ hướng đến khi chọn mua sữa bột cho bé yêu. Bên cạnh đó, Meiji còn mang đến những sản phẩm hết sức tiện dụng khiến các mẹ càng hài lòng hơn. Một khi con đường chinh phục trái tim các mẹ đã thành công thì giá tiền với mẹ không còn là vấn đề nữa, bởi lẽ người mẹ nào không muốn đem những điều tốt đẹp nhất trên đời dành cho bé cưng của mẹ?",
                        Author = "Johnny D Vũ Lê",
                        ProductId = 1,
                        UploadDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        View = 0,
                        Like = 0,
                        ImageUrl = "/images/blogs/blog1.jpg"
                    },

                    new Blog
                    {
                        Title = "Vì sao ba mẹ nên chọn sữa Famna cho bé?",
                        Content = "Sữa Famna được sản xuất 100% tại Thụy Điển. Quốc gia này hội tụ nhiều chuyên gia dinh dưỡng và các nhà cung cấp nguyên liệu sạch hàng đầu Châu  u như: BASF (Đức), Lonza & Chr. Hansen (Đan Mạch),...\n\nThành phần Sữa Famna chứa hệ 2’-FL HMO, hay còn được biết đến là hoạt chất Prebiotics. Hoạt chất này có cấu trúc tương tự như dưỡng chất được tìm thấy trong sữa mẹ, giúp hình thành và nuôi dưỡng các loại vi khuẩn có lợi cho đường ruột của bé. Đồng thời, thành phần này cũng hỗ trợ ngăn chặn sự bám dính của các mầm bệnh trong đường ruột. Sức khỏe hệ tiêu hóa của bé nhờ vậy được tăng cường một cách đáng kể.\n\nSữa Famna bổ sung các thành phần hỗ trợ bé tăng cường sức đề kháng hiệu quả bao gồm: 2’-FL HMO và các loại Vitamin A, E, C. Các thành phần này được kết hợp với nhau theo một tỷ lệ hợp lý, giúp bé khỏe mạnh hơn và tránh được các bệnh nhiễm khuẩn thường gặp như: cảm, cúm, tiêu chảy,...\n\nMột công dụng tuyệt vời khác của dòng sữa này chính là giúp bé phát triển trí não tối ưu. Sữa Famna có thể mang lại công dụng này là nhờ chứa 100% DHA từ tảo tinh khiết, kết hợp cùng các dưỡng chất như: ARA, Axit Linoleic, Alpha Linolenic, Taurin, Lutein, Choline, I ốt, Sắt,... Các dưỡng chất này vừa giúp bé tăng cường khả năng nhận thức và ghi nhớ, vừa giúp bé cải thiện khả năng học tập một cách hiệu quả hơn.\n\nVới thành phần Canxi, Kẽm và Vitamin D3, sữa Famna giúp hệ xương và răng của bé phát triển chắc khỏe. Cũng chính nhờ những dưỡng chất này mà bé đạt được chiều cao lý tưởng theo chuẩn độ tuổi.\n\nKhông chỉ hỗ trợ bé phát triển chiều cao, sữa Famna còn nhận được nhiều lời khen của ba mẹ về công dụng giúp bé tăng cân hiệu quả. Để phát huy được công dụngnày thì không thể không nhắc đến các loại axit amin có nguồn gốc từ đạm sữa, chất béo và một số khoáng chất thiết yếu khác như: Kẽm, Magie,...",
                        Author = "Daniel V Tín Lê",
                        ProductId = 29,
                        UploadDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        View = 0,
                        Like = 0,
                        ImageUrl = "/images/blogs/blog2.jpg"
                    },

                    new Blog
                    {
                        Title = "Khám phá thành phần sữa ColosBaby Gold Pedia nào đặc biệt tốt cho trẻ suy dinh dưỡng, thấp còi",
                        Content = "Được sản xuất bởi Công Ty Cổ Phần Sữa VitaDairy Việt Nam, sữa ColosBaby Gold Pedia chứa thành phần sữa non ColosIgG 24h với hàm lượng kháng thể IgG cao. Nguyên liệu sữa non này được VitaDairy nhập khẩu độc quyền từ Tập đoàn PantheryX (Mỹ). Ngoài sữa non nhập khẩu từ Mỹ, các nguyên liệu còn lại được nhập khẩu trực tiếp từ New Zealand. Hơn nữa, sản phẩm còn được sản xuất trên dây chuyền công nghệ hiện đại. Do đó, ba mẹ hoàn toàn có thể an tâm về chất lượng tuyệt vời và sự an toàn khi cho bé sử dụng sữa non ColosBaby Gold Pedia.,\r\n  \r\n  Cụ thể, sữa ColosBaby Gold Pedia chứa đạm whey thuỷ phân, đạm whey cô đặc và đạm sữa giúp bé hấp thu dễ dàng. Hệ đạm này đậm độ chuẩn 1.0 kcal/ml, giúp cung cấp tới 600 kcal/ngày. Nhờ đó có thể bù đắp năng lượng thiếu hụt để trẻ tăng cân nhanh và bền vững.\r\n\r\nĐặc biệt, sản phẩm được bổ sung lợi khuẩn Bifidobacterium và chất xơ hoà tan FOS/Inulin. Bé vì vậy có thể tiêu hóa tốt lượng đạm trên. Còn đường tiêu hóa thì hoạt động khỏe mạnh, cân bằng và phòng ngừa táo bón hiệu quả.\r\n\r\nĐáng chú ý hơn khi vitamin K2 trong sữa ColosBaby Gold Pedia là loại vitamin K2 duy nhất có nguồn gốc từ thiên nhiên - MK7. Với MK7, khả năng định hướng được canxi vào xương trở nên hiệu quả, từ đó giúp cho xương chắc khỏe hơn. Hơn nữa, sữa ColosBaby Gold Pedia còn tích hợp thêm Photpho và Magie nên công dụng giúp bé phát triển chiều cao càng hiệu quả.\r\n\r\nTuy không tác động trực tiếp đến khả năng tăng trưởng chiều cao và cân nặng, song kháng thể IgG giúp hệ miễn dịch của bé trở nên mạnh mẽ hơn. IgG tạo hàng rào bảo vệ hệ hô hấp, hệ tiêu hóa, giúp tiêu diệt và ngăn chặn các yếu tố gây bệnh như vi khuẩn, virus. Cơ thể khỏe mạnh cũng là điều kiện cần cho việc phát triển chiều cao và tăng trưởng cân nặng. \r\n\r\nBổ sung kháng thể IgG từ sữa non ColosIgG 24h là cách tăng cường miễn dịch trực tiếp để bảo vệ cơ thể khỏi các tác nhân gây bệnh, giúp bé luôn khỏe mạnh. Thêm nữa, việc kết hợp Lactoferrin - là một dưỡng chất tự nhiên được tìm thấy trong sữa mẹ có khả năng gắn kết phân tử sắt - giúp tăng khả năng tiêu diệt vi khuẩn, tăng cường miễn dịch.\r\n",
                        Author = "Edward M Nhân Đụt",
                        ProductId = 66,
                        UploadDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        View = 0,
                        Like = 0,
                        ImageUrl = "/images/blogs/blog3.jpg"
                    },

                    new Blog
                    {
                        Title = "Vì sao ba mẹ nên chọn sữa Friso Gold cho bé tiêu hóa kém?",
                        Content = "Đến từ Hà Lan, sữa Friso Gold thuộc tập đoàn FrieslandCampina - một tập đoàn sữa liên hợp lớn nhất trên thế giới với hơn 150 năm kinh nghiệm. Các sản phẩm của tập đoàn này đã có mặt ở hơn 100 quốc gia trên thế giới, trong đó có Việt Nam. \r\n\r\nĐến Việt Nam từ năm 1995, các sản phẩm của FrieslandCampina đang ngày càng được người tiêu dùng Việt tin chọn bởi uy tín của tập đoàn và chất lượng của sản phẩm. Tất cả sản phẩm đều được cam kết về chất lượng, đảm bảo đạt tiêu chuẩn của Cục vệ sinh an toàn thực phẩm và của Bộ Y Tế.\r\n\r\nBa mẹ có biết, sức khỏe của hệ tiêu hóa liên quan mật thiết tới chế độ dinh dưỡng và khẩu phần ăn uống. Đối với trẻ sơ sinh và trẻ nhỏ, sữa là thức ăn có giá trị dinh dưỡng cao, đáp ứng tốt nhu cầu phát triển của trẻ. \r\n\r\nTuy nhiên, hệ tiêu hóa của trẻ lúc này còn non yếu và chưa hoàn thiện về mặt chức năng. Do đó, trẻ khó có thể dung nạp đầy đủ các dưỡng chất từ sữa. Nhưng nếu sữa có cấu trúc đạm mềm nhỏ và dễ tiêu, thì việc dung nạp dưỡng chất từ sữa sẽ dễ dàng hơn, từ đó giúp con phát triển cả về thể chất và tinh thần trong giai đoạn “vàng” đầu đời.\r\n\r\nNhờ có nguồn gốc xuất xứ và quy trình xử lý đặc biệt, nên thành phần đạm trong sữa Friso Gold rất tốt cho hệ tiêu hóa của trẻ sơ sinh và trẻ nhỏ. Cụ thể hơn:\r\n\r\n.Sữa Friso Gold được làm từ 100% nguồn sữa từ chính các trang trại tại Hà Lan - giống bò thuần chủng nhất Châu  u. Đây là nguồn sữa NOVAS Signature Milk, chứa loại đạm mềm, nhỏ và hoàn toàn tự nhiên. \r\n\r\nSữa Friso Gold ứng dụng quy trình xử lý một lần nhiệt - Một cách đơn giản, sữa được vận chuyển trực tiếp từ trang trại của tập đoàn về nhà máy sản xuất, nên nguồn sữa chỉ cần được sấy khô một lần duy nhất, mà không phải trải qua nhiều quy trình sấy nhiệt. Quy trình độc đáo này giúp bảo toàn 90% đạm mềm nhỏ và dễ tiêu trong quá trình xử lý nhiệt, tránh tuyệt đối tình trạng đạm biến tính, vón cục gây khó tiêu.  \r\n\r\nBên cạnh thành phần đạm đặc biệt như Con Cưng đã trình bày ở trên, sữa Friso Gold còn chứa chất xơ GOS với hàm lượng cao giúp tăng cường lợi khuẩn Bifidus và cải thiện hệ tiêu hóa của trẻ. Thêm nữa, sữa Friso Gold không chứa đường Sucrose nên có vị thanh nhạt và tự nhiên. Bé nhờ vậy mà dễ dàng làm quen và tiếp nhận hơn. Với những ưu điểm vượt trội này, sữa Friso Gold đã nằm trong top các dòng sữa dành cho trẻ táo bón được ưa chọn hàng đầu hiện nay.\r\n",
                        Author = "Vigor H Đạt NG",
                        ProductId = 56,
                        UploadDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        View = 0,
                        Like = 0,
                        ImageUrl = "/images/blogs/blog4.jpg"
                    },

                    new Blog
                    {
                        Title = "Review sữa Hikid dê có tốt cho bé không? Giá bao nhiêu?",
                        Content = "Sữa Hikid dê bổ sung cho bé thành phần Arginine, chất tăng trưởng CBP, IgF, Canxi và vitamin K2 giúp bé phát triển chiều cao tối đa. Đặc biệt, thành phần Canxi trong sữa Hikid dê có hàm lượng cao 600mg/100gr bột sữa, giúp đáp ứng đủ 100% nhu cầu hàng ngày của bé 5 tuổi.\n\nBên cạnh công dụng phát triển chiều cao, sữa Hikid dê còn hỗ trợ bé tăng trưởng về cân nặng. Như đã đề cập, sữa Hikid dê chứa tới 45 dưỡng chất cần thiết được phối trộn theo công thức độc quyền của Ildong. Công thức này tập trung tối ưu khả năng chuyển hóa để cơ thể bé hấp thu được nhiều dưỡng chất nhất, từ đó tăng cân khỏe mạnh.\n\nChưa hết, sữa Hikid dê có nguồn gốc từ sữa dê nên rất dễ tiêu hóa và hấp thu. Đặc điểm này có được là bởi kích thước phân tử đạm và chất béo trong sữa dê nhỏ gấp 20 lần so với trong sữa bò. Theo đó, sữa Hikid dê đặc biệt phù hợp cho các bé kém hấp thu, nhẹ cân, thấp còi, suy dinh dưỡng, giúp bé nhanh chóng bắt kịp đà tăng trưởng.\n\nNgoài ra, sữa Hikid dê còn giúp tăng cường sức đề kháng khỏe mạnh nhờ chứa thành phần sữa non. Hàm lượng sữa non này cung cấp các yếu tố miễn dịch tự nhiên như IgG, IgF, TgF và slgA, hỗ trợ tạo hàng rào miễn dịch cho cơ thể bé. Chưa hết, sữa Hikid dê còn chứa các chất chống Oxy hóa gồm: glutathione, senlium, Beta Caroten giúp ngăn chặn mầm bệnh và giữ cho tế bào khỏe mạnh.\n\nSữa Hikid dê cung cấp bộ 10 vi chất phát triển trí não cho bé gồm: DHA, Taurin, Choline, Lecithin, Alpha linoleic axit, Phosphatidylcoline, phosphatidylethanolamine, phosphatidylinositol, Inositol, Linoleic axit. Sự kết hợp của các thành phần này cùng với Kẽm hỗ trợ tăng cấu tạo chất xám, tăng liên kết thần kinh giúp bé nhận thức nhanh và ghi nhớ lâu.\n\nĐể sữa phát huy tốt các công dụng, ba mẹ nên cho bé uống 3 ly sữa mỗi ngày vào buổi sáng, buổi chiều sau khi vận động và buổi tối trước khi đi ngủ. Một tin vui cho các mẹ thích dòng sữa dê, sữa Hikid dê phiên bản Nâng cấp 2023 đã được xử lý mùi tự nhiên nên có hương vị nguyên kem thơm ngon và dễ uống. Ba mẹ yêu thích sữa dê có thể yên tâm chọn cho bé yêu nhé.",
                        Author = "Vigor H Đạt NG",
                        ProductId = 40,
                        UploadDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        View = 0,
                        Like = 0,
                        ImageUrl = "/images/blogs/blog5.jpg"
                    },

                    new Blog
                    {
                        Title = "Khám phá 3 lý do mẹ nên cho bé uống sữa Bubs Organic số 2",
                        Content = "Nguồn nguyên liệu của sữa Bubs Organic số 2 được thu từ đàn bò tại Úc và Newzealand. Điều đặc biệt nằm ở chỗ đàn bò này được chăn nuôi bằng phương pháp hữu cơ tại các trang trại sạch bậc nhất ở 2 đất nước trên. Cụ thể hơn, đàn bò nơi đây được nuôi hoàn toàn bằng cỏ tự nhiên trên cánh đồng cỏ đạt tiêu chí 3 không: không sử dụng hóa chất, thuốc trừ sâu; không dùng phân bón hóa học; không sử dụng hạt giống biến đổi gen. Bên cạnh đó, đàn bò còn được chăm sóc rất cẩn thận từ khâu vệ sinh đến các hoạt động thư giãn... để cho ra chất lượng sữa tốt nhất.  \r\n\r\nChưa kể, quy trình sản xuất sữa Bubs Organic số 2 cũng được kiểm soát nghiêm ngặt từ nguyên liệu đầu vào đến các khâu chế biến, đóng gói, bảo quản, phân phối… Tất cả các khâu đều đáp ứng nghiêm ngặt tiêu chuẩn chất lượng cũng như vấn đề vệ sinh an toàn thực phẩm. \r\n\r\nChính nhờ những ưu điểm kể trên, sữa Bubs Organic số 2 vinh dự đạt được chứng nhận NASAA, ACO và Clean Label Project™ nổi tiếng trên thế giới. Theo đó, sản phẩm đảm bảo không chứa kim loại nặng, kháng sinh, chất bảo quản hay thành phần gây biến đổi gen (Non-GMO). Vì vậy, sữa Bubs Organic số 2 là dòng sữa an toàn cho bé mà ba mẹ không nên bỏ qua. \r\n\r\nƯu điểm nổi bật tiếp theo của sản phẩm là về thành phần. Thành phần dưỡng chất của sữa Bubs Organic số 2 rất đa dạng, đảm bảo cung cấp đầy đủ dinh dưỡng cần thiết cho sự phát triển toàn diện của trẻ. Cụ thể:\r\n\r\n. Canxi, vitamin D, A, C, kẽm, sắt, iốt,... giúp hệ xương, răng phát triển chắc khỏe và tăng khả năng miễn dịch. \r\n\r\n. Omega-3 DHA và Omega-6 ARA giúp bé phát triển trí não và thông minh hơn;\r\n\r\n. Prebiotic GOS và Probiotics BB536 giúp hệ vi sinh đường ruột cân bằng và phát triển khoẻ mạnh. \r\n\r\n. Hàm lượng CLA (Omega-6) nhiều hơn 500% so với sữa bò thông thường. Đây là loại chất béo chuyển hóa tự nhiên có trong các thực phẩm lành mạnh, có tác dụng giúp bé tăng cường hệ miễn dịch. \r\n\r\nBa mẹ biết không, DHA là dưỡng chất quan trọng góp phần phát triển trí não, trí thông minh của trẻ, nhất là trong giai đoạn 1000 ngày đầu đời. Không chỉ vậy, DHA chiếm tỉ trọng rất cao trong võng mạc (gần 60%), nên rất cần thiết cho sự phát triển chức năng nhìn của mắt. Thành phần này cũng có trong công thức sữa Bubs Organic số 2, DHA trong sữa Bubs Organic số 2 còn đặc biệt hơn nhiều vì được chiết xuất từ nguồn gốc thực vật, cụ thể là tảo biển. \r\n\r\nSo với DHA có nguồn gốc từ động vật, thì nguồn DHA từ tảo biển không có vị hay mùi tanh, nên bé dễ uống và dễ dung nạp hơn. Cũng nhờ đặc điểm nầy, bé có thể tránh được tình trạng nôn trớ khi uống sữa. Càng yên tâm hơn khi tảo được nuôi trồng và chiết xuất trực tiếp nên đảm bảo không bị nhiễm kim loại nặng hay thuỷ ngân. Vì vậy, bé hoàn toàn nhận được những dưỡng chất sạch và an toàn khi sử dụng sữa Bubs Organic số 2.\r\n",
                        Author = "Extonaldo Meverlyn",
                        ProductId = 68,
                        UploadDate = DateTime.Now,
                        UpdateDate = DateTime.Now,
                        View = 0,
                        Like = 0,
                        ImageUrl = "/images/blogs/blog6.jpg"
                    },

                };
                foreach (var blog in blogs)
                {
                    context.Blogs.Add(blog);
                }
                context.SaveChanges();
            }

        }
    }
}

