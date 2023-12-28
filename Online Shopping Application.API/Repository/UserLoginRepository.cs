namespace Online_Shopping_Application.API.Repository
{
    public class UserLoginRepository : Repository<UserLogin>, IUserLogin
    {
        private readonly FammsContext _context;
        public UserLoginRepository(FammsContext context) : base(context)
        {
            _context = context;
        }
        public async Task<string> FindEmailByAsync(string email)
        {
            var user = await _context.UserLogins.FirstOrDefaultAsync(u => u.Username == email);

            if (user != null)
            {
                return "Email found";
            }
            else
            {
                return "Email not found";
            }
        }


        public async Task<bool> CheckPasswordAsync(string email, string password)
        {
            var user = await _context.UserLogins.FirstOrDefaultAsync(u => u.Username == email);

            if (user != null)
            {
                if (user.Password == password)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;

            }
        }

    }
}