using MediatR;
using System;

namespace StudyApi.Application.Users.Commands
{
    // Comando que representa a ação de deletar um usuário
    public record DeleteUserCommand(Guid Id) : IRequest<bool>;
}
