using CleanArchitectureExample.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Interfaces.Repositorys
{
    public interface IDepartmentRepositories
    {
        Task<List<Department>> GetListDepartment();
        Task<Department> GetDepartmentById(int Id);
    }
}
