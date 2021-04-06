using System.Data;
using System.Data.SqlClient;

namespace DilemmaSvc.Application.Common
{
    public interface ISqlConnectionFactory
    {
        public IDbConnection GetConnection();
    }
}