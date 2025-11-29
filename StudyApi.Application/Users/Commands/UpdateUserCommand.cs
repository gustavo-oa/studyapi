using System;
using MediatR;
using StudyApi.Application.Users.Models;

namespace StudyApi.Application.Users.Commands
{
    public record UpdateUserCommand(
        Guid Id,
        string name,
        string Email,
        bool IsEnabled
    ) : IRequest<UserDto>;
}
