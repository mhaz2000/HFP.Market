using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.Products.Handlers
{
    internal class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetAsync(p=> p.Id == request.Id);
            if (product is null)
                throw new BusinessException("محصول مورد نظر یافت نشد.");
            await _repository.DeleteAsync(product.Id);
        }
    }
}
