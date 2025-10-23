using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.Consumers.Handlers;

internal class DeleteConsumerCommandHandler : ICommandHandler<DeleteConsumerCommand>
{
    private readonly IConsumerRepository _repository;

    public DeleteConsumerCommandHandler(IConsumerRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteConsumerCommand request, CancellationToken cancellationToken)
    {
        var consumer = await _repository.GetAsync(c=> c.Id == request.Id);

        if (consumer is null)
            throw new BusinessException("مصرف کننده یافت نشد.");

        await _repository.DeleteAsync(consumer.Id);
    }
}