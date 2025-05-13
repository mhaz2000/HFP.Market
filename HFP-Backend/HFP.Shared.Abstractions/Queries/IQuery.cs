using MediatR;

namespace HFP.Shared.Abstractions.Queries
{
    public interface IQuery : IRequest;

    public interface IQuery<TResult> : IQuery, IRequest<TResult>;
}
