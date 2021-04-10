using System.Threading;
using NUnit.Framework;

namespace DilemmaApp.Services.Dilemma.Infrastructure.Tests.PostgresDb
{
    [TestFixture]
    public class SqlDatabaseTests
    {
        private TestDatabase _db;
        private const string _connString = "User ID=dev;Password=Dev_!=;Host=localhost;Port=25522;";

        [OneTimeSetUp]
        public void CreateTestDatabase()
        {
            _db = new TestDatabase(_connString);
            _db.ExecuteScript("../../../../../database/db-create.sql");
            _db.ExecuteScript("../../../../../database/db-test-data.sql");
        }

        [Test]
        public void Test()
        {
            Thread.Sleep(20000);
        }

        [OneTimeTearDown]
        public void DisposeTestDatabase()
        {
            _db.Dispose();
        }
    }
}