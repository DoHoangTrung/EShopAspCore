using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.System.Roles;
using EshopAspCore.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Application.System.Users
{
    public interface IUserService
    {
        Task<ApiResult<string>> AuthenticateAsync(LoginRequest request);

        Task<ApiResult<bool>> Register(RegisterRequest request);

        Task<ApiResult<PageResult<UserViewModel>>> GetUserPaging( GetUserPagingRequest request);

        Task<ApiResult<bool>> Update(Guid id,UserUpdateRequest request);

        Task<ApiResult<UserViewModel>> GetById(Guid id);

        Task<ApiResult<bool>> Delete(Guid id);

        Task<ApiResult<bool>> RoleAssign (Guid id, RoleAssignRequest request);
    }
}
