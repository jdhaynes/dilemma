namespace Common.Application
{
    public interface IRequestHandler<TQuery, TResult> where TQuery : IRequest<TResult>
    {
        TResult Handle(TQuery query);
    }
}