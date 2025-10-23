using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;
using Microsoft.Extensions.Configuration;

namespace HFP.Application.Commands.Interactive.Handlers
{
    internal class SendDoorCodeCommandHandler : ICommandHandler<SendDoorCodeCommand>
    {
        private readonly HttpClient _httpClient;

        private readonly string _resberyAddress;

        public SendDoorCodeCommandHandler(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _resberyAddress = configuration["ResberyAddress"] ?? throw new Exception("Resbery address is invalid!");
        }

        public async Task Handle(SendDoorCodeCommand request, CancellationToken cancellationToken)
        {
            var url = $"http://{_resberyAddress}/door-to-open/{request.doorCode}";

            var response = await _httpClient.PostAsync(url, null, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new BusinessException($"Failed to send door code: {response.StatusCode}, {message}");
            }
        }
    }
}
