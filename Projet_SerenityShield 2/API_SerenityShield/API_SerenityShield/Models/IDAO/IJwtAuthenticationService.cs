
using API_SerenityShield.Models;
using API_SerenityShield.Models.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;



namespace API_SerenityShield
{
    public interface IJwtAuthenticationService
    {

        User Authenticate(string publickey);
        Heir AuthenticateHeir(string publickey);
        string GenerateToken(string secret, List<Claim> claims);

    }
}
