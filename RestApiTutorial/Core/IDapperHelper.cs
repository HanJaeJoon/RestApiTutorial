using Microsoft.Data.Sqlite;

namespace RestApiTutorial.Core;

public interface IDapperHelper
{
    Task<T> ExecuteQuery<T>(Func<SqliteConnection, Task<T>> operation);
    Task ExecuteQuery<T>(Func<SqliteConnection, Task> operation);
    Task<T> FirstOrDefaultAsync<T>(string sql, object? parameters = null);
    Task<IEnumerable<T>> GetAll<T>(string sql, object? parameters = null);
    Task<int> Insert<T>(string sql, object? parameters = null);
}