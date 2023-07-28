using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "نام")]
        [MaxLength(50, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string? FirstName { get; set; }

        [Display(Name = "نام خانوادگی")]
        [MaxLength(70, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string? LastName { get; set; }

        [Display(Name = "نام شهر")]
        [MaxLength(70, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string? City { get; set; }

        #region Relations

        public ICollection<Product> Products { get; set; }

        #endregion
    }
}
