using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Identity;

namespace Expire_Api.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(120)]
        public string? FullName { get; set; }

        public string? Address { get; set; }

        public byte[]? Image { get; set; }

        public Gender? Gender { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
