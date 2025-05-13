namespace HFP.Application.Services
{
    public interface IProductReadService
    {
        Task<bool> CheckExistByNameAsync(string productName, Guid? id = null);
    }
}
