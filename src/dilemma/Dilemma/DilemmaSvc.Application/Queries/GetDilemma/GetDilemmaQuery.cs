using System;
using Common.Application;

namespace DilemmaSvc.Application.Queries.GetDilemma
{
    public class GetDilemmaQuery : IQuery<DilemmaDto>
    {
        public Guid DilemmaId { get; }

        public GetDilemmaQuery(Guid dilemmaId)
        {
            DilemmaId = dilemmaId;
        }
    }
}