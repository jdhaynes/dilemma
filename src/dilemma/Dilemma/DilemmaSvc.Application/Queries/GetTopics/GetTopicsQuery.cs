using System.Collections.Generic;
using DilemmaApp.Services.Common.Application;
using DilemmaApp.Services.Dilemma.Application.Queries.GetTopics.DTOs;

namespace DilemmaApp.Services.Dilemma.Application.Queries.GetTopics
{
    public class GetTopicsQuery : IRequest<ICollection<Topic>>
    {
        // Currently no query parameters.
    }
}