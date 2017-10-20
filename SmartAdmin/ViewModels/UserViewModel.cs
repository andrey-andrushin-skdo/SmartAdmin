using System.ComponentModel.DataAnnotations;

namespace SmartAdmin.ViewModels
{
    public class UserViewModel
    {
        [Required]
        public int Id { get; set; }
        
        public string Name { get; set; }

        public bool IsActive { get; set; }
    }
}