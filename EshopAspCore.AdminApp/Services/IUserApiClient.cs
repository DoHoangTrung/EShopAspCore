using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.System.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EshopAspCore.AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);

        Task<PageResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest request);
    }
}
