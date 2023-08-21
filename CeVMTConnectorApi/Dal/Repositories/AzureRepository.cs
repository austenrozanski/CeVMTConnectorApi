using CeVMTConnectorApi.Dal.Contexts;

namespace CeVMTConnectorApi.Dal.Repositories;

public class AzureRepository : BaseRepository
{
    public AzureRepository(AzureDataContext dataContext) : base(dataContext)
    {
    }
}