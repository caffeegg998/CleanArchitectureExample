

using CleanArchitectureExample.Domain.Entities;

namespace CleanArchitectureExample.Application.DTOs.Account
{
    public class UserProfileDTO
    {
        public string? UserId { get; set; }
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int Department { get; set; }
        public string? MaNhanVien { get; set; }
        public string? MagnetCode { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
