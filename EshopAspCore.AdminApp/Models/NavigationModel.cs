using EshopAspCore.ViewModels.System.Languages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Models
{
    public class NavigationModel
    {
        public string CurrentLanguageId { get; set; }
        public List<LanguageViewModel> Languages { get; set; }
    }
}
