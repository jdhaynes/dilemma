using System.Collections.Generic;
using Common.Application;
using DilemmaSvc.Application.Queries.GetTopics.DTOs;

namespace DilemmaSvc.Application.Queries.GetTopics
{
    public class GetTopicsQuery : IRequest<ICollection<Topic>>
    {
        // Currently no query parameters.
    }
}