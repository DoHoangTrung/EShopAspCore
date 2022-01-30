using EshopAspCore.ViewModels.Catalog.Categories;
using EshopAspCore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Application.Catalog.Categories
{
    public interface ICategoryService
    {
        Task<List<CategoryViewModel>> GetAll(string languageId);
        Task<List<CategoryViewModel>> GetByProductId(int productId,string languageId);
        Task<bool> Update(int productId, List<SelectedItem> categoryIds);

        Task<CategoryViewModel> GetById(int id, string languageId);
    }
}
