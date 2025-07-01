using API_SerenityShield.Models;
using API_SerenityShield.Models.DAO;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API_SerenityShield
{
    public class JwtAuthenticationService:IJwtAuthenticationService
    {
   
 
        public User  Authenticate(string publickey)
            
        {

            bool ok = false;
            User us = new User();
            us = us.GetUserByPublicKey(publickey);
     
            return (us);
        }

        public Heir AuthenticateHeir(string publickey)

        {

            bool ok = false;
            Heir he = new Heir();
            he = he.GetHeirByPublicKey(publickey);

            return (he);
        }

        public string GenerateToken(string secret, List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(1200),
                SigningCredentials = new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256Signature)

            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
