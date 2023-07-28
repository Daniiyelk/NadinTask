using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        [Display(Name = "نام محصول")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        public string Name { get; set; }

        [Display(Name = "تاریخ تولید")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public DateTime ProduceDate { get; set; }

        [Display(Name = "شماره موبایل")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [RegularExpression(@"(^(09|9)[0-9]\d{8}$)", ErrorMessage = "موبایل وارد شده صحیح نمی باشد")]
        public string ManufactorPhone { get; set; }

        [Display(Name = " ایمیل سازنده")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(70, ErrorMessage = "{0} نمی تواند بیشتر از {1} کاراکتر باشد .")]
        [EmailAddress(ErrorMessage = "ایمیل وارد شده معتبر نمی باشد")]
        public string ManufactorEmail { get; set; }

        [Display(Name = "آیا در دسترس است ؟")]
        public bool IsAvailable { get; set; }

        [Display(Name = "آیدی کاربر")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public string UserId { get; set; }


        #region Relations

        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
        #endregion
    }
}
