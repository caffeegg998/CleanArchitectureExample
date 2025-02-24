using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CleanArchitectureExample.Domain.Entities.Identity
{
    public class UserProfile
    {
        [Key]
        public string UserId { get; set; }

        public string FullName {  get; set; }
        public DateOnly DateOfBirth { get; set; }

        public string Factory {  get; set; }
        public string Department { get; set; }

        public string? CVNCode { get; set; }

        public string? MagnetCode { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}
