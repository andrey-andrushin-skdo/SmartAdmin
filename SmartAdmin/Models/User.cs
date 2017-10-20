using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartAdmin.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public int UserRoleId { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
        
        [Required]
        public string LastName { get; set; }

        public bool IsActive { get; set; }

        public DateTime? DateUpdate { get; set; }
    }

    [Table("UserRole")]
    public class UserRole
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Code { get; set; }
    }
}