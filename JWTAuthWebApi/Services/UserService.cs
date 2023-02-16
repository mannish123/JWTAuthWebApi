using JWTAuthWebApi.Entities;
using JWTAuthWebApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JWTAuthWebApi.Services
{

    public class UserService : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private IConfiguration _configuration;
        //private List<User> _users = new List<User>
        //{
        //    new User { Id = 1, FirstName = "Test", LastName = "User", Username = "test", Password = "test" }
        //};

        public UserService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            User user = new User();
            using (var context = new SchoolContext())
            {
                var userData = context.User.Where(x => x.Username == model.Username && x.Password == model.Password);



                // return null if user not found
                if (userData == null) return null;
                else
                {
                    user = userData.FirstOrDefault();
                }
            }
            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<User> GetAll()
        {
            List<User> _users;
            using (var context = new SchoolContext())
            {
                _users = context.User.ToList();
            }

            return _users;
        }

        public User GetById(int id)
        {
            //return _users.FirstOrDefault(x => x.Id == id);

            User _users;
            using (var context = new SchoolContext())
            {
                _users = context.User.Where(x => x.Id == id).FirstOrDefault();
            }

            return _users;
        }

        // helper methods

        private string GenerateJwtToken(User user)
        {
            _ = int.TryParse(_configuration["AppSettings:RefreshTokenValidityInDays"], out int tokenValidityInDays);
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["AppSettings:Secret"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UserId", user.Id.ToString()), new Claim("Username", user.Username.ToString()) }),
                Expires = DateTime.Now.AddDays(tokenValidityInDays),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            //var refreshToken = GenerateRefreshToken();

            //_ = int.TryParse(_configuration["AppSettings:TokenValidityInMinutes"], out int refreshTokenValidityInDays);

            //user.RefreshToken = refreshToken;
            //user.RefreshTokenExpiryTime = DateTime.Now.AddMinutes(refreshTokenValidityInDays);

            return tokenHandler.WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
