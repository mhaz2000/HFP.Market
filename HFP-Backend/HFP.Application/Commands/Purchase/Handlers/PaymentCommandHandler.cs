using HFP.Application.DTO;
using HFP.Application.Services;
using HFP.Domain.Repositories;
using HFP.Shared.Abstractions.Commands;
using Microsoft.Extensions.Configuration;
using PosInterface;

namespace HFP.Application.Commands.Purchase.Handlers
{
    internal class PaymentCommandHandler : ICommandHandler<PaymentCommand, PaymentResultDto>
    {
        private readonly ITransactionRepository _repository;
        private readonly ITransactionReadService _readService;

        private readonly string _pcPosIp;
        private readonly int _pcPosPort;
        public PaymentCommandHandler(ITransactionRepository repository, ITransactionReadService readService, IConfiguration configuration)
        {
            _repository = repository;
            _readService = readService;

            _pcPosIp = configuration["PCPos:Ip"] ?? string.Empty;
            _pcPosPort = int.Parse(configuration["PCPos:Port"] ?? "0");
        }
        public async Task<PaymentResultDto> Handle(PaymentCommand request, CancellationToken cancellationToken)
        {
            PCPos pcPos = new PCPos();
            pcPos.InitLAN(_pcPosIp, _pcPosPort);

            var priceToPay = await _readService.GetTransactionAmountAsync(request.BuyerId);

            PaymentResult result = pcPos.DoSyncPayment(((long)priceToPay*10).ToString(), string.Empty, "1232", DateTime.Now);

            return new PaymentResultDto() { IsSuccess = result.ErrorCode == 0, ErrorMessage = result.ErrorMsg };
        }
    }
}
