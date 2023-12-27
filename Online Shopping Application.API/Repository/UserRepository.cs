//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore;
//using Online_Shopping_Application.API.Models; // Assuming you have a User model

//namespace Online_Shopping_Application.API.Repository
//{
//    public class UserRepository<T> : IUserRepository<T> where T : class
//    {
//        private readonly FammsContext _context;

//        public UserRepository(FammsContext context)
//        {
//            _context = context;
//        }

//        public async Task<T?> FindByEmailAsync(string email)
//        {
//            return await _context.Set<T>().FirstOrDefaultAsync(u => u.Username == email);
//        }

//        public async Task<T> CheckPasswordAsync(T user, string password)
//        {

//            return await _context.Set<T>().FindAsync(u => u.Password == password);  
//        }

//        public async Task<T> GetRolesAsync(T user)
//        {
            
//        }

//        public async Task<T> CreateUserAsync(T user, string password)
//        {
            
//        }
//    }
//}
