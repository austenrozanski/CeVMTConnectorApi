using System.Data;
using CeVMTConnectorApi.Dal.Contexts;
using Dapper;

namespace CeVMTConnectorApi.Dal.Repositories;

public class BaseRepository
{
    private readonly BaseDataContext _dataContext;

    public BaseRepository(BaseDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    // Query the database for multiple instances of an object
    // Returns a list with the results
    public async Task<List<dynamic>?> QueryAsync(string query)
    {
        using (var connection = _dataContext.CreateConnection())
        {
            var result = await connection.QueryAsync(query);
            return result?.ToList();
        }
    }
    
    // Query the database for multiple instances of an object using a generic type
    // Returns a list with the results
    public async Task<List<T>?> QueryAsync<T>(string query)
    {
        using (var connection = _dataContext.CreateConnection())
        {
            var result = await connection.QueryAsync<T>(query);
            return result?.ToList();
        }
    }
    
    // Execute a query on the database
    // Returns a list with the results
    public async Task<int> ExecuteAsync(string query)
    {
        using (var connection = _dataContext.CreateConnection())
        {
            var result = await connection.ExecuteAsync(query);
            return result;
        }
    }

    public int ExecuteStoredProcedure(string storedProcName, DynamicParameters? parameters = null)
    {
        using (var connection = _dataContext.CreateConnection())
        {
            var result = connection.Execute("spMagicProc", parameters, commandType: CommandType.StoredProcedure); 
            return result;
        }
    }
}