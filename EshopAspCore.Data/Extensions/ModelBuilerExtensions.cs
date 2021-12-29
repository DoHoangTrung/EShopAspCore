using EshopAspCore.Data.Entities;
using EshopAspCore.Data.Enum;
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
                new Language() { Id = "vi-VN", Name = "Tiếng Việt", IsDefault = true },
                new Language() { Id = "en-US", Name = "English", IsDefault = false }
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
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-nam",
                    SeoDescription = "Sản phẩm áo thời trang nam",
                    SeoTitle = "Sản phẩm áo thời trang nam"
                },
                new CategoryTranslation()
                {
                    Id = 2,
                    CategoryId = 1,
                    Name = "Men shirt",
                    LanguageId = "en-US",
                    SeoAlias = "men-shirt",
                    SeoDescription = "The shirt produtcs for men",
                    SeoTitle = "The shirt producs for men",
                }, new CategoryTranslation()
                {
                    Id = 3,
                    CategoryId = 2,
                    Name = "Áo nữ",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-nu",
                    SeoDescription = "Sản phẩm áo thời trang nu",
                    SeoTitle = "Sản phẩm áo thời trang nu"
                },
                new CategoryTranslation()
                {
                    Id = 4,
                    CategoryId = 2,
                    Name = "Women shirt",
                    LanguageId = "en-US",
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

                });

            modelBuiler.Entity<ProductTranslation>().HasData(
                new ProductTranslation()
                {
                    Id = 1,
                    ProductId = 1,
                    Name = "Áo hoodie nam",
                    LanguageId = "vi-VN",
                    SeoAlias = "ao-hoodie-nam",
                    SeoDescription = "Sản phẩm áo hoodie nam",
                    SeoTitle = "Sản phẩm áo hoodie nam"
                },
                new ProductTranslation()
                {
                    Id = 2,
                    ProductId = 1,
                    Name = "hoodie shirt for men",
                    LanguageId = "en-US",
                    SeoAlias = "hoodie-shirt-for-men",
                    SeoDescription = "The hoodie shirt produtcs for men",
                    SeoTitle = "The hoodie shirt products for men",
                }
                ) ;

            modelBuiler.Entity<ProductInCategory>().HasData(
               new List<ProductInCategory>()
                    {
                        new ProductInCategory(){CategoryId=1, ProductId=1}
                    }
               );
        }
    }
}
