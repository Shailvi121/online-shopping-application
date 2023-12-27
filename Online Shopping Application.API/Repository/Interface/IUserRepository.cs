namespace Online_Shopping_Application.API.Repository.Interface
{
    public interface IUserRepository<T> where T : class
    {
      

        Task<T?> FindByEmailAsync(string email);

        Task<T> CheckPasswordAsync(T user, string password);

        Task<T> GetRolesAsync(T user);

       
        Task<T> CreateUserAsync(T user, string password);

        
    }
}
