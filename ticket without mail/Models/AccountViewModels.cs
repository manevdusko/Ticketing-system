using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ticket_without_mail.Models
{
    public class ExternalLoginConfirmationViewModel
    {
        [Required]
        [Display(Name = "e-mail")]
        public string Email { get; set; }
    }

    public class ExternalLoginListViewModel
    {
        public string ReturnUrl { get; set; }
    }

    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; }
        public ICollection<System.Web.Mvc.SelectListItem> Providers { get; set; }
        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; }

        [Required]
        [Display(Name = "Code")]
        public string Code { get; set; }
        public string ReturnUrl { get; set; }

        [Display(Name = "Запомни го пребарувачот?")]
        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }

    public class ForgotViewModel
    {
        [Required]
        [Display(Name = "e-mail")]
        public string Email { get; set; }
    }

    public class LoginViewModel
    {
        [Required]
        [Display(Name = "e-mail")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "лозинка")]
        public string Password { get; set; }

        [Display(Name = "Остани најавен?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "e-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} мора да биде долга најмалку {2} карактери.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потврди лозинка")]
        [Compare("Password", ErrorMessage = "Лозинките не се софпаѓаат.")]
        public string ConfirmPassword { get; set; }
    }

    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "e-mail")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} мора да биде долга најмалку {2} карактери.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Лозинка")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Потврди лозинка")]
        [Compare("Password", ErrorMessage = "Лозинките не се софпаѓаат.")]
        public string ConfirmPassword { get; set; }

        public string Code { get; set; }
    }

    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "e-mail")]
        public string Email { get; set; }
    }
}
