using System.Data;
using System.Linq;
using Dapper;
using DilemmaApp.Services.Common.Application;
using DilemmaApp.Services.Dilemma.Application.Interfaces;
using DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma.DTOs;

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

        public DTOs.Dilemma Handle(GetDilemmaQuery query)
        {
            DTOs.Dilemma dilemma = GetDilemmaFromDatabase(query);
            dilemma = GetOptionImageUrls(dilemma);
            
            return dilemma;
        }

        private DTOs.Dilemma GetOptionImageUrls(DTOs.Dilemma dilemma)
        {
            // TODO: Can't modify state of dilemma passed in as arg.
            foreach (Option option in dilemma.Options)
            {
                option.ImageUrl = _fileStore.GetPublicUrlForObject(option.ImageObjectId);
            }

            return dilemma;
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
	                       description     AS {nameof(Option.Description)},
	                       image_object_id AS {nameof(Option.ImageObjectId)}
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