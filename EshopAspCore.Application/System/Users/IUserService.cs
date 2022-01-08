using EshopAspCore.ViewModels.Common;
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
        Task<string> AuthenticateAsync(LoginRequest request);
        Task<bool> Register(RegisterRequest request);
        Task<PageResult<UserViewModel>> GetUserPaging(GetUserPagingRequest request);
    }
}
