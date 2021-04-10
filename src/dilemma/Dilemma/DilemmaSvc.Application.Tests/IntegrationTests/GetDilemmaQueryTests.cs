using System;
using DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma;
using DilemmaApp.Services.Dilemma.Infrastructure.Postgres;
using NUnit.Framework;

namespace DilemmaApp.Services.Dilemma.Application.Tests.IntegrationTests
{
    [TestFixture]
    public class GetDilemmaQueryTests
    {
        private GetDilemmaQueryHandler _handler;

        private string _connString =
            "User ID=dev;Password=Dev_!=;Host=localhost;Port=25522;Database=dilemma_svc";

        [SetUp]
        public void Setup()
        {
            PostgresConnectionFactory connectionFactory =
                new PostgresConnectionFactory(_connString);

            _handler = new GetDilemmaQueryHandler(connectionFactory);
        }

        [Test]
        public void GivenDilemmaDoesntExistWhenGetReturnsNull()
        {
            Guid id = new Guid("37EFCE6A-31E4-4422-8EF3-400BD465E4B3");
            GetDilemmaQuery query = new GetDilemmaQuery(id);
            var dilemma = _handler.Handle(query);

            Assert.IsNull(dilemma);
        }

        [Test]
        public void GivenDilemmaExistsWhenGetReturnsNotNull()
        {
            Guid id = new Guid("7cc8b887-5461-4e26-901d-d87a7774a499");
            GetDilemmaQuery query = new GetDilemmaQuery(id);
            var dilemma = _handler.Handle(query);

            Assert.IsNotNull(dilemma);
        }
    }
}