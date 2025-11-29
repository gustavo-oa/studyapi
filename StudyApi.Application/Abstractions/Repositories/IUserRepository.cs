using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace StudyApi.Application.Abstractions.Repositories
{
    public interface IUserRepository
    {
        Task<Guid> CreateAsync(User user, CancellationToken ct);
        Task<User?> GetByEmailAsync(string email, CancellationToken ct);
       Task<User?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<User?> GetByNameAsync(string name, CancellationToken ct);
        Task<bool> DeleteAsync(User user, CancellationToken ct);
    }
}



