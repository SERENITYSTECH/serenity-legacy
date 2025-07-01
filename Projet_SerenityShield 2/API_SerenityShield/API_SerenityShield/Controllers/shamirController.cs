using API_SerenityShield.Models;
using API_SerenityShield.Models.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace API_SerenityShield.Controllers
{


    [Route("[controller]")]
    [ApiController]
    public class shamirController : ControllerBase
    {
        public object SSSRevolvingResultModel { get; private set; }

        [HttpPost]
        [Produces("application/json")]
        [Route("secret-sharing")]
  
        public IActionResult ShamirSecretSharing([FromBody] ShamirSecretSharingModel phrase)
        {
            ShamirSecretSharing malisteSeed =new ShamirSecretSharing();
            malisteSeed = malisteSeed.GetShamirKeys(phrase.Formulation);

            return Ok(malisteSeed);

        
        }
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ShamirSecretSharingRevolvingResultModel), StatusCodes.Status200OK)]
        [Route("secret-sharing-revolving")]
        [Authorize]
        public IActionResult ShamirRevolving([FromBody] ShamirSecretSharingRevolvingModel Keys)
        {

            //Methode pour récupere le token Bearer du header
            Request.Headers.TryGetValue("Authorization", out var headerValue);
            string headerValues = headerValue;
            string Token = string.Empty;
            if (headerValues != null)
            {
                string[] tab = headerValues.Split(' ');
                Token = tab[1];
            }

            //recupation IdUser du token 
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(Token);
            IEnumerable<Claim> claims = jwtSecurityToken.Claims;
            List<Claim> lclaims = claims.ToList();
            string PublicKey = lclaims[0].Value;
            //decryptage idUSER
            Cryptage cr = new Cryptage();
            string idUser = cr.DecryptHexa(lclaims[1].Value);
            User us = new User();
            bool existUser = us.IfExistUser(idUser);
            bool egale = false;
            ShamirSecretSharing malisteSeed = new ShamirSecretSharing();
            string maSeed = String.Empty;
            if (existUser==true)
            {
              //verify that publicKey exist and match with
                WalletConnection wac = new WalletConnection();
                us.ListPublicKey = wac.GetListPublicKeys(idUser);
                if (us.ListPublicKey.Count > 0)
                {

                    foreach (var item in us.ListPublicKey)
                    {
                        if (item == PublicKey)
                        {
                            egale = true;
                            break;
                        }
                    }
                    if (egale==true)
                    {
                       
                         maSeed = malisteSeed.GetShamirString(Keys.Key1, Keys.Key2);
                    }
                    else
                    {
                        IMessageError messageError = new IMessageError();
                        messageError.message = "There is no public Key matching.";
                        StatusCode(403, messageError);
                    }
                }
                else
                {
                    us = us.GetUserHeirById(idUser);
                    Heir h = new Heir();
                    h=h.GetHeirById(us.IdHeir);
                    if (h.PublicKey==PublicKey)
                    {
                        egale = true;
                     
                    }
                    if (egale == true)
                    {

                        maSeed = malisteSeed.GetShamirString(Keys.Key1, Keys.Key2);
                    }
                    else
                    {
                        IMessageError messageError = new IMessageError();
                        messageError.message = "There is no public Key matching.";
                        StatusCode(403, messageError);
                    }
                }
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "This User don't exist the idUser was changed before requesting.";
                return StatusCode(403,messageError);
            }

            ShamirSecretSharingRevolvingResultModel sssresult = new ShamirSecretSharingRevolvingResultModel();
            sssresult.viewingKey = maSeed;

            return Ok(sssresult);


        }

    }
}
