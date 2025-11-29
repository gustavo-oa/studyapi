using MediatR;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Application.Users.Models;
using StudyApi.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StudyApi.Application.Users.Commands
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, UserDto>
    {
        private readonly IUserRepository _repo;

        // ✅ Construtor recebe IUserRepository via injeção de dependência
        public CreateUserHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<UserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            // ✅ Verifica se já existe usuário com mesmo email
            var existing = await _repo.GetByEmailAsync(request.Email, cancellationToken);
            if (existing != null)
                throw new InvalidOperationException("Já existe um usuário com esse email.");

            // ✅ Cria usuário
            var entity = new User
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Email = request.Email,
                SenhaHash = HashPassword(request.Password), // gera hash da senha
                IsActive = true,
                CreateDate = DateTime.UtcNow,
                UpdateDate = DateTime.UtcNow
            };

            // ✅ Salva no banco
            var id = await _repo.CreateAsync(entity, cancellationToken);
            entity.Id = id;

            // ✅ Retorna DTO
            return new UserDto(entity.Id, entity.Nome, entity.Email, entity.CreateDate, entity.UpdateDate);
        }

        // 🔒 Método privado para gerar hash da senha
        private static string HashPassword(string password)
        {
            using var sha = System.Security.Cryptography.SHA256.Create();
            var bytes = System.Text.Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToHexString(hash);
        }
    }
}
