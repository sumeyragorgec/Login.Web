using System.ComponentModel.DataAnnotations;

namespace Login.Web.Models.ViewModels
{
    public class SignInViewModel
    {
        public SignInViewModel()
        {

        }
        public SignInViewModel(string email , string password)
        {
            Email = email;
            Password = password;
        }

        [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [Display(Name = "Şifre :")]
        public string Password { get; set; }

        [Display(Name = "Beni Hatırla :")]
        public bool RememberMe { get; set; }
    }
}
