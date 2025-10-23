using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using Microsoft.Extensions.Configuration;

namespace HFP.Application.Commands.Interactive.Handlers
{
    internal class MarketDoorClosedCommandHandler : ICommandHandler<MarketDoorClosedCommand>
    {
        private readonly ITransactionRepository _repository;
        private readonly string Path;
        public MarketDoorClosedCommandHandler(ITransactionRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            Path = configuration["StaticPath"] ?? throw new Exception("Path invalid!");
        }
        public async Task Handle(MarketDoorClosedCommand request, CancellationToken cancellationToken)
        {
            try
            {
                File.WriteAllText(Path, "0");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to file: {ex.Message}");
            }

            await _repository.SetPendingTransactionAsCanceledAsync();
        }
    }
}
