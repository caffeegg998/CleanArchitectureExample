using CleanArchitectureExample.Application.DTOs.Account;
using CleanArchitectureExample.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.DTOs
{
    public class DepartmentDTO
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

       
        public ICollection<UserProfileDTO> UserProfilesDTO { get; set; }
    }
}
