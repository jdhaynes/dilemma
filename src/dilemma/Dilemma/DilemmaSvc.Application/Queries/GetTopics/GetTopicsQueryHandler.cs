using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using DilemmaApp.Services.Common.Application;
using DilemmaApp.Services.Dilemma.Application.Interfaces;
using DilemmaApp.Services.Dilemma.Application.Queries.GetTopics.DTOs;

namespace DilemmaApp.Services.Dilemma.Application.Queries.GetTopics
{
    public class GetTopicsQueryHandler : IRequestHandler<GetTopicsQuery, ICollection<Topic>>
    {
        private ISqlConnectionFactory _connectionFactory;

        public GetTopicsQueryHandler(ISqlConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public ICollection<Topic> Handle(GetTopicsQuery query)
        {
            // TODO: this query result won't change often - cache result?
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                string sql = $@"
                    SELECT id   AS {nameof(Topic.TopicId)},
	                       name AS {nameof(Topic.Name)}
                    FROM topic";

                List<Topic> topics = connection.Query<Topic>(sql).ToList();
                return topics;
            }
        }
    }
}