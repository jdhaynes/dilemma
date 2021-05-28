using System.Collections.Generic;
using DilemmaApp.Services.Common.Application.RequestPipeline;
using DilemmaApp.Services.Dilemma.Application.Queries.GetTopics.DTOs;
using MediatR;

namespace DilemmaApp.Services.Dilemma.Application.Queries.GetTopics
{
    public class GetTopicsQuery : IRequest<Response<ICollection<Topic>>>
    {
        // Currently no query parameters.
    }
}