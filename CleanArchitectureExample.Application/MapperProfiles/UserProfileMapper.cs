using AutoMapper;
using CleanArchitectureExample.Application.DTOs.Account;
using CleanArchitectureExample.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace CleanArchitectureExample.Application.MapperProfiles
{
    public class UserProfileMapper : Profile
    {
        public UserProfileMapper()
        {
            CreateMap<UserProfile, UserProfileDTO>();
            CreateMap<UserProfileDTO, UserProfile>();
        }
    }
}
