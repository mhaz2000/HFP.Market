using HFP.Application.Services;
using HFP.Domain.Consts;
using HFP.Domain.Factories.interfaces;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;
using Microsoft.Extensions.Configuration;

namespace HFP.Application.Commands.Interactive.Handlers
{

    internal class CustomerEnteredHandler : ICommandHandler<CustomerEnteredCommand, (bool, bool)>
    {
        private readonly IConsumerRepository _consumerRepository;
        private readonly ITransactionFactory _factory;
        private readonly ITransactionRepository _repository;
        private readonly IBuyerRepository _buyerRepository;
        private readonly IBuyerFactory _buyerFactory;
        private readonly HttpClient _httpClient;

        private readonly string _resberyAddress;
        private readonly string Path;
        public CustomerEnteredHandler(ITransactionRepository repository, ITransactionFactory factory, IConsumerRepository consumerRepository,
            IBuyerRepository buyerRepository, IBuyerFactory buyerFactory, HttpClient httpClient, IConfiguration configuration)
        {
            _factory = factory;
            _repository = repository;
            _buyerRepository = buyerRepository;
            _consumerRepository = consumerRepository;
            _buyerFactory = buyerFactory;
            _httpClient = httpClient;
            Path = configuration["StaticPath"] ?? throw new Exception("Path invalid");
            _resberyAddress = configuration["ResberyAddress"] ?? throw new Exception("Path invalid");
        }

        public async Task<(bool, bool)> Handle(CustomerEnteredCommand request, CancellationToken cancellationToken)
        {
            var buyer = await _buyerRepository.GetAsync(b => b.BuyerId == request.BuyerId);
            if (buyer is null)
            {
                buyer = _buyerFactory.Create(request.BuyerId);
                await _buyerRepository.AddAsync(buyer);
            }

            var previousTransaction = await _repository
                .GetAsync(t => t.BuyerId == request.BuyerId && t.Type == TransactionType.Invoice && t.Status == TransactionStatus.Pending && !t.Products.Any(),
                t => t.Products);

            if (previousTransaction is not null)
                await _repository.DeleteAsync(previousTransaction.Id);

            var transaction = _factory.Create(TransactionStatus.Pending, TransactionType.Invoice, DateTime.UtcNow, request.BuyerId);

            await _repository.AddAsync(transaction);

            try
            {
                File.WriteAllText(Path, "1");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to file: {ex.Message}");
            }

            var consumer = await _consumerRepository.GetAsync(t=> t.UId == request.BuyerId);
            if (consumer is null)
                return (false, false);

            if (!consumer.IsWarehouseman)
            {
                try
                {
                    var url = $"http://{_resberyAddress}/door-to-open/1";

                    var response = await _httpClient.PostAsync(url, null, cancellationToken);

                    if (!response.IsSuccessStatusCode)
                    {
                        var message = await response.Content.ReadAsStringAsync();
                        throw new BusinessException($"Failed to send door code: {response.StatusCode}, {message}");
                    }
                }
                catch (Exception ex)
                {
                }
                
            }

            return (true, consumer.IsWarehouseman);
        }
    }
}
