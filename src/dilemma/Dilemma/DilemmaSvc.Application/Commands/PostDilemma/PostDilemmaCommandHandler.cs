using System;
using System.Threading;
using System.Threading.Tasks;
using DilemmaApp.Services.Dilemma.Application.Interfaces;
using MediatR;

namespace DilemmaApp.Services.Dilemma.Application.Commands.PostDilemma
{
    public class PostDilemmaCommandHandler : IRequestHandler<PostDilemmaCommand,
        PostDilemmaCommandResult>
    {
        private IDilemmaRepository _dilemmaRepository;

        public PostDilemmaCommandHandler(IDilemmaRepository dilemmaRepository)
        {
            _dilemmaRepository = dilemmaRepository;
        }

        public async Task<PostDilemmaCommandResult> Handle(PostDilemmaCommand request, 
            CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
} 