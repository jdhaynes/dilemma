using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DilemmaApp.Services.Dilemma.Application.Interfaces;
using DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma.DTOs;
using MediatR;

namespace DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma
{
    public class GetDilemmaQueryHandler : IRequestHandler<GetDilemmaQuery, DTOs.Dilemma>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IFileStore _fileStore;

        public GetDilemmaQueryHandler(ISqlConnectionFactory sqlConnectionFactory,
            IFileStore fileStore)
        {
            _connectionFactory = sqlConnectionFactory;
            _fileStore = fileStore;
        }

        public async Task<DTOs.Dilemma> Handle(GetDilemmaQuery query, 
            CancellationToken cancellationToken)
        {
            return GetDilemmaFromDatabase(query);
        }
        
        private DTOs.Dilemma GetDilemmaFromDatabase(GetDilemmaQuery query)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                // TODO: Add column index to option.dilemma_id to optimise option lookup.
                // TODO: Potential to optimise to utilise single query with LEFT JOIN.

                string sql = $@"
                    SELECT id             AS {nameof(DTOs.Dilemma.DilemmaId)},
	                       question       AS {nameof(DTOs.Dilemma.Question)},
	                       posted_date    AS {nameof(DTOs.Dilemma.PostedDate)},
	                       withdrawn_date AS {nameof(DTOs.Dilemma.WithdrawnDate)}
                    FROM dilemma
                    WHERE dilemma.id = @DilemmaId;

                    SELECT id              AS {nameof(Option.OptionId)},
	                       description     AS {nameof(Option.Description)}
                    FROM option
                    WHERE option.dilemma_id = @DilemmaId;";

                using (var multiQuery =
                    connection.QueryMultiple(sql, new {DilemmaId = query.DilemmaId}))
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