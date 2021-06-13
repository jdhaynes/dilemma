using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DilemmaApp.Services.Common.Application;
using DilemmaApp.Services.Dilemma.Application.Interfaces;
using DilemmaApp.Services.Dilemma.Application.Queries.GetTopics.DTOs;
using MediatR;

namespace DilemmaApp.Services.Dilemma.Application.Queries.GetTopics
{
    public class GetTopicsQueryHandler : IRequestHandler<GetTopicsQuery, Response<ICollection<Topic>>>
    {
        private ISqlConnectionFactory _connectionFactory;

        public GetTopicsQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Response<ICollection<Topic>>> Handle(GetTopicsQuery query,
            CancellationToken cancellationToken)
        {
            // TODO: this query result won't change often - cache result?
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                string sql = $@"
                    SELECT id   AS {nameof(Topic.TopicId)},
	                       name AS {nameof(Topic.Name)}
                    FROM topic";

                List<Topic> topics = connection.Query<Topic>(sql).ToList();
                return new Response<ICollection<Topic>>(topics, ResponseState.Ok);
            }
        }
    }
}