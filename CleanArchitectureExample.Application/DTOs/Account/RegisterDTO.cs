using System.ComponentModel.DataAnnotations;

namespace CleanArchitectureExample.Application.DTOs.Account
{
    public class RegisterDTO
    {
        [Required]
        public string username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(100,ErrorMessage = "Mật khẩu phải có ít nhất {2} ký tự và tối đa {1} ký tự.",MinimumLength = 6)]
        public string Password { get; set; }

        [Required]
        //[StringLength(100, ErrorMessage = "Mật khẩu phải có ít nhất {2} ký tự và tối đa {1} ký tự.", MinimumLength = 6)]    
        public string FullName { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string Department { get; set; }

        public int detectUser {  get; set; }


    }
}
