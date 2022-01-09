using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EshopAspCore.ViewModels.System.Users
{
    public class RegisterRequest
    {
        [DisplayName("Tên")]
        public string FirstName { get; set; }

        [DisplayName("Họ")]
        public string LastName { get; set; }
        
        [DisplayName("Ngày sinh")]
        [DataType(DataType.Date)]
        public DateTime Dob { get; set; }
        
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("Số điện thoại")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Tên đăng nhập")]
        public string UserName { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Mật khẩu")]
        //Default identity password role .net core(see it on gg)
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,}$", ErrorMessage = "Password at least 6 characters and contain one uppercase letter, one lowercase letter, one digit and one special character.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [DisplayName("Xác nhận mật khẩu")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
