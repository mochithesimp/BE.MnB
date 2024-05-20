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
                        Description = ""
                    },
                    new Category
                    {
                        Name = "Sữa hạt",
                        Description = ""
                    },
                    new Category
                    {
                        Name = "Thức uống dinh dưỡng",
                        Description = ""
                    },
                    new Category
                    {
                        Name = "Sữa tươi, sữa chua",
                        Description = ""
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
                        ImageBrandUrl = "/images/brands/meiji.jpg"
                    },
                    new Brand
                    {
                        Name = "TH True Milk",
                        ImageBrandUrl = "/images/brands/thtruemilk.jpg"
                    },
                    new Brand
                    {
                        Name = "Fruto Nyanyan",
                        ImageBrandUrl = "/images/brands/FrutoNyanya.jpg"
                    },
                    new Brand
                    {
                        Name = "Sahmyook",
                        ImageBrandUrl = "/images/brands/Sahmyook.jpg"
                    },
                    new Brand
                    {
                        Name = "137 Degrees",
                        ImageBrandUrl = "/images/brands/137degree.jpg"
                    },
                    new Brand
                    {
                        Name = "Nestle",
                        ImageBrandUrl = "/images/brands/Nestle.jpg"
                    },
                    new Brand
                    {
                        Name = "HOFF",
                        ImageBrandUrl = "/images/brands/HOFF.jpg"
                    },
                    new Brand
                    {
                        Name = "Yakult",
                        ImageBrandUrl = "/images/brands/Yakult.jpg"
                    },
                    new Brand
                    {
                        Name = "Vinamilk",
                        ImageBrandUrl = "/images/brands/Vinamilk.jpg"
                    },
                    new Brand
                    {
                        Name = "PediaSure",
                        ImageBrandUrl = "/images/brands/PediaSure.jpg"
                    },
                    new Brand
                    {
                        Name = "Similac",
                        ImageBrandUrl = "/images/brands/Similac.jpg"
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
                        Address = "Viet Nam"
                    },
                    new User
                    {
                        RoleId = 2,
                        Email = "tin@tin.com",
                        Name = "Quốc Tín",
                        Password = "123",
                        PhoneNumber = "0909090909",
                        Address = "Hà Nội"
                    },
                    new User
                    {
                        RoleId = 1,
                        Email = "admin@admin.com",
                        Name = "admin",
                        Password = "123",
                        PhoneNumber = "0808080808",
                        Address = "Viet Nam"
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

