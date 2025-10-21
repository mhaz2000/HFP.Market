using HFP.Shared.Abstractions.Commands;
using HFP.Shared.Abstractions.Exceptions;

namespace HFP.Application.Commands.Interactive.Handlers
{
    internal class SendDoorCodeCommandHandler : ICommandHandler<SendDoorCodeCommand>
    {
        private readonly HttpClient _httpClient;

        public SendDoorCodeCommandHandler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task Handle(SendDoorCodeCommand request, CancellationToken cancellationToken)
        {
            var url = $"http://localhost:6000/door-to-open/{request.doorCode}";

            var response = await _httpClient.PostAsync(url, null, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                throw new BusinessException($"Failed to send door code: {response.StatusCode}, {message}");
            }
        }
    }
}
