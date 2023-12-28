namespace Online_Shopping_Application.API.Repository.Interface
{
    public interface IUserLogin : IRepository<UserLogin>
    {
        Task<string> FindEmailByAsync(string email);
        Task<bool> CheckPasswordAsync(string email, string password);



    }
}