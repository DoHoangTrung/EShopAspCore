using EshopAspCore.Data.Entity;
using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.System.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Application.System.Roles
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<ApiResult<List<RoleViewModel>>> GetAll()
        {
            var roles = await _roleManager.Roles.Select(x => new RoleViewModel()
            {
                Id = x.Id,
                Description = x.Description,
                Name = x.Name
            }).ToListAsync();

            if (roles.Count == 0)
            {
                return new ApiErrorResult<List<RoleViewModel>>("Get all role api result return null.");
            }

            return new ApiSuccessResult<List<RoleViewModel>>(roles);
        }
    }
}
