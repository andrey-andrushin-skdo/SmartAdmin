using System.ComponentModel.DataAnnotations;

namespace SmartAdmin.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        public bool IsActive { get; set; }
    }

    public class UserRole
    {
    }
}