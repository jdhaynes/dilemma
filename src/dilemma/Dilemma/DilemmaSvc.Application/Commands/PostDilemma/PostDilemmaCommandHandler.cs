using System;
using DilemmaApp.Services.Common.Application;
using DilemmaApp.Services.Dilemma.Application.Interfaces;

namespace DilemmaApp.Services.Dilemma.Application.Commands.PostDilemma
{
    public class PostDilemmaCommandHandler : ICommandHandler<PostDilemmaCommand,
        PostDilemmaCommandResult>
    {
        private IDilemmaRepository _dilemmaRepository;

        public PostDilemmaCommandHandler(IDilemmaRepository dilemmaRepository)
        {
            _dilemmaRepository = dilemmaRepository;
        }

        public PostDilemmaCommandResult Handle(PostDilemmaCommand request)
        {
            throw new NotImplementedException();
        }
    }
} 