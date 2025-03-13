using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Entities.Identity;
using CleanArchitectureExample.Domain.Interfaces.Repositorys;
using CleanArchitectureExample.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Infrastructure.Persistence.Repositories
{
    public class DepartmentRepositories : IDepartmentRepositories
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepositories(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Department?> GetDepartmentById(int Id)
        {
            return await _context.Departments.Include(d => d.UserProfiles).FirstOrDefaultAsync(c => c.DepartmentId == Id);
        }

        public async Task<List<Department>> GetListDepartment()
        {
            return await _context.Departments
                .Include(d => d.UserProfiles)
                .ToListAsync();
        }
    }
}
