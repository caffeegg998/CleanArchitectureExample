using CleanArchitectureExample.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Domain.Interfaces
{
    public interface IUserProfileRepository
    {
        Task SaveUser(UserProfile userProfile);
        Task UpdateUser(UserProfile userProfile);

        Task<UserProfile?> GetUser(string userId);

        IEnumerable<UserProfile> GetAllUsers();
    }
}
