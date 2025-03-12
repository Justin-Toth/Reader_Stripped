using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;


namespace UFID_Reader.Services;

public interface IDbService
{
    // Returns a list of objects from the database
    Task<List<T>> QueryAsync<T>(string sql, object? parameters = null);
    
    // Returns a single object from the database    
    Task<T?> QuerySingleAsync<T>(string sql, object? parameters = null);
    
    // Executes a query that does not return any data
    Task<int> ExecuteAsync(string sql, object? parameters = null);
}

public class DbService(string connectionString) : IDbService
{
    private IDbConnection CreateConnection()
    {
        var connection = new MySqlConnection(connectionString);

        return connection;
    }

    public async Task<List<T>> QueryAsync<T>(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        var rows = await connection.QueryAsync<T>(sql, parameters);
        return rows.ToList();
    }

    public async Task<T?> QuerySingleAsync<T>(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        var row = await connection.QuerySingleOrDefaultAsync<T>(sql, parameters);
        return row;
    }

    public async Task<int> ExecuteAsync(string sql, object? parameters = null)
    {
        using var connection = CreateConnection();
        return await connection.ExecuteAsync(sql, parameters);
    }
}