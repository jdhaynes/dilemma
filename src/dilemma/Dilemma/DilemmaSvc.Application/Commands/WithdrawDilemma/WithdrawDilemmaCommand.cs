using System;
using MediatR;

namespace DilemmaApp.Services.Dilemma.Application.Commands.WithdrawDilemma
{
    public class WithdrawDilemmaCommand : IRequest<Unit>
    {
        public Guid DilemmaId { get; set; }

        public WithdrawDilemmaCommand(Guid dilemmaId)
        {
            DilemmaId = dilemmaId;
        }
    }
}