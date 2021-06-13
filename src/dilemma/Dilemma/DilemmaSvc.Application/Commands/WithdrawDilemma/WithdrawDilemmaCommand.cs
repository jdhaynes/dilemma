using System;
using DilemmaApp.Services.Common.Application;
using MediatR;

namespace DilemmaApp.Services.Dilemma.Application.Commands.WithdrawDilemma
{
    public class WithdrawDilemmaCommand : IRequest<Response>
    {
        public Guid DilemmaId { get; set; }

        public WithdrawDilemmaCommand(Guid dilemmaId)
        {
            DilemmaId = dilemmaId;
        }
    }
}