using EshopAspCore.Data.EF;
using EshopAspCore.Data.Entity;
using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.System.Roles;
using EshopAspCore.ViewModels.Utilities.Slides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Application.Utilities.Slides
{
    public class SlideService : ISlideService
    {
        private readonly EshopDbContext _context;
        public SlideService(EshopDbContext context)
        {
            _context = context;
        }

        public async Task<List<SlideViewModel>> GetAll()
        {
            var slides = await _context.Slides.OrderBy(x=>x.SortOrder).Select(x => new SlideViewModel()
            {
                Id = x.Id,
                Description = x.Description,
                Image = x.Image,
                Name = x.Name,
                Url = x.Url,
                Status = x.Status
            }).ToListAsync();

            return slides;
        }
    }
}
