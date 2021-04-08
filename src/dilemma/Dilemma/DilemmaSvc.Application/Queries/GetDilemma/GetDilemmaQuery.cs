using System;
using Common.Application;

namespace DilemmaSvc.Application.Queries.GetDilemma
{
    public class GetDilemmaQuery : IRequest<DTOs.Dilemma>
    {
        public Guid DilemmaId { get; }

        public GetDilemmaQuery(Guid dilemmaId)
        {
            DilemmaId = dilemmaId;
        }
    }
}