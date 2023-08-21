using CeVMTConnectorApi.Dal.Contexts;

namespace CeVMTConnectorApi.Dal.Repositories;

public class SqlRepository : BaseRepository
{
    public SqlRepository(SqlDataContext dataContext) : base(dataContext)
    {
    }
}