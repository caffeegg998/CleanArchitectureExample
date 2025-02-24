

namespace CleanArchitectureExample.Application.DTOs.Account
{
    public class UserProfileDTO
    {
        public Guid UserId { get; set; }
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Department { get; set; }
        public string Factory { get; set; }

        public string MagnetCode { get; set; }
        public string CVNCode { get; set; }
    }
}
