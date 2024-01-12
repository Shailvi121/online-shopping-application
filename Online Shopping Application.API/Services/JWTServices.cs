using Online_Shopping_Application.Response;

namespace Online_Shopping_Application.API.Services
{
    public class JWTServices
    {
        private readonly IConfiguration _configuration;

        public JWTServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public JWTResponse GenerateToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

           
            var JwtResponse = new JWTResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return JwtResponse; 
        }
    }
}
