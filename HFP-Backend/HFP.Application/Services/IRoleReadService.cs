namespace HFP.Application.Services
{
    public interface IRoleReadService
    {
        Task<string?> GetUserRoleNameAsync(Guid userId);
        Task<Guid> GetRoleIdByNameAsync(string roleName);
    }
}
