using System.Threading;
using System.Threading.Tasks;
using DilemmaApp.Services.Dilemma.Application.Interfaces;
using MediatR;

namespace DilemmaApp.Services.Dilemma.Application.Commands.WithdrawDilemma
{
    public class WithdrawDilemmaCommandHandler : IRequestHandler<WithdrawDilemmaCommand, Unit>
    {
        private IDilemmaRepository _dilemmaRepository;

        public WithdrawDilemmaCommandHandler(IDilemmaRepository dilemmaRepository)
        {
            _dilemmaRepository = dilemmaRepository;
        }

        public async Task<Unit> Handle(WithdrawDilemmaCommand request,
            CancellationToken cancellationToken)
        {
            var dilemma = _dilemmaRepository.GetDilemma(request.DilemmaId);
            dilemma.Withdraw();
            _dilemmaRepository.UpdateDilemma(dilemma);

            return Unit.Value;
        }
    }
}