using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;
using System.Transactions;

namespace RestApiTutorial.Core;

public class SqliteHelper: IDapperHelper
{
    private readonly string _connectionString;

    public SqliteHelper(string connectionString) 
    {
        _connectionString = connectionString;
    }

    async Task<SqliteConnection> InitializeConnection()
    {
        var sqlconnection = new SqliteConnection(_connectionString);
        await sqlconnection.OpenAsync();

        return sqlconnection;
    }

    public async Task<T> ExecuteQuery<T>(Func<SqliteConnection, Task<T>> operation)
    {
        try
        {
            using var connection = await InitializeConnection();
            return await operation(connection);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task ExecuteQueryWithTransaction<T>(Func<SqliteConnection, Task> operation)
    {
        using var tscn = new TransactionScope();

        try
        {
            using var connection = await InitializeConnection();
            await operation(connection);
            tscn.Complete();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public async Task ExecuteQuery<T>(Func<SqliteConnection, Task> operation)
    {
        using var connection = await InitializeConnection();
        await operation(connection);
    }

    public async Task<IEnumerable<T>> GetAll<T>(string sql, object? parameters = null)
    {
        return await ExecuteQuery(async con => await con.QueryAsync<T>(sql, parameters));
    }

    public async Task<T> FirstOrDefaultAsync<T>(string sql, object? parameters = null)
    {
        return await ExecuteQuery(async con => await con.QueryFirstOrDefaultAsync<T>(sql, parameters));
    }

    public async Task<int> Insert<T>(string sql, object? parameters = null)
    {
        return await ExecuteQuery<int>(async con => await con.ExecuteAsync(sql, parameters));
    }
}