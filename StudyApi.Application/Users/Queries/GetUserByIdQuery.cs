using MediatR;
using StudyApi.Application.Users.Models;

namespace StudyApi.Application.Users.Queries
{
    public record GetUserByIdQuery(Guid Id) : IRequest<UserDto>;
}