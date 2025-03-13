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
            //CreateMap<UserProfile, UserProfileDTO>();
            //CreateMap<UserProfileDTO, UserProfile>();
            CreateMap<UserProfileDTO, UserProfile>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId.ToString()))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.DepartmentId))
                .ForMember(dest => dest.Department, opt => opt.Ignore());

            CreateMap<UserProfile, UserProfileDTO>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.Department.DepartmentName));
        }
    }
}
