using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace SmartAdmin.ViewModels
{
    [DebuggerDisplay("{Username}")]
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "Имя пользователя")]
        public string Username { get; set; }
        
        [Required]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
    }
}