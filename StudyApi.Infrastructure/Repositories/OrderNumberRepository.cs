using System.Data;
using Dapper;
using StudyApi.Application.Abstractions.Repositories;
using StudyApi.Domain.Entities;
using StudyApi.Infrastructure.Data;

namespace StudyApi.Infrastructure.Repositories;

public class OrderNumberRepository(IDbConnectionFactory factory) : IOrderNumberRepository
{
    private const string Table = "ordernumber";

    public async Task<Guid> CreateAsync(OrderNumber ordernumber, CancellationToken ct)
    {
        var sql = $"INSERT INTO {Table} (id_ordernumber, nome, createdate, updatedate, isenabled) " +
                  "VALUES (@Id, @Nome,  @CreateDate, @UpdateDate, @IsEnabled) RETURNING id;";

        using var conn = factory.CreateConnection();
        var id = await conn.ExecuteScalarAsync<Guid>(new CommandDefinition(sql, ordernumber, cancellationToken: ct));
        return id;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct)
    {
        var sql = $"DELETE FROM {Table} WHERE id = @Id";
        using var conn = factory.CreateConnection();
        var rows = await conn.ExecuteAsync(new CommandDefinition(sql, new { Id = id }, cancellationToken: ct));
        return rows > 0;
    }

    public async Task<IEnumerable<OrderNumber>> GetAllAsync(CancellationToken ct)
    {
        var sql = $"SELECT id, nome, price, createdate, updatedate, isenabled FROM {Table} ORDER BY createdate DESC";
        using var conn = factory.CreateConnection();
        var result = await conn.QueryAsync<OrderNumber>(new CommandDefinition(sql, cancellationToken: ct));
        return result;
    }
    public async Task<IEnumerable<OrderNumber>> GetAllEnableAsync(CancellationToken ct)
    {
        var sql = @$"SELECT id, nome, price, createdate, updatedate, isenabled
                     FROM {Table}
                     WHERE isenabled = true
                     ORDER BY createdate DESC";
        using var conn = factory.CreateConnection();
        var result = await conn.QueryAsync<OrderNumber>(new CommandDefinition(sql, cancellationToken: ct));
        return result;
    }

    public async Task<OrderNumber?> GetByIdAsync(Guid id, CancellationToken ct)
    {
        var sql = $"SELECT id, nome, price, createdate, updatedate, isenabled FROM {Table} WHERE id = @Id";
        using var conn = factory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<OrderNumber>(new CommandDefinition(sql, new { Id = id }, cancellationToken: ct));
    }

      public async Task<OrderNumber?> GetByNameAsync(string name, CancellationToken ct)
    {
        var sql = $"SELECT id, nome, price, createdate, updatedate, isenabled FROM {Table} WHERE nome = @Name limit 1";
        using var conn = factory.CreateConnection();
        return await conn.QuerySingleOrDefaultAsync<OrderNumber>(new CommandDefinition(sql, new { Name = name }, cancellationToken: ct));
    }


    public async Task<bool> UpdateAsync(OrderNumber ordernumber, CancellationToken ct)
    {
        var sql = $"UPDATE {Table} SET nome = @Nome, price = @Price, isenabled = @IsEnabled, updatedate = @UpdateDate WHERE id = @Id;";

        using var conn = factory.CreateConnection();
        var rows = await conn.ExecuteAsync(new CommandDefinition(sql, ordernumber, cancellationToken: ct));
        return rows > 0;
    }
}
