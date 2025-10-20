namespace HFP.Application.Services
{
    public interface ITransactionReadService
    {
        Task<decimal> GetTransactionAmountAsync(string buyerId);
    }

}
