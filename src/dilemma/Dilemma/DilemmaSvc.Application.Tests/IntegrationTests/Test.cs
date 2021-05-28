using System;
using System.Threading;
using DilemmaApp.Services.Common.Application;
using DilemmaApp.Services.Common.Application.RequestPipeline;
using DilemmaApp.Services.Common.Application.RequestPipeline.Validation;
using DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma;
using DilemmaApp.Services.Dilemma.Infrastructure.Postgres;
using NUnit.Framework;

namespace DilemmaApp.Services.Dilemma.Application.Tests.IntegrationTests
{
    [TestFixture]
    public class Test
    {
        [Test]
        public void Test1()
        {
            GetDilemmaQuery query = new GetDilemmaQuery(default(Guid));
            GetDilemmaQueryHandler handler = new GetDilemmaQueryHandler(
                new PostgresConnectionFactory(
                    "Database=dilemma_svc;User ID=dev;Password=Dev_!=;Host=localhost;Port=25522;"),
                null);


            var validation =
                new ValidationHandler<GetDilemmaQuery, 
                    Response<Queries.GetDilemma.DTOs.Dilemma>,
                    Queries.GetDilemma.DTOs.Dilemma>
                    (handler, new GetDilemmaQueryValidator()).Handle(query, new CancellationToken());
        }
    }
}