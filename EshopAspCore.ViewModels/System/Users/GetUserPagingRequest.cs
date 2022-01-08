using EshopAspCore.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.ViewModels.System.Users
{
    public class GetUserPagingRequest  : PagingRequestBase
    {
        public string Keywords { get; set; }
    }
}
