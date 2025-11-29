using MediatR;
using StudyApi.Application.Abstractions.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace StudyApi.Application.Users.Commands
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {
        private readonly IUserRepository _repo;

        public DeleteUserHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            // Busca o usuário pelo Id
            var user = await _repo.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
                return false; // usuário não encontrado

            // Deleta o usuário
            return await _repo.DeleteAsync(user, cancellationToken);
        }
    }
}
