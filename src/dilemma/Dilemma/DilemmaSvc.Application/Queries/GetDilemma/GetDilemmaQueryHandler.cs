using Common.Application;

namespace DilemmaSvc.Application.Queries.GetDilemma
{
    public class GetDilemmaQueryHandler : IQueryHandler<GetDilemmaQuery, DilemmaDto>
    {
        public DilemmaDto Handle(GetDilemmaQuery query)
        {
            return null;
        }
    }
}