using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using DilemmaApp.Services.Common.Application;
using DilemmaApp.Services.Dilemma.Application.Interfaces;
using DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma.DTOs;
using MediatR;

namespace DilemmaApp.Services.Dilemma.Application.Queries.GetDilemma
{
    public class GetDilemmaQueryHandler : IRequestHandler<GetDilemmaQuery, Response<DTOs.Dilemma>>
    {
        private readonly ISqlConnectionFactory _connectionFactory;
        private readonly IFileStore _fileStore;

        public GetDilemmaQueryHandler(ISqlConnectionFactory sqlConnectionFactory,
            IFileStore fileStore)
        {
            _connectionFactory = sqlConnectionFactory;
            _fileStore = fileStore;
        }

        public async Task<Response<DTOs.Dilemma>> Handle(GetDilemmaQuery query,
            CancellationToken cancellationToken)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection())
            {
                string sql = $@"
                    SELECT d.id             AS {nameof(DTOs.Dilemma.DilemmaId)},
                           d.topic_id       AS {nameof(DTOs.Dilemma.TopicId)},
	                       d.question       AS {nameof(DTOs.Dilemma.Question)},
	                       d.posted_date    AS {nameof(DTOs.Dilemma.PostedDate)},
	                       d.withdrawn_date AS {nameof(DTOs.Dilemma.WithdrawnDate)},
                           p.id             AS {nameof(DTOs.Poster.PosterId)},
                           p.dob            AS {nameof(DTOs.Poster.DateOfBirth)},
                           o.id             AS {nameof(DTOs.Option.OptionId)},
                           o.description    AS {nameof(DTOs.Option.Description)}
                    FROM dilemma AS d
                    LEFT JOIN poster AS p
                    ON d.poster_id = p.id
                    LEFT JOIN option AS o
                    ON d.id = o.dilemma_id
                    WHERE d.id = @DilemmaId;";

                Dictionary<Guid, DTOs.Dilemma> dict = new Dictionary<Guid, DTOs.Dilemma>();

                DTOs.Dilemma result = connection.Query<DTOs.Dilemma, Poster, Option, DTOs.Dilemma>(
                        sql,
                        (dilemma, poster, option) =>
                        {
                            DTOs.Dilemma dilemmaEntity;

                            if (!dict.TryGetValue(dilemma.DilemmaId, out dilemmaEntity))
                            {
                                dilemmaEntity = dilemma;
                                dilemmaEntity.Options = new List<Option>();
                                dilemmaEntity.Poster = poster;
                                dict.Add(dilemmaEntity.DilemmaId, dilemmaEntity);
                            }

                            dilemmaEntity.Options.Add(option);
                            return dilemmaEntity;
                        },
                        splitOn: $"{nameof(DTOs.Poster.PosterId)},{nameof(Option.OptionId)}",
                        param: new {DilemmaId = query.DilemmaId})
                    .Distinct()
                    .SingleOrDefault();
                
                return new Response<DTOs.Dilemma>(result, ResponseState.Ok);
            }
        }
    }
}