namespace Online_Shopping_Application.API.Repository.Interface
{
    public interface IUserRole : IRepository<UserRole>
    {
        Task<List<UserRole>> GetRolesAsync(string user);
    }
}