namespace DilemmaApp.Services.Common.Application
{
    public interface ICommandHandler<in TCommand, TResult> : IRequestHandler<TCommand, TResult>
        where TCommand : ICommand<TResult>, IRequest<TResult>
    {
    }
}