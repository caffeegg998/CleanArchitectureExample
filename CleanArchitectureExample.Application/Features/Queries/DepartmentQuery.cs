using CleanArchitectureExample.Application.DTOs;
using CleanArchitectureExample.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Features.Queries
{
    public class DepartmentQuery
    {
        public class GetListDepartment : IRequest<List<DepartmentDTO>>
        {

        }
        public class GetDepartmentById : IRequest<DepartmentDTO>
        {
            public int Id { get; set; }
            public GetDepartmentById(int id )
            {
                Id = id;
            }
        }
    }
}
