using MediatR;
using StudyApi.Application.Users.Models;

namespace StudyApi.Application.Users.Commands;

public record CreateUserCommand(string Nome, string Email, string Password) : IRequest<UserDto>;