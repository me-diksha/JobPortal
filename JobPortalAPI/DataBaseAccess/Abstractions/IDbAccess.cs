using System.Data;

namespace JobPortalAPI.DataBaseAccess.Abstractions
{
    public interface IDbAccess
    {
        IDbConnection CreateConnection();
    }
}
