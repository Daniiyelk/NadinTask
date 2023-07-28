using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.User
{
    public class UserRegisterDto
    {
        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserName { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"(^(09|9)[0-9]\d{8}$)", ErrorMessage = "موبایل وارد شده صحیح نمی باشد")]
        public string PhoneNumber { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string Repassword { get; set; }
    }
}
