using System.Data;
namespace CleanArchitecture.Shared.Interfaces
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
