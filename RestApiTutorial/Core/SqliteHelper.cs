using Dapper;
using Microsoft.Data.Sqlite;
using System.Data;

namespace RestApiTutorial.Core;

public class SqliteHelper
{
    private readonly IDbConnection _connection;

    public SqliteHelper(IConfiguration configuration)
    {
        _connection = new SqliteConnection(configuration.GetConnectionString("SqliteConnection"));
        _connection.Open();
    }

    private void Excute(string sql)
    {
        var command = _connection.CreateCommand();
        command.CommandText = sql;

        _connection.ExecuteAsync(sql);
    }

    private void Select<T>(string sql)
    {
        var command = _connection.CreateCommand();
        command.CommandText = sql;

        _connection.ExecuteScalar<T>(sql);
    }
}