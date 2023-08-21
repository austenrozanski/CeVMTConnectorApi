namespace CeVMTConnectorApi.Dal.Contexts;

public class SqlDataContext : BaseDataContext
{
    public SqlDataContext(IConfiguration config) 
        : base(config.GetConnectionString("SqlConnection") ?? "")
    {
    }
}