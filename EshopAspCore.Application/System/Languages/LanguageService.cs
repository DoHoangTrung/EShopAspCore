using EshopAspCore.Data.EF;
using EshopAspCore.ViewModels.System.Languages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Application.System.Languages
{
    public class LanguageService : ILanguageService
    {
        private readonly EshopDbContext _context;

        public LanguageService(EshopDbContext context)
        {
            _context = context;
        }

        public async Task<List<LanguageViewModel>> GetAll()
        {
            var languages = await _context.Languages.Select(x=>new LanguageViewModel()
            {
                Id = x.Id,
                Name = x.Name
            }).ToListAsync();

            return languages;
        }
    }
}
