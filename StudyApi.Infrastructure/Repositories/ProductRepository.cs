using System.Data;
using Dapper;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Domain.Entities;
using StudyApi.Infrastructure.Data;

namespace StudyApi.Infrastructure.Repositories;

public class ProductRepository(IDbConnectionFactory factory) : IProductRepository
{
    private const string Table = "products";

    public async Task<Guid> CreateAsync(Product product, CancellationToken ct)
    {
        var sql = $"INSERT INTO {Table} (id, nome, price, createdate, updatedate, isenabled) " +
                  "VALUES (@Id, @Nome, @Price, @CreateDate, @UpdateDate, @IsEnabled) RETURNING id;";

        using var conn = factory.CreateConnection();
        var id = await conn.ExecuteScalarAsync<Guid>(new CommandDefinition(sql, product, cancellationToken: ct));
        return id;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
    {
        var sql = $"DELETE FROM {Table} WHERE id = @Id";
        using var conn = factory.CreateConnection();
        var rows = await conn.ExecuteAsync(new CommandDefinition(sql, new { Id = id }, cancellationToken: ct));
        return rows > 0;
    }

    public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken ct)
    {
        var sql = $"SELECT id, nome, price, createdate, updatedate, isenabled FROM {Table} ORDER BY createdate DESC";
        using var conn = factory.CreateConnection();
        var result = await conn.QueryAsync<Product>(new CommandDefinition(sql, cancellationToken: ct));
        return result;
    }
    public async Task<IEnumerable<Product>> GetAllEnableAsync(CancellationToken ct)
    {
        var sql = @$"SELECT id, nome, price, createdate, updatedate, isenabled
                     FROM {Table}
                     WHERE isenabled = true
                     ORDER BY createdate DESC";
        using var conn = factory.CreateConnection();
        var result = await conn.QueryAsync<Product>(new CommandDefinition(sql, cancellationToken: ct));
        return result;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var sql = $"SELECT id, nome, price, createdate, updatedate, isenabled FROM {Table} WHERE id = @Id";
        using var conn = factory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<Product>(new CommandDefinition(sql, new { Id = id }, cancellationToken: ct));
    }

      public async Task<Product?> GetByNameAsync(string name, CancellationToken ct)
    {
        var sql = $"SELECT id, nome, price, createdate, updatedate, isenabled FROM {Table} WHERE nome = @Name limit 1";
        using var conn = factory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<Product>(new CommandDefinition(sql, new { Name = name }, cancellationToken: ct));
    }


    public async Task<bool> UpdateAsync(Product product, CancellationToken ct)
    {
        var sql = $"UPDATE {Table} SET nome = @Nome, price = @Price, isenabled = @IsEnabled, updatedate = @UpdateDate WHERE id = @Id;";

        using var conn = factory.CreateConnection();
        var rows = await conn.ExecuteAsync(new CommandDefinition(sql, product, cancellationToken: ct));
        return rows > 0;
    }
}
