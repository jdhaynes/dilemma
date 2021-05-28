using System;
using DilemmaApp.Services.Common.Application;
using DilemmaApp.Services.Common.Application.RequestPipeline;
using MediatR;

namespace DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma
{
    public class GetDilemmaQuery : IRequest<Response<DTOs.Dilemma>>
    {
        public Guid DilemmaId { get; }

        public GetDilemmaQuery(Guid dilemmaId)
        {
            DilemmaId = dilemmaId;
        }
    }
}