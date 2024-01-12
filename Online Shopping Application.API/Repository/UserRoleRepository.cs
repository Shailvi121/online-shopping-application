//namespace Online_Shopping_Application.API.Repository
//{
//    public class UserRoleRepository : Repository<UserRole>, IUserRole
//    {
//        private readonly FammsContext _context;
//        public UserRoleRepository(FammsContext context) : base(context)
//        {
//            _context = context;
//        }

//        public async Task<List<UserRole>> GetRolesAsync(string username)
//        {
//            try
//            {
//                var userRoles = await _context.UserRoles
//                    .Include(ur => ur.Role)
//                    .Include(ur => ur.User)
//                    .Where(ur => ur.User.Username == username)
//                    .ToListAsync();

//                return userRoles;
//            }
//            catch (Exception ex)
//            {
//                throw new Exception($"Error occurred while retrieving roles for user {username}.", ex);
//            }
//        }

//    }
//}