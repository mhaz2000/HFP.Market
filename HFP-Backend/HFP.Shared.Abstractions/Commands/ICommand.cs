using MediatR;

namespace HFP.Shared.Abstractions.Commands
{
    public interface ICommand : IRequest;
    public interface ICommand<TResponse> : IRequest<TResponse>;
}
