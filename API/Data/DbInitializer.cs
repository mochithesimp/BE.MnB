using API.Entities;

namespace API.Data
{
    public static class DbInitializer
    {

        public static void Initialize(StoreContext context)
        {
            if (context.Products.Any()) return;

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
            };

            foreach (var product in products)
            {
                context.Products.Add(product);
            }
            //context.Products.AddRange(products);

            context.SaveChanges();
        }
    }
}

