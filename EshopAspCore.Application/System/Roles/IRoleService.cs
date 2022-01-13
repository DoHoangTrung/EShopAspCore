using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.System.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Application.System.Roles
{
    public interface IRoleService
    {
        Task<ApiResult<List<RoleViewModel>>> GetAll();
    }
}
