using System.Data;

namespace DilemmaSvc.Application.Interfaces
{
    public interface ISqlConnectionFactory
    {
        public IDbConnection GetConnection();
    }
}