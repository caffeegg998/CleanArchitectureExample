using AutoMapper;
using CleanArchitectureExample.Application.DTOs;
using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.MapperProfiles
{
    public class DepartmentMapperProfile :Profile
    {
        public  DepartmentMapperProfile()
        {
            // Mapping from Department to DepartmentDTO
            CreateMap<Department, DepartmentDTO>()
            .ForMember(dest => dest.UserProfilesDTO, opt => opt.MapFrom(src => src.UserProfiles)); // Mapping UserProfiles collection

            // Optional: Mapping from DepartmentDTO to Department (if needed)
            CreateMap<DepartmentDTO, Department>()
                .ForMember(dest => dest.UserProfiles, opt => opt.MapFrom(src => src.UserProfilesDTO)); // Mapping UserProfilesDTO collection
        }
        
    }
}
