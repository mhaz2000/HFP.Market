using HFP.Domain.Consts;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;

namespace HFP.Application.Commands.Interactive.Handlers
{
    internal class CustomerEnteredHandler : ICommandHandler<CustomerEnteredCommand>
    {
        private readonly ITransactionFactory _factory;
        private readonly ITransactionRepository _repository;
        private readonly IBuyerRepository _buyerRepository;
        private readonly IBuyerFactory _buyerFactory;

        public CustomerEnteredHandler(ITransactionRepository repository, ITransactionFactory factory, IBuyerRepository buyerRepository, IBuyerFactory buyerFactory)
        {
            _factory = factory;
            _repository = repository;
            _buyerRepository = buyerRepository;
            _buyerFactory = buyerFactory;
        }

        public async Task Handle(CustomerEnteredCommand request, CancellationToken cancellationToken)
        {
            var buyer = await _buyerRepository.GetAsync(b => b.BuyerId == request.BuyerId);
            if(buyer is null)
            {
                buyer = _buyerFactory.Create(request.BuyerId);
                await _buyerRepository.AddAsync(buyer);
            }

            var previousTransaction = await _repository
                .GetAsync(t => t.BuyerId == request.BuyerId && t.Type == TransactionType.PreInvoice && t.Status == TransactionStatus.Pending && !t.Products.Any(),
                t => t.Products);

            if (previousTransaction is not null)
                await _repository.DeleteAsync(previousTransaction.Id);

            var transaction = _factory.Create(TransactionStatus.Pending, TransactionType.PreInvoice, DateTime.Now, request.BuyerId);

            await _repository.AddAsync(transaction);
        }
    }
}
