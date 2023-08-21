using CeVMTConnectorApi.Dal.Repositories;
using Dapper;

namespace CeVMTConnectorApi.Bll;

public class DataTransferService
{
    private readonly AzureRepository _azureRepository;
    private readonly SqlRepository _sqlRepository;
    private readonly ILogger<DataTransferService> _logger;

    public DataTransferService(
        AzureRepository azureRepository, 
        SqlRepository sqlRepository, 
        ILogger<DataTransferService> logger)
    {
        _azureRepository = azureRepository;
        _sqlRepository = sqlRepository;
        _logger = logger;
    }
    
    public async Task TransferData()
    {
        DateTime yesteday = DateTime.Now.AddDays(-1);
        
        // Retrieve data from the Azure DB
        var dataList = await _azureRepository
            .QueryAsync($"SELECT * FROM MyTable WHERE DateCreated > {yesteday}");

        // End if not data found to transfer
        if (dataList == null || dataList.Count == 0)
        {
            return;
        }
        
        // Convert data to format that can be used in the INSERT query below
        var values = "";
        for (int i = 0; i < dataList.Count; i++)
        {
            if (i != 0)
            {
                values += ", ";
            }

            var data = dataList[i];
            values += $"({data.Id}, {data.col1}, {data.col2})";
        }

        // Insert data into the SQL DB
        await _sqlRepository
            .ExecuteAsync("INSERT INTO MyTable (id, col1, col2) VALUES {values}");
    }
    
    public async Task<IEnumerable<dynamic>?> ExampleGet(int id)
    {
        var result = await _azureRepository
            .QueryAsync($"SELECT * FROM MyTable WHERE Id={id}");

        return result;
    }

    public async Task ExampleAddRow()
    {
        var rowsAffected = await _azureRepository
            .ExecuteAsync("INSERT INTO MyTable (id, val1, val2)" +
                          "VALUES (1, 2, 3)");
    }

    public void ExampleCallStoredProcWithParameters()
    {
        var parameters = new DynamicParameters();
        parameters.Add("@var1", 11);
        parameters.Add("@var2", "test");

        var rowsAffected = _sqlRepository.ExecuteStoredProcedure("myStoredProc", parameters);
    }
    
    public void ExampleCallStoredProcWithoutParameters()
    {
        var rowsAffected = _sqlRepository.ExecuteStoredProcedure("myStoredProc2");
    }
}