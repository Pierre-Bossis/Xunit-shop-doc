using Microsoft.IdentityModel.Tokens;
using Shop.Models.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shop.Tools
{
    public class JwtGenerator
    {
        private readonly IConfiguration _config;

        public JwtGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string Generate(ConnectedUserDTO user)
        {
            if(user is null)
                throw new ArgumentNullException("user");

            string key = _config.GetSection("tokenInfo").GetSection("secretKey").Value;
            SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(key));
            SigningCredentials signingKey = new(securityKey, SecurityAlgorithms.HmacSha512);

            Claim[] myClaims = new[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User"),
                //new Claim(ClaimTypes.Email, user.Email),
                //new Claim("Pseudo", user.Pseudo)
            };

            JwtSecurityToken jwt = new(
                claims: myClaims,
                signingCredentials: signingKey,
                expires: DateTime.Now.AddDays(7),
                issuer: "https://localhost:7197", //Emetteur du token
                audience: "http://localhost:4200" //Consomateur du token
                );

            JwtSecurityTokenHandler handler = new();

            return handler.WriteToken(jwt);
        }
    }
}
