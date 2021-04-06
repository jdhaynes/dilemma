using System.Data;
using DilemmaSvc.Application.Common;
using Npgsql;

namespace DilemmaSvc.Infrastructure.Postgres
{
    public class PostgresConnectionFactory : ISqlConnectionFactory
    {
        private IDbConnection _connection;
        private readonly string _connectionString;

        public PostgresConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        
        public IDbConnection GetConnection()
        {
            if (_connection == null || _connection.State == ConnectionState.Closed)
            {
                _connection = new NpgsqlConnection(_connectionString);
                _connection.Open();
            }
            
            return _connection;
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }
    }
}