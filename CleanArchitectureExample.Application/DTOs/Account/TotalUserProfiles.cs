
namespace CleanArchitectureExample.Application.DTOs.Account
{
    public class TotalUserProfiles
    {
        public int TotalUserTL { get; set; }
        public int TotalUserQV { get; set; }
        public int TotalUserTS { get; set; }
        public List<UserProfileDTO> userProfileDTOs { get; set; }
    }
}
