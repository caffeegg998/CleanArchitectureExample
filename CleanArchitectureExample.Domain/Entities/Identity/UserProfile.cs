using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CleanArchitectureExample.Domain.Entities.Identity
{
    public class UserProfile
    {
        [Key]
        public string UserId { get; set; }
        public string FullName {  get; set; }
        public DateOnly DateOfBirth { get; set; }
        
        public int DepartmentId { get; set; }
        
        public Department Department { get; set; }
        public string? MaNhanVien { get; set; }
        public string? MagnetCode { get; set; }
        public DateTime CreateAt { get; set; }

        [ForeignKey(nameof(UserId))]
        public IdentityUser User { get; set; }
    }
}
