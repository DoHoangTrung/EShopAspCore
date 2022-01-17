using EshopAspCore.Data.EF;
using EshopAspCore.ViewModels.Catalog.Categories;
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
                            Name = ct.Name
                        });

            var categories = await query.ToListAsync();
            return categories;
        }
    }
}
