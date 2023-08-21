using System.Transactions;

namespace CeVMTConnectorApi.Dal.Contexts;

public class AzureDataContext : BaseDataContext
{
    public AzureDataContext(IConfiguration config)
        : base(config.GetConnectionString("AzureConnection") ?? "")
    {
    }
}