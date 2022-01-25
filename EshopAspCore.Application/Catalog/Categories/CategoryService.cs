using EshopAspCore.Data.EF;
using EshopAspCore.Data.Entity;
using EshopAspCore.ViewModels.Catalog.Categories;
using EshopAspCore.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly EshopDbContext _context;

        public CategoryService(EshopDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryViewModel>> GetAll(string languageId)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId
                        select (new CategoryViewModel()
                        {
                            Id = c.Id,
                            Name = ct.Name,
                            ParentId = c.ParentId
                        });

            var categories = await query.ToListAsync();
            return categories;
        }

        public async Task<List<CategoryViewModel>> GetById(int productId, string languageId)
        {
            //return list{id, name by language} by id product
            var categories = await (from pic in _context.ProductInCategories
                                    join ct in _context.CategoryTranslations on pic.CategoryId equals ct.CategoryId
                                    where pic.ProductId == productId && ct.LanguageId == languageId
                                    select new CategoryViewModel() 
                                    {
                                        Id = ct.Id,
                                        Name = ct.Name
                                    }).ToListAsync();

            return categories;
        }

        public async Task<bool> Update(int productId, List<SelectedItem> items)
        {
            var currentCategories = await (from pic in _context.ProductInCategories
                                           join c in _context.Categories on pic.CategoryId equals c.Id
                                           where pic.ProductId == productId
                                           select pic.CategoryId).ToListAsync();

            foreach (var item in items)
            {
                bool selected = item.Selected;
                bool isExist = currentCategories.Contains(int.Parse(item.Id));

                var productInCategory = new ProductInCategory()
                {
                    CategoryId = int.Parse(item.Id),
                    ProductId = productId
                };

                if (selected && isExist == false)
                {
                    //insert
                    await _context.ProductInCategories.AddAsync(productInCategory);
                }
                else if (selected == false && isExist)
                {
                    //remove
                    _context.ProductInCategories.Remove(productInCategory);
                }
            }

            var numberAffected = await _context.SaveChangesAsync();

            if (numberAffected > 0)
                return true;
            else
                return false;
        }
    }
}
