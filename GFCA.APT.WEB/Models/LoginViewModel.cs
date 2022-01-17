using System.ComponentModel.DataAnnotations;

namespace GFCA.APT.WEB.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username required", AllowEmptyStrings = false)]
        [Display(Name = "Username")]
        public string Username { get; set; }

        //[Required(ErrorMessage = "Email required", AllowEmptyStrings = true)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password required", AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
}