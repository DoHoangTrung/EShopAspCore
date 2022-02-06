using EshopAspCore.Data.Entity;
using EshopAspCore.Data.Enum;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Data.Extensions
{
    public static class ModelBuilerExtensions
    {
        public static void Seed(this ModelBuilder modelBuiler)
        {
            modelBuiler.Entity<AppConfig>().HasData(
                new AppConfig() { Key = "HomeTitle", Value = "This is homepage of Eshop" },
                new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of Eshop" },
                new AppConfig() { Key = "HomeDescription", Value = "This is discription of Eshop" }
            );

            modelBuiler.Entity<Language>().HasData(
                new Language() { Id = "vi", Name = "Tiếng Việt", IsDefault = true },
                new Language() { Id = "en", Name = "English", IsDefault = false }
            );

            modelBuiler.Entity<Category>().HasData(
                new Category()
                {
                    Id = 1,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 1,
                    Status = Status.Active,

                },
                new Category()
                {
                    Id = 2,
                    IsShowOnHome = true,
                    ParentId = null,
                    SortOrder = 2,
                    Status = Status.Active,

                });

            modelBuiler.Entity<CategoryTranslation>().HasData(
                new CategoryTranslation()
                {
                    Id = 1,
                    CategoryId = 1,
                    Name = "Áo nam",
                    LanguageId = "vi",
                    SeoAlias = "ao-nam",
                    SeoDescription = "Sản phẩm áo thời trang nam",
                    SeoTitle = "Sản phẩm áo thời trang nam"
                },
                new CategoryTranslation()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Men shirt",
                    LanguageId = "en",
                    SeoAlias = "men-shirt",
                    SeoDescription = "The shirt produtcs for men",
                    SeoTitle = "The shirt producs for men",
                }, new CategoryTranslation()
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Áo nữ",
                    LanguageId = "vi",
                    SeoAlias = "ao-nu",
                    SeoDescription = "Sản phẩm áo thời trang nu",
                    SeoTitle = "Sản phẩm áo thời trang nu"
                },
                new CategoryTranslation()
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "Women shirt",
                    LanguageId = "en",
                    SeoAlias = "Women-shirt",
                    SeoDescription = "The shirt produtcs for women",
                    SeoTitle = "The shirt producs for women",
                });

            modelBuiler.Entity<Product>().HasData(
                new Product()
                {
                    Id = 1,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 100000,
                    Price = 200000,
                    Stock = 0,
                    ViewCount = 0,
                    IsFeatured = true,
                });

            modelBuiler.Entity<ProductTranslation>().HasData(
                new ProductTranslation()
                {
                    Id = 1,
                    ProductId = 1,
                    Name = "Áo hoodie nam",
                    LanguageId = "vi",
                    SeoAlias = "ao-hoodie-nam",
                    SeoDescription = "Sản phẩm áo hoodie nam",
                    SeoTitle = "Sản phẩm áo hoodie nam"
                },
                new ProductTranslation()
                {
                    Id = 2,
                    ProductId = 1,
                    Name = "hoodie shirt for men",
                    LanguageId = "en",
                    SeoAlias = "hoodie-shirt-for-men",
                    SeoDescription = "The hoodie shirt produtcs for men",
                    SeoTitle = "The hoodie shirt products for men",
                }
                );

            modelBuiler.Entity<ProductInCategory>().HasData(
               new List<ProductInCategory>()
                    {
                        new ProductInCategory(){CategoryId=1, ProductId=1}
                    }
               );


            var ADMIN_ID = new Guid("DB9ED923-492B-467A-97E4-EE81C9DE0A64");
            var ROLE_ID = new Guid("2A905B66-98FB-4E82-9D98-5CF68EBB16EA");

            modelBuiler.Entity<AppRole>().HasData(
                new AppRole()
                {
                    Id = ROLE_ID,
                    Name = "admin",
                    NormalizedName = "admin",
                    Description = "Administrator role"
                },
                new AppRole()
                {
                    Id = Guid.NewGuid(),
                    Name = "user",
                    NormalizedName = "user",
                    Description = "User role"
                });

            var hasher = new PasswordHasher<AppUser>();
            var seedUser = new AppUser
            {
                Id = ADMIN_ID,
                UserName = "trung123",
                NormalizedUserName = "trung123",
                Email = "some-admin-email@nonce.fake",
                NormalizedEmail = "some-admin-email@nonce.fake",
                EmailConfirmed = true,
                SecurityStamp = string.Empty,
                FirstName = "Trung",
                LastName = "Do",
                Dob = new DateTime(1998, 3, 18),
            };

            seedUser.PasswordHash = hasher.HashPassword(seedUser, "Trung123$");
            modelBuiler.Entity<AppUser>().HasData(seedUser);

            modelBuiler.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });

            modelBuiler.Entity<Slide>().HasData(
                 new Slide()
                 {
                     Id = 1,
                     Name = "Second Thumbnail label",
                     Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
                     SortOrder = 1,
                     Url = "#",
                     Image = "/themes/images/carousel/1.png",
                     Status = Status.Active,
                 },
                new Slide()
                {
                     Id =2,
                    Name = "Second Thumbnail label",
                    Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
                    SortOrder = 2,
                    Url = "#",
                    Image = "/themes/images/carousel/2.png",
                    Status = Status.Active,
                },
                new Slide()
                {
                     Id = 3,
                    Name = "Second Thumbnail label",
                    Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
                    SortOrder = 3,
                    Url = "#",
                    Image = "/themes/images/carousel/3.png",
                    Status = Status.Active,
                },
                new Slide()
                {
                     Id = 4,
                    Name = "Second Thumbnail label",
                    Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
                    SortOrder = 4,
                    Url = "#",
                    Image = "/themes/images/carousel/4.png",
                    Status = Status.Active,
                },
                new Slide()
                {
                     Id = 5,
                    Name = "Second Thumbnail label",
                    Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
                    SortOrder = 5,
                    Url = "#",
                    Image = "/themes/images/carousel/5.png",
                    Status = Status.Active,
                },
                new Slide()
                {
                     Id = 6,
                    Name = "Second Thumbnail label",
                    Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
                    SortOrder = 6,
                    Url = "#",
                    Image = "/themes/images/carousel/6.png",
                    Status = Status.Active,
                });
        }
    }
}
