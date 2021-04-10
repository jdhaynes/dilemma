using DilemmaApp.Common.Application;

namespace DilemmaApp.Services.Common.Application
{
    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand, IRequest<TResult>
    {
    }
}