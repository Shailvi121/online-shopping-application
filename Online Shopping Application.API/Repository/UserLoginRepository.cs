namespace Online_Shopping_Application.API.Repository
{
    public class UserLoginRepository : Repository<UserLogin>, IUserLogin
    {
        private readonly FammsContext _context;

        public UserLoginRepository(FammsContext context) : base(context)
        {
            _context = context;
        }

        public async Task<UserLogin> FindUserByEmailAsync(string email)
        {
            var user = await _context.UserLogins
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == email);

            return user;
        }
    }
}



        //public async Task<bool> CheckPasswordAsync(string email, string password)
        //{
        //    var pwd = await _context.UserLogins.FirstOrDefaultAsync(u => u.Password == password);


        //    if (pwd != null)
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}

    