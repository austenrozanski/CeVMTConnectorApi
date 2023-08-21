using System.Data;
using Microsoft.Data.SqlClient;

namespace CeVMTConnectorApi.Dal.Contexts;

public class BaseDataContext
{
    private readonly string _connectionString;

    protected BaseDataContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
        => new SqlConnection(_connectionString);
}