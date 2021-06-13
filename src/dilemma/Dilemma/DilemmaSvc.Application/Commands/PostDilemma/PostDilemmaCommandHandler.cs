using System;
using System.Threading;
using System.Threading.Tasks;
using DilemmaApp.Services.Common.Application;
using DilemmaApp.Services.Dilemma.Application.Interfaces;
using MediatR;

namespace DilemmaApp.Services.Dilemma.Application.Commands.PostDilemma
{
    public class PostDilemmaCommandHandler : IRequestHandler<PostDilemmaCommand,
        Response<PostDilemmaCommandResult>>
    {
        private IDilemmaRepository _dilemmaRepository;

        public PostDilemmaCommandHandler(IDilemmaRepository dilemmaRepository)
        {
            _dilemmaRepository = dilemmaRepository;
        }

        public async Task<Response<PostDilemmaCommandResult>> Handle(PostDilemmaCommand request,
            CancellationToken cancellationToken)
        {
            Domain.Dilemma.Model.Dilemma dilemma = new Domain.Dilemma.Model.Dilemma(
                Guid.NewGuid(),
                request.TopicId,
                request.PosterId,
                request.Question);

            request.Options.ForEach(o => dilemma.AddOption(Guid.NewGuid(), o.Description));
            dilemma.PostToTopic();

            _dilemmaRepository.AddDilemma(dilemma);

            return new Response<PostDilemmaCommandResult>(new PostDilemmaCommandResult()
            {
                DilemmaId = dilemma.Id
            }, ResponseState.Created);
        }
    }
}