using EshopAspCore.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EshopAspCore.ViewModels.Catalog.Products.Manage
{
    public class ProductUpdateRequest
    {
        public int Id { get; set; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }

        [Required]
        public string SeoAlias { get; set; }
        public string LanguageId { set; get; }
        public List<SelectedItem> CategoryIds{ set; get; }
        public IFormFile ThumbNailImage { get; set; }
        public bool? IsFeatured { get; set; }
        public string ImagePath { get; }
    }
}
