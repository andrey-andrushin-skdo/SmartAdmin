using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace SmartAdmin.ViewModels
{
    [DebuggerDisplay("{Id}:{IsActive}")]
    public class ActivateUserViewModel
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public bool IsActive { get; set; }
    }
}