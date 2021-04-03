using System;
using DilemmaSvc.Application.Queries;
using DilemmaSvc.Application.Queries.GetDilemma;
using NUnit.Framework;

namespace DilemmaSvc.Application.Tests.UnitTests
{
    [TestFixture]
    public class GetDilemmaQueryTests
    {
        private GetDilemmaQueryHandler _handler;

        [SetUp]
        public void Setup()
        {
            _handler = new GetDilemmaQueryHandler();
        }

        [Test]
        public void GivenDilemmaDoesntExistWhenGetReturnsNull()
        {
            GetDilemmaQuery query = new GetDilemmaQuery()
            {
                DilemmaId = new Guid("37EFCE6A-31E4-4422-8EF3-400BD465E4B3")
            };
            DilemmaDto dilemma = _handler.Handle(query);

            Assert.IsNull(dilemma);
        }

        [Test]
        public void GivenDilemmaExistsWhenGetReturnsNotNull()
        {
            GetDilemmaQuery query = new GetDilemmaQuery()
            {
                DilemmaId = new Guid("B41ED0C0-9F3B-423C-8B48-C0D9A04E1FE6")
            };
            DilemmaDto dilemma = _handler.Handle(query);

            Assert.IsNotNull(dilemma);
        }
    }
}