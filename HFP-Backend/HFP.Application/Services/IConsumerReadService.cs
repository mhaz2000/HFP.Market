namespace HFP.Application.Services
{
    public interface IConsumerReadService
    {
        Task<bool> CheckExistByNameAsync(string name);
        Task<bool> CheckExistByUIdAsync(string uid);
    }

}
