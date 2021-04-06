using System.Data;
using System.Linq;
using Common.Application;
using Dapper;
using DilemmaSvc.Application.Common;
using DilemmaSvc.Application.Queries.GetDilemma.DTOs;

namespace DilemmaSvc.Application.Queries.GetDilemma
{
    public class GetDilemmaQueryHandler : IQueryHandler<GetDilemmaQuery, DTOs.Dilemma>
    {
        private readonly ISqlConnectionFactory _connectionFactory;

        public GetDilemmaQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
        {
            _connectionFactory = sqlConnectionFactory;
        }

        public DTOs.Dilemma Handle(GetDilemmaQuery query)
        {
            return GetDilemmaAndOptions(query);
        }

        private DTOs.Dilemma GetDilemmaAndOptions(GetDilemmaQuery query)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                // TODO: add column index to option.dilemma_id to optimise option lookup.
                // TODO: potential to optimise to utilise single query with LEFT JOIN.
                string sql = $@"
                    SELECT id             AS {nameof(DTOs.Dilemma.DilemmaId)},
	                       question       AS {nameof(DTOs.Dilemma.Question)},
	                       posted_date    AS {nameof(DTOs.Dilemma.PostedDate)},
	                       withdrawn_date AS {nameof(DTOs.Dilemma.WithdrawnDate)}
                    FROM dilemma
                    WHERE dilemma.id = @DilemmaId;

                    SELECT id              AS {nameof(Option.OptionId)},
	                       description     AS {nameof(Option.Description)},
	                       image_object_id AS {nameof(Option.ImageObjectId)}
                    FROM option
                    WHERE option.dilemma_id = @DilemmaId;";

                using (var multiQuery =
                    connection.QueryMultiple(sql, new { DilemmaId = query.DilemmaId }))
                {
                    DTOs.Dilemma dilemma = multiQuery.Read<DTOs.Dilemma>().SingleOrDefault();
                    if (dilemma != null)
                    {
                        dilemma.Options = multiQuery.Read<Option>().ToList();
                    }

                    return dilemma;
                }
            }
        }
    }
}