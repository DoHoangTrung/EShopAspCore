using EshopAspCore.Data.Entity;
using EshopAspCore.ViewModels.Common;
using EshopAspCore.ViewModels.System.Roles;
using EshopAspCore.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly IConfiguration _config;

        public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager
            , IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _config = config;
        }

        public async Task<ApiResult<string>> AuthenticateAsync(LoginRequest request)
        {
            //search user name
            var user = await _userManager.FindByNameAsync(request.UserName);
            if (user == null)
                return new ApiErrorResult<string>("UserName or password is incorrect.");

            //login by user name and password
            var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, true);
            if (!result.Succeeded)
                return new ApiErrorResult<string>("Login failed.");

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Role,string.Join(";",roles)),
                new Claim(ClaimTypes.Name,user.UserName),
            };

            //symmetric security 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Tokens:Issuer"],
                _config["Tokens:Issuer"],
                claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credential);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return new ApiSuccessResult<string>(tokenString);
        }

        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return new ApiErrorResult<UserViewModel>("User doesn't exist.");

            var roles = await _userManager.GetRolesAsync(user);

            var userViewModel = new UserViewModel()
            {
                Id = user.Id,
                Dob = user.Dob,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                PhoneNumber = user.PhoneNumber,
                Roles = roles,
            };

            return new ApiSuccessResult<UserViewModel>(userViewModel);
        }

        public async Task<ApiResult<PageResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request)
        {
            var query = _userManager.Users;

            //filter
            if (!string.IsNullOrEmpty(request.Keywords))
            {
                query = query.Where(u => u.UserName.Contains(request.Keywords)
                    || u.PhoneNumber.Contains(request.Keywords));
            }

            //2.paging
            int totalRow = await query.CountAsync();

            var data = await query.Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new UserViewModel()
                {
                    Id = x.Id,
                    Dob = x.Dob,
                    Email = x.Email,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserName = x.UserName,
                    PhoneNumber = x.PhoneNumber ,
                }).ToListAsync();

            //4.select and projection
            var pageResult = new PageResult<UserViewModel>()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalRecord = totalRow,
                Items = data
            };

            return new ApiSuccessResult<PageResult<UserViewModel>>(pageResult);
        }

        public async Task<ApiResult<bool>> Register(RegisterRequest request)
        {
            //check dupplicate user name and email
            var userByName = await _userManager.FindByNameAsync(request.UserName);
            var userByEmail = await _userManager.FindByEmailAsync(request.Email);

            if (userByName != null)
                return new ApiErrorResult<bool>("Username already exists.");

            if (userByEmail!= null)
                return new ApiErrorResult<bool>("Email already exists.");

            var user = new AppUser()
            {
                Dob = request.Dob,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
                PhoneNumber = request.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
                return new ApiSuccessResult<bool>(true);

            return new ApiErrorResult<bool>();
        }

        public async Task<ApiResult<bool>> Update(Guid id,UserUpdateRequest request)
        {
            //check email exist?
            var userByEmail = await _userManager.FindByEmailAsync(request.Email);

            if (await _userManager.Users.AnyAsync(x=>x.Email == request.Email && x.Id != id))
                return new ApiErrorResult<bool>("Email already exists.");

            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return new ApiErrorResult<bool>("Cant find user with id " + id.ToString());

            user.Dob = request.Dob;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
                return new ApiSuccessResult<bool>(true);

            return new ApiErrorResult<bool>("Update failed.");
        }


        public async Task<ApiResult<bool>> Delete(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return new ApiErrorResult<bool>("User doesn't exist.");


            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
                return new ApiSuccessResult<bool>(true);

            return new ApiErrorResult<bool>("Delete failed.");
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
                return new ApiErrorResult<bool>("User doesn't exist.");

            var removedRoles = request.Roles.Where(x => x.Selected == false).Select(x => x);
            var addedRoles= request.Roles.Where(x => x.Selected == true).Select(x => x);

            foreach (var role in removedRoles)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
            }

            foreach (var role in addedRoles)
            {
                if(await _userManager.IsInRoleAsync(user, role.Name) == false)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);
                }
            }

            return new ApiSuccessResult<bool>(true);
        }
    }
}
