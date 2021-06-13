using System;
using System.Data;
using DilemmaApp.Services.Common.Application.Interfaces;
using Npgsql;

namespace DilemmaApp.Common.Infrastructure
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
