using MediatR;
using StudyApi.Application.Users.Models;

namespace StudyApi.Application.Users.Queries
{
    public record GetUserByNameQuery(string Name) : IRequest<UserDto>;
}