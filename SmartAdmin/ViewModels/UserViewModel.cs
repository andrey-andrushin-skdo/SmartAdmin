using System.ComponentModel.DataAnnotations;

namespace SmartAdmin.ViewModels
{
    public class UserViewModel
    {
        [Required]
        [Display(Name = "№")]
        public int Id { get; set; }

        [Display(Name = "Пользователь")]
        public string Name { get; set; }

        [Display(Name = "Активен")]
        public bool IsActive { get; set; }
    }
}