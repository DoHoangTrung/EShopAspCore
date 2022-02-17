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
            const int cannonNumber = 7;
            const int cardNumber = 4;

            const int idxStartCannon = 1;
            const int idxEndCannon = idxStartCannon + cannonNumber - 1;
            const int idxStartCard = idxEndCannon + 1;
            const int idxEndCard = idxStartCard + cardNumber - 1; //(4): number of card
            int idValue;

            #region AppConfig
            modelBuiler.Entity<AppConfig>().HasData(
                new AppConfig() { Key = "HomeTitle", Value = "This is homepage of Eshop" },
                new AppConfig() { Key = "HomeKeyword", Value = "This is keyword of Eshop" },
                new AppConfig() { Key = "HomeDescription", Value = "This is discription of Eshop" }
            );
            #endregion

            #region Language
            modelBuiler.Entity<Language>().HasData(
                new Language() { Id = "vi", Name = "Tiếng Việt", IsDefault = true },
                new Language() { Id = "en", Name = "English", IsDefault = false }
            );
            #endregion

            #region Category
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
                    Name = "Máy ảnh",
                    LanguageId = "vi",
                    SeoAlias = "may-anh",
                    SeoDescription = "Sản phẩm máy ảnh",
                    SeoTitle = "Sản phẩm máy ảnh"
                },
                new CategoryTranslation()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Cannon",
                    LanguageId = "en",
                    SeoAlias = "Cannon",
                    SeoDescription = "The cheap cannon",
                    SeoTitle = "Cannon",
                }, new CategoryTranslation()
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Thẻ nhớ",
                    LanguageId = "vi",
                    SeoAlias = "the-nho",
                    SeoDescription = "Sản phẩm thẻ nhớ",
                    SeoTitle = "Sản phẩm thẻ nhớ tốt"
                },
                new CategoryTranslation()
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "Memory stick",
                    LanguageId = "en",
                    SeoAlias = "Memory-stick",
                    SeoDescription = "The best memory stick",
                    SeoTitle = "Memory stick",
                });

            #endregion

            #region Product
            //product
            var products = new List<Product>();
            for (int i = idxStartCannon; i <= idxEndCannon; i++)
            {
                products.Add(new Product()
                {
                    Id = i,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 100000,
                    Price = 200000,
                    Stock = 20,
                    ViewCount = 0,
                    IsFeatured = true,
                });
            }

            for (int i = idxStartCard; i <= idxEndCard; i++)
            {
                products.Add(new Product()
                {
                    Id = i,
                    DateCreated = DateTime.Now,
                    OriginalPrice = 50000,
                    Price = 100000,
                    Stock = 30,
                    ViewCount = 0,
                    IsFeatured = true,
                });
            }

            modelBuiler.Entity<Product>().HasData(products);

            //product translation
            var tranProducts = new List<ProductTranslation>();
            idValue = 1;
            for (int i = idxStartCannon; i <= idxEndCannon; i++)
            {
                tranProducts.Add(new ProductTranslation()
                {
                    Id = idValue,
                    ProductId = i,
                    Name = $"Máy ảnh { i }",
                    LanguageId = "vi",
                    SeoAlias = "may-anh",
                    SeoTitle = "Sản phẩm máy ảnh",
                    SeoDescription = "Sản phẩm máy ảnh tốt"
                });
                idValue++;

                tranProducts.Add(new ProductTranslation()
                {
                    Id = idValue,
                    ProductId = i,
                    Name = $"Cannon {i}",
                    LanguageId = "en",
                    SeoAlias = "cannon",
                    SeoTitle = "cannon",
                    SeoDescription = "The best cannon",
                });
                idValue++;
            }

            for (int i = idxStartCard; i <= idxEndCard; i++)
            {
                tranProducts.Add(new ProductTranslation()
                {
                    Id = idValue,
                    ProductId = i,
                    Name = $"Thẻ nhớ { i }",
                    LanguageId = "vi",
                    SeoAlias = "the-nho",
                    SeoTitle = "Sản phẩm thẻ nhớ",
                    SeoDescription = "Sản phẩm thẻ nhớ tốt"
                });
                idValue++;

                tranProducts.Add(new ProductTranslation()
                {
                    Id = idValue,
                    ProductId = i,
                    Name = $"Memory card {i}",
                    LanguageId = "en",
                    SeoAlias = "memory-card",
                    SeoTitle = "memory-card",
                    SeoDescription = "The best memory card",
                });
                idValue++;
            }
            modelBuiler.Entity<ProductTranslation>().HasData(tranProducts);

            //product in category
            
            for (int i = idxStartCannon; i <= idxEndCannon; i++)
            {
                modelBuiler.Entity<ProductInCategory>().HasData(
                new List<ProductInCategory>()
                     {
                        new ProductInCategory(){CategoryId=1, ProductId=i}
                     }
                );
            }
            for (int i = idxStartCard; i <= idxEndCard; i++)
            {
                modelBuiler.Entity<ProductInCategory>().HasData(
                new List<ProductInCategory>()
                     {
                        new ProductInCategory(){CategoryId=2, ProductId=i}
                     }
                );
            }
            

            //add product image 
            var images = new List<ProductImage>();
            idValue = 1;
            for (int i = 1; i <= cannonNumber; i++)
            {
                images.Add(new ProductImage()
                {
                    Id = idValue,
                    Caption = "ThumbnailImage",
                    DateCreated = DateTime.Now,
                    FileSize = 5000,
                    IsDefault = true,
                    ProductId = i,
                    SortOrder = 1,
                    ImagePath = $"m{i}.jpg"
                });
                idValue++;
            }
            for (int i = 1; i <= cardNumber; i++)
            {
                images.Add(new ProductImage()
                {
                    Id = idValue,
                    Caption = "ThumbnailImage",
                    DateCreated = DateTime.Now,
                    FileSize = 5000,
                    IsDefault = true,
                    ProductId = i + idxEndCannon,
                    SortOrder = 1,
                    ImagePath = $"t{i}.jpg"
                });
                idValue++;
            }
            modelBuiler.Entity<ProductImage>().HasData(images);


            #endregion

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

            #region Slides
            var slides = new List<Slide>();
            for (int i = 1; i <= 6; i++)
            {
                slides.Add(new Slide()
                {
                    Id = i,
                    Name = "Second Thumbnail label",
                    Description = "Cras justo odio, dapibus ac facilisis in, egestas eget quam. Donec id elit non mi porta gravida at eget metus. Nullam id dolor id nibh ultricies vehicula ut id elit.",
                    SortOrder = i,
                    Url = "#",
                    Image = $"/themes/images/carousel/{i}.png",
                    Status = Status.Active,
                });
            }

            modelBuiler.Entity<Slide>().HasData(slides);

            #endregion
        }
    }
}
