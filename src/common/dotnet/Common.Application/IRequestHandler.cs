namespace DilemmaApp.Services.Common.Application
{
    public interface IRequestHandler<in TRequest, TResult> where TRequest : IRequest<TResult>
    {
        TResult Handle(TRequest request);
    }
}