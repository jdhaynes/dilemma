using System.Data;

namespace DilemmaApp.Services.Dilemma.Application.Interfaces
{
    public interface ISqlConnectionFactory
    {
        public IDbConnection GetConnection();
    }
}