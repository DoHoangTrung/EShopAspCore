using EshopAspCore.ViewModels.System.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Application.System.Languages
{
    public interface ILanguageService
    {
        public Task<List<LanguageViewModel>> GetAll();
    }
}
