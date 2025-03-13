using AutoMapper;
using CleanArchitectureExample.Application.DTOs;
using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Entities.Identity;
using CleanArchitectureExample.Infrastructure.Persistence.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CleanArchitectureExample.Application.Features.Queries.DepartmentQuery;

namespace CleanArchitectureExample.Application.Features.Handlers
{
    public class DepartmentQueriesHandler : 
        IRequestHandler<GetListDepartment, List<DepartmentDTO>>,
        IRequestHandler<GetDepartmentById, DepartmentDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentQueriesHandler(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<List<DepartmentDTO>> Handle(GetListDepartment request, CancellationToken cancellationToken)
        {
            List<Department> listDepartment = await _unitOfWork.DepartmentRepositories.GetListDepartment();
            List<DepartmentDTO> userProfile = _mapper.Map<List<DepartmentDTO>>(listDepartment); // Chuyển đổi User sang UserDTO
            return userProfile;
        }

        public async Task<DepartmentDTO> Handle(GetDepartmentById request, CancellationToken cancellationToken)
        {
            
            Department department = await _unitOfWork.DepartmentRepositories.GetDepartmentById(request.Id);
            DepartmentDTO departmentDTO = _mapper.Map<DepartmentDTO>(department); // Chuyển đổi User sang UserDTO
            return departmentDTO;
        }
    }
}
