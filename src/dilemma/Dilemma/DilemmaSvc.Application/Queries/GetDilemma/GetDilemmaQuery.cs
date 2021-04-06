using System;
using Common.Application;

namespace DilemmaSvc.Application.Queries.GetDilemma
{
    public class GetDilemmaQuery : IQuery<DTOs.Dilemma>
    {
        public Guid DilemmaId { get; }

        public GetDilemmaQuery(Guid dilemmaId)
        {
            DilemmaId = dilemmaId;
        }
    }
}