using System;
using System.Linq;
using DilemmaApp.Services.Dilemma.Infrastructure.Postgres;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace DilemmaApp.Services.Dilemma.Infrastructure.Tests.PostgresDb
{
    [TestFixture]
    public class EntityFrameworkTests
    {
        private const string _connString = "Database=dilemma_svc;User ID=dev;Password=Dev_!=;Host=localhost;Port=25522;";
        private IServiceCollection _collection;
        private IServiceProvider _provider;

        public EntityFrameworkTests()
        {
            _collection = new ServiceCollection();
            _collection.AddDbContext<DilemmaContext>(options => options.UseNpgsql(_connString));
            _provider = _collection.BuildServiceProvider();
        }
        
        [Test]
        public void Test1()
        {
            DilemmaContext context = _provider.GetService<DilemmaContext>();
            var optionSql = context.Options.ToQueryString();
            var option = context.Options.ToList();
            var dilemma = context.Dilemmas.ToList();
            
            Assert.Pass();
        }
    }
}