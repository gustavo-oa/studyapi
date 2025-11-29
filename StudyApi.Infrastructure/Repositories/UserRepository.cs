using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Domain.Entities;
using StudyApi.Infrastructure.Data;

namespace StudyApi.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _factory;
        private const string Table = "users";

        public UserRepository(IDbConnectionFactory factory)
        {
            _factory = factory;
        }

        // Cria um novo usuário
        public async Task<Guid> CreateAsync(User user, CancellationToken ct)
        {
            var sql = $@"
                INSERT INTO {Table} (id, nome, email, senha_hash, is_active, create_date)
                VALUES (@Id, @Nome, @Email, @SenhaHash, @IsActive, @CreateDate)
                RETURNING id;
            ";

            using var conn = _factory.CreateConnection();
            return await conn.ExecuteScalarAsync<Guid>(sql, user);
        }

        // Busca usuário por email
        public async Task<User?> GetByEmailAsync(string email, CancellationToken ct)
        {
            var sql = $"SELECT * FROM {Table} WHERE email = @Email LIMIT 1;";
            using var conn = _factory.CreateConnection();
            return await conn.QuerySingleOrDefaultAsync<User>(sql, new { Email = email });
        }

        // Busca usuário por ID
        public async Task<User?> GetByIdAsync(Guid id, CancellationToken ct)
        {
            var sql = $"SELECT * FROM {Table} WHERE id = @Id LIMIT 1;";
            using var conn = _factory.CreateConnection();
            return await conn.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
        }

        // Busca usuário por nome
        public async Task<User?> GetByNameAsync(string name, CancellationToken ct)
        {
            var sql = $"SELECT * FROM {Table} WHERE nome = @Name LIMIT 1;";
            using var conn = _factory.CreateConnection();
            return await conn.QuerySingleOrDefaultAsync<User>(sql, new { Name = name });
        }

        // Deleta usuário
        public async Task<bool> DeleteAsync(User user, CancellationToken ct)
        {
            var sql = $"DELETE FROM {Table} WHERE id = @Id;";
            using var conn = _factory.CreateConnection();
            var rows = await conn.ExecuteAsync(sql, new { user.Id });
            return rows > 0;
        }

        // Atualiza usuário
        public async Task<User?> UpdateAsync(User user, CancellationToken ct)
        {
            var sql = $@"
                UPDATE {Table} 
                SET nome = @Nome, email = @Email, senha_hash = @SenhaHash, is_active = @IsActive
                WHERE id = @Id;
            ";

            using var conn = _factory.CreateConnection();
            await conn.ExecuteAsync(sql, user);
            return user;
        }
    }
}
