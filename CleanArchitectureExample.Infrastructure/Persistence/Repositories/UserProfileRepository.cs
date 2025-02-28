using CleanArchitectureExample.Domain.Entities;
using CleanArchitectureExample.Domain.Entities.Identity;
using CleanArchitectureExample.Domain.Interfaces;
using CleanArchitectureExample.Infrastructure.Persistence.DbContexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Infrastructure.Persistence.Repositories
{
    public class UserProfileRepository : IUserProfileRepository
    {
        //private readonly IdentityDbContext _identityDbContext;
        private readonly ApplicationDbContext _identityDbContext;
        public UserProfileRepository(ApplicationDbContext identityDbContext)
        {
            _identityDbContext = identityDbContext;
        }
        public IEnumerable<UserProfile> GetAllUsers()
        {
            return _identityDbContext.UserProfiles.ToList();
        }

        public async Task<UserProfile?> GetUser(string userId) => await _identityDbContext.UserProfiles.FindAsync(userId);

        public async Task SaveUser(UserProfile userProfile) => await _identityDbContext.UserProfiles.AddAsync(userProfile);

        public async Task UpdateUser(UserProfile userProfile)
        {
            var existingUser = await _identityDbContext.UserProfiles.FindAsync(userProfile.UserId);
            if (existingUser == null)
            {
                throw new Exception("User not found");
            }

            // Cập nhật các giá trị từ product vào existingProduct
            existingUser.DateOfBirth = userProfile.DateOfBirth;
           
            existingUser.Department = userProfile.Department;
            existingUser.FullName = userProfile.FullName;
            existingUser.MaNhanVien = userProfile.MaNhanVien;
      

            // Đánh dấu entity là đã thay đổi
            _identityDbContext.UserProfiles.Update(existingUser);

            // Đợi quá trình lưu vào DB
            //await _context.SaveChangesAsync();
        }
    }
}
