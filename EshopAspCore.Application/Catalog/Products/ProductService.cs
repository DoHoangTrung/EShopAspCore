using EshopAspCore.Application.Catalog.Categories;
using EshopAspCore.Application.Common;
using EshopAspCore.Data.EF;
using EshopAspCore.Data.Entity;
using EshopAspCore.Utilities.Constants;
using EshopAspCore.Utilities.Exceptions;
using EshopAspCore.Utilities.ExtensionMethods;
using EshopAspCore.ViewModels.Catalog.ProductImages;
using EshopAspCore.ViewModels.Catalog.Products;
using EshopAspCore.ViewModels.Catalog.Products.Manage;
using EshopAspCore.ViewModels.Catalog.Products.Public;
using EshopAspCore.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace EshopAspCore.Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly EshopDbContext _context;
        private readonly IStorageService _storageService;
        private readonly ICategoryService _categoryService;

        public ProductService(EshopDbContext context, IStorageService storageService, ICategoryService categoryService)
        {
            _context = context;
            _storageService = storageService;
            _categoryService = categoryService;
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                ProductId = productId,
                Caption = request.Caption,
                DateCreated = DateTime.Now,
                IsDefault = request.IsDefault,
                SortOrder = request.SortOrder
            };

            //save image
            if (request.ImageFile != null)
            {
                productImage.ImagePath = await SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }

            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            //create current language translation + empty translation for other languages
            var languages = _context.Languages;

            var trans = new List<ProductTranslation>();

            foreach (var lang in languages)
            {
                if (lang.Id == request.LanguageId)
                {
                    trans.Add(new ProductTranslation()
                    {
                        Name = request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        LanguageId = request.LanguageId,
                    });
                }
                else
                {
                    trans.Add(new ProductTranslation()
                    {
                        Name = request.Name,
                        SeoAlias = SystemConstants.NA,
                        LanguageId = lang.Id,
                    });
                }
            }

            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = trans
            };
            //save image
            if (request.ThumbNailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbNailImage.Length,
                        ImagePath = await SaveFile(request.ThumbNailImage),
                        IsDefault = request.IsDefault,
                        SortOrder = 1
                    }
                };
            }

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new EshopException($"Cannot find a product: {productId}");

            var images = _context.ProductImages.Where(i => i.ProductId == productId);
            foreach (var img in images)
            {
                await _storageService.DeleteFileAsync(img.ImagePath);
            }

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
        {
            //1. Select join
            var query = from p in _context.Products
                        from pic in _context.ProductInCategories.Where(x => x.ProductId == p.Id).DefaultIfEmpty()
                        from c in _context.Categories.Where(c => pic.CategoryId == c.Id).DefaultIfEmpty()
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join l in _context.Languages on pt.LanguageId equals l.Id
                        where pt.LanguageId == request.LanguageId
                        select new { p, pic, pt, c, l };

            

            //2. filter
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                var keywords = request.Keyword.ToSearchFormat();
                var compareInfo = CultureInfo.InvariantCulture.CompareInfo;
                var options = CompareOptions.IgnoreCase |
                            CompareOptions.IgnoreSymbols |
                            CompareOptions.IgnoreNonSpace;

                /*linq-where-ignore-accentuation-and-case*/
                query = from q in query
                           where EF.Functions.Like(EF.Functions.Collate(q.pt.Name, "Latin1_General_CI_AI"), $"%{keywords}%")
                           select q;

                //query = query.Where(x => compareInfo.IndexOf(x.pt.Name, keywords, options) > -1);
            }

            if (request.CategoryId.HasValue)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }

            //3. Paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    Language = x.l.Name,
                    CategoryId = x.pic != null ? x.pic.CategoryId : null,

                })
                .ToListAsync();

            data = data.GroupBy(x => x.Id).Select(x => x.First()).ToList();

            //sort
            switch (request.SelectionSortOrder)
            {
                case SystemConstants.SelectionSortOrder.PriceLowestFirst:
                    data = data.OrderBy(x => x.Price).ToList();
                    break;
                case SystemConstants.SelectionSortOrder.ProductNameAZ:
                    data = data.OrderBy(x => x.Name).ToList();
                    break;
                case SystemConstants.SelectionSortOrder.ProductNameZA:
                    data = data.OrderByDescending(x => x.Name).ToList();
                    break;
                case SystemConstants.SelectionSortOrder.ProductStocke:
                    data = data.OrderBy(x => x.Stock).ToList();
                    break;
                default:
                    break;
            }

            //each product: category string (ao, quan) , images product
            foreach (var product in data)
            {
                var categories = await (from pic in _context.ProductInCategories
                                        join ct in _context.CategoryTranslations on pic.CategoryId equals ct.CategoryId
                                        where pic.ProductId == product.Id && ct.LanguageId == request.LanguageId
                                        select ct.Name).ToListAsync();

                var cateString = string.Join(",", categories);
                product.CategoriesString = cateString;

                //get images
                var images = await _context.ProductImages.Where(x => x.ProductId == product.Id)
               .Select(x => new ProductImageViewModel()
               {
                   Id = x.Id,
                   ProductId = x.ProductId,
                   Caption = x.Caption,
                   DateCreated = x.DateCreated,
                   FileSize = x.FileSize,
                   FileUrl = _storageService.GetFileUrl(x.ImagePath),
                   IsDefault = x.IsDefault,
                   SortOrder = x.SortOrder,
               }).ToListAsync();

                if (images.Count > 0)
                {
                    product.Images = images;

                    var thumnail = images.FirstOrDefault(x => x.IsDefault);
                    if (thumnail == null)
                    {
                        product.ThumbnailImage = string.Empty;
                    }
                    else
                    {
                        product.ThumbnailImage = thumnail.FileUrl;
                    }
                }
            }

            //4. Select and projection
            var pagedResult = new PageResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };

            return pagedResult;
        }

        public async Task<ApiResult<ProductViewModel>> GetById(int productId, string languageId)
        {
            var product = await _context.Products.FindAsync(productId);
            var productTranslation = await _context.ProductTranslations
                .FirstOrDefaultAsync(p => p.ProductId == productId && p.LanguageId == languageId);

            var images = await _context.ProductImages.Where(x => x.ProductId == productId)
                .Select(x => new ProductImageViewModel()
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    Caption = x.Caption,
                    DateCreated = x.DateCreated,
                    FileSize = x.FileSize,
                    FileUrl = _storageService.GetFileUrl(x.ImagePath),
                    IsDefault = x.IsDefault,
                    SortOrder = x.SortOrder,
                }).ToListAsync();

            var result = new ProductViewModel()
            {
                Id = product.Id,
                LanguageId = productTranslation != null ? productTranslation.LanguageId : null,
                DateCreated = product.DateCreated,
                Description = productTranslation != null ? productTranslation.Description : null,
                Details = productTranslation != null ? productTranslation.Details : null,
                Name = productTranslation != null ? productTranslation.Name : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = productTranslation != null ? productTranslation.SeoAlias : null,
                SeoDescription = productTranslation != null ? productTranslation.SeoDescription : null,
                SeoTitle = productTranslation != null ? productTranslation.SeoTitle : null,
                Stock = product.Stock,
                ViewCount = product.ViewCount,
            };

            if (images.Count > 0)
            {
                result.Images = images;

                var thumnail = images.FirstOrDefault(x => x.IsDefault);
                if (thumnail == null)
                {
                    result.ThumbnailImage = string.Empty;
                }
                else
                {
                    result.ThumbnailImage = thumnail.FileUrl;
                }
            }

            var currentCategories = await _categoryService.GetByProductId(productId, languageId);
            var allCategory = await _categoryService.GetAll(languageId);
            foreach (var cate in allCategory)
            {
                var selectedItem = new SelectedItem()
                {
                    Id = cate.Id.ToString(),
                    Name = cate.Name,
                    Selected = currentCategories.Any(x => x.Id == cate.Id)
                };

                result.AllCategory.Add(selectedItem);
            };

            return new ApiSuccessResult<ProductViewModel>(result);
        }

        public async Task<List<ProductImageViewModel>> GetListImages(int productId)
        {
            return await _context.ProductImages.Where(i => i.ProductId == productId)
                .Select(i => new ProductImageViewModel()
                {
                    ProductId = productId,
                    Id = i.Id,
                    Caption = i.Caption,
                    DateCreated = i.DateCreated,
                    FileSize = i.FileSize,
                    FileUrl = i.ImagePath,
                    IsDefault = i.IsDefault,
                    SortOrder = i.SortOrder
                }).ToListAsync();
        }

        public async Task<ProductImageViewModel> GetProductImageById(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new EshopException($"Cannot find an image with id {imageId}");


            var imageView = new ProductImageViewModel()
            {
                ProductId = productImage.ProductId,
                Id = productImage.Id,
                Caption = productImage.Caption,
                DateCreated = productImage.DateCreated,
                FileSize = productImage.FileSize,
                FileUrl = productImage.ImagePath,
                IsDefault = productImage.IsDefault,
                SortOrder = productImage.SortOrder
            };

            return imageView;
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new EshopException($"Cannot find an image with id {imageId}");

            _context.ProductImages.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranslation = await _context.ProductTranslations.
                FirstOrDefaultAsync(x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);
            if (product == null || productTranslation == null) throw new EshopException($"Cannot find a product with id: {request.Id}");

            productTranslation.Name = request.Name;
            productTranslation.SeoAlias = request.SeoAlias;
            productTranslation.SeoDescription = request.SeoDescription;
            productTranslation.SeoTitle = request.SeoTitle;
            productTranslation.Description = request.Description;
            productTranslation.Details = request.Details;

            //update image
            if (request.ThumbNailImage != null)
            {
                var thumbnailImage = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);

                if (thumbnailImage != null)
                {
                    thumbnailImage.FileSize = request.ThumbNailImage.Length;
                    thumbnailImage.ImagePath = await SaveFile(request.ThumbNailImage);
                    _context.ProductImages.Update(thumbnailImage);
                }
            }

            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new EshopException($"Cannot find an image with id {imageId}");

            //save image
            if (request.ImageFile != null)
            {
                productImage.ImagePath = await SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }

            _context.ProductImages.Add(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null) throw new EshopException($"Cannot find a product with id: {productId}");

            product.Price = newPrice;

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(productId);

            if (product == null) throw new EshopException($"Cannot find a product with id: {productId}");

            product.Stock += addedQuantity;

            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            //var fileName = $"{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<PageResult<ProductViewModel>> GetAllByCategoryId(string languageId, GetPublicProductPagingRequest request)
        {
            //1. join table
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == languageId
                        select new { p, pt, pic };

            //2.filter
            if (request.CategoryId.HasValue && request.CategoryId > 0)
            {
                query = query.Where(p => p.pic.CategoryId == request.CategoryId);
            }

            //3.paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            //4.select and projection
            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecord = totalRow,
                Items = data,
                PageSize = request.PageSize,
                PageIndex = request.PageIndex,
            };

            return pageResult;
        }

        public async Task<List<ProductViewModel>> GetFeaturedProduct(string languageId, int takeNum)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join l in _context.Languages on pt.LanguageId equals l.Id
                        where pt.LanguageId == languageId && p.IsFeatured == true
                        select new { p, l, pt };


            var data = await query.OrderByDescending(x => x.p.DateCreated)
                .Take(takeNum)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    Language = x.l.Name,
                }).ToListAsync();



            foreach (var product in data)
            {
                var images = await _context.ProductImages.Where(x => x.ProductId == product.Id)
               .Select(x => new ProductImageViewModel()
               {
                   Id = x.Id,
                   ProductId = x.ProductId,
                   Caption = x.Caption,
                   DateCreated = x.DateCreated,
                   FileSize = x.FileSize,
                   FileUrl = _storageService.GetFileUrl(x.ImagePath),
                   IsDefault = x.IsDefault,
                   SortOrder = x.SortOrder,
               }).ToListAsync();

                if (images.Count > 0)
                {
                    product.Images = images;

                    var thumnail = images.FirstOrDefault(x => x.IsDefault);
                    if (thumnail == null)
                    {
                        product.ThumbnailImage = string.Empty;
                    }
                    else
                    {
                        product.ThumbnailImage = thumnail.FileUrl;
                    }
                }
            }

            return data;
        }

        public async Task<List<ProductViewModel>> GetLatestProduct(string languageId, int takeNum)
        {
            //1. Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join l in _context.Languages on pt.LanguageId equals l.Id
                        where pt.LanguageId == languageId
                        select new { p, pt, l };

            var data = await query.OrderByDescending(x => x.p.DateCreated)
                .Take(takeNum)
                .Select(x => new ProductViewModel()
                {
                    Id = x.p.Id,
                    Name = x.pt.Name,
                    DateCreated = x.p.DateCreated,
                    Description = x.pt.Description,
                    Details = x.pt.Details,
                    LanguageId = x.pt.LanguageId,
                    OriginalPrice = x.p.OriginalPrice,
                    Price = x.p.Price,
                    SeoAlias = x.pt.SeoAlias,
                    SeoDescription = x.pt.SeoDescription,
                    SeoTitle = x.pt.SeoTitle,
                    Stock = x.p.Stock,
                    ViewCount = x.p.ViewCount,
                    Language = x.l.Name,
                }).ToListAsync();

            foreach (var product in data)
            {
                var images = await _context.ProductImages.Where(x => x.ProductId == product.Id)
               .Select(x => new ProductImageViewModel()
               {
                   Id = x.Id,
                   ProductId = x.ProductId,
                   Caption = x.Caption,
                   DateCreated = x.DateCreated,
                   FileSize = x.FileSize,
                   FileUrl = _storageService.GetFileUrl(x.ImagePath),
                   IsDefault = x.IsDefault,
                   SortOrder = x.SortOrder,
               }).ToListAsync();

                if (images.Count > 0)
                {
                    product.Images = images;

                    var thumnail = images.FirstOrDefault(x => x.IsDefault);
                    if (thumnail == null)
                    {
                        product.ThumbnailImage = string.Empty;
                    }
                    else
                    {
                        product.ThumbnailImage = thumnail.FileUrl;
                    }
                }
            }

            return data;
        }
    }
}
