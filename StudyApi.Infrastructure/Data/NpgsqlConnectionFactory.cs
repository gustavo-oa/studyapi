using System.Data;
using Microsoft.Extensions.Options;
using Npgsql;

namespace StudyApi.Infrastructure.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

public class NpgsqlConnectionFactory(IOptions<DbOptions> options) : IDbConnectionFactory
{
    private readonly string _conn = options.Value.ConnectionString;

    public IDbConnection CreateConnection()
        => new NpgsqlConnection(_conn);
}

