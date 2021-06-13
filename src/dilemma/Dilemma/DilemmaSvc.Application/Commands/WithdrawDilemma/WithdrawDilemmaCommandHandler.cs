using System.Threading;
using System.Threading.Tasks;
using DilemmaApp.Services.Common.Application;
using DilemmaApp.Services.Dilemma.Application.Interfaces;
using MediatR;

namespace DilemmaApp.Services.Dilemma.Application.Commands.WithdrawDilemma
{
    public class WithdrawDilemmaCommandHandler : IRequestHandler<WithdrawDilemmaCommand, Response>
    {
        private IDilemmaRepository _dilemmaRepository;

        public WithdrawDilemmaCommandHandler(IDilemmaRepository dilemmaRepository)
        {
            _dilemmaRepository = dilemmaRepository;
        }

        public async Task<Response> Handle(WithdrawDilemmaCommand request,
            CancellationToken cancellationToken)
        {
            var dilemma = _dilemmaRepository.GetDilemma(request.DilemmaId);
            dilemma.Withdraw();
            _dilemmaRepository.UpdateDilemma(dilemma);

            return new Response();
        }
    }
}