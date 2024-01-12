namespace Online_Shopping_Application.API.Repository.Interface
{
    public interface IUserLogin : IRepository<UserLogin>
    {
        Task<UserLogin> FindUserByEmailAsync(string email);
        //Task<bool> CheckPasswordAsync(string email, string password);
        

    }
}