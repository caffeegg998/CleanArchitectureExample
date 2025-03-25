using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.DTOs.Account
{
    public class UserProfileDTO_Min
    {
        public string? UserId { get; set; }
        public string FullName { get; set; }
       
    }
}
