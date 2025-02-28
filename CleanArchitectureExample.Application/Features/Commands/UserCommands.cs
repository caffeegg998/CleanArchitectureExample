using CleanArchitectureExample.Application.DTOs.Account;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureExample.Application.Features.Commands
{
    public class UserCommands
    {
        public class CreateUserCommand : IRequest<Guid>
        {
            public UserProfileDTO userProfileDTO { get; set; }
        }

        public class UpdateUserCommand : IRequest<UserProfileDTO>
        {
            public UserProfileDTO userProfileDTO { get; set; }
        }
        public class DeleteUserCommand : IRequest<bool>
        {
            public DeleteUserCommand(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; set; }
        }
        public class GetUserByIdCommand : IRequest<UserProfileDTO>
        {
          
            public string Id { get; set; }
        }
    }
}
