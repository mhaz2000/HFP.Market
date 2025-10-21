namespace HFP.Application.Services
{
    public interface IWarehousemanReadService
    {
        Task<bool> CheckExistByNameAsync(string name);
        Task<bool> CheckExistByUIdAsync(string uid);
        Task<bool> CheckIsAdminAsync(string buyerId);
    }

}
