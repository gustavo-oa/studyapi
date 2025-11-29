using System.Data;
using Dapper;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace StudyApi.Infrastructure.Data;

public class DatabaseInitializer(IDbConnectionFactory factory, ILogger<DatabaseInitializer> logger) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        const string sql = @"
            CREATE TABLE IF NOT EXISTS products (
                id uuid PRIMARY KEY,
                nome text NOT NULL,
                price numeric(18,2) NOT NULL,
                createdate timestamp with time zone NOT NULL DEFAULT current_timestamp,
                updatedate timestamp with time zone NOT NULL DEFAULT current_timestamp,
                isenabled boolean NOT NULL DEFAULT true
            );";

        try
        {
            using var conn = factory.CreateConnection();
            if (conn is Npgsql.NpgsqlConnection npg)
            {
                await npg.OpenAsync(cancellationToken);
            }
            await conn.ExecuteAsync(new CommandDefinition(sql, cancellationToken: cancellationToken));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error initializing database tables");
            throw;
        }
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
