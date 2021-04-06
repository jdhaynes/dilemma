using System;
using System.IO;
using Npgsql;

namespace DilemmaSvc.Infrastructure.Tests
{
    public class TestDatabase : IDisposable
    {
        private NpgsqlConnection _conn;
        private NpgsqlConnectionStringBuilder _connString;
        private bool _exists = false;

        public bool Exists => _exists;

        public TestDatabase(string connectionString)
        {
            _connString = new NpgsqlConnectionStringBuilder(connectionString);
            OpenConnection();
            
            _connString.Database = GenerateRandName();
        }

        private void OpenConnection()
        {
            _conn = new NpgsqlConnection(_connString.ToString());
            _conn.Open();
        }

        private string GenerateRandName()
        {
            return $"testdb_{DateTime.Now.Ticks}";
        }

        public void Create()
        {
            ExecuteCommand($"CREATE DATABASE {_connString.Database}");
            _exists = true;
        }

        public void Drop()
        {
            ExecuteCommand($@"
                SELECT pid, PG_TERMINATE_BACKEND(pid)
                FROM pg_stat_activity
                WHERE datname = '{_connString.Database}'
                  AND pid <> PG_BACKEND_PID();");
            
            ExecuteCommand($"DROP DATABASE {_connString.Database}");

            _exists = false;
        }

        private void ExecuteCommand(string command)
        {
            using (NpgsqlCommand nspgsqlCommand = _conn.CreateCommand())
            {
                nspgsqlCommand.CommandText = command;
                nspgsqlCommand.ExecuteNonQuery();
            }
        }

        public void ExecuteScript(string path)
        {
            string scriptSql = File.ReadAllText(path);
            ExecuteCommand(scriptSql);
        }

        public void Dispose()
        {
            if (_exists)
            {
                Drop();
            }
            
            _conn?.Close();
        }
    }
}