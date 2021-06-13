using System.Data;

namespace DilemmaApp.Services.Common.Application.Interfaces
{
    public interface ISqlConnectionFactory
    {
        public IDbConnection GetConnection();
    }
}