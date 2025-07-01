using API_SerenityShield.Models;
using API_SerenityShield.Models.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SecretSharingDotNet.Cryptography;
using SecretSharingDotNet.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Cors;
using System.Text;

namespace API_SerenityShield.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly IJwtAuthenticationService _jwtAuthenticationService;
        private readonly IConfiguration _config;

        public authController(IJwtAuthenticationService JwtAuthenticationService, IConfiguration config)
        {
            _jwtAuthenticationService = JwtAuthenticationService;
            _config = config;
        }

        static string BytesToStringConversion(byte[] bytes)
        {
            using (MemoryStream Stream = new MemoryStream(bytes))
            {
                using (StreamReader streamReader = new StreamReader(Stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }
        public static byte[] IntArrayToByteArray(int[] ints)
        {

            List<byte> bytes = new List<byte>(ints.GetUpperBound(0) * sizeof(byte));

            foreach (int integer in ints)
            {

                bytes.Add(BitConverter.GetBytes(integer)[0]);

            }

            return bytes.ToArray();

        }
        /// <summary>
        /// Get Bearer Token From Login
        /// </summary>
        /// <remarks>
        /// Need publicKey code and int[] (int array)
        /// </remarks>
        /// <response code="200">Return Bearer token or empty Json</response> 
        /// <response code="400">If the item is null</response> 
        /// <response code="500">Internal Error</response>  

        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(JwtToken), StatusCodes.Status200OK)]
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] LoginModelCheckCode model)
        {
            Cryptage   cryp=new Cryptage();

            //verify is authorized id user
            WalletConnection walletConnection = new WalletConnection();
            walletConnection = walletConnection.GetWalletConnectionByPublicKey(model.PublicKey);
            bool existinBaseandAuthorized = false;
            if (walletConnection.IdWalletConnection!=null)
            {
                string iduser = cryp.DecryptHexa(walletConnection.IdUser);
                if (Convert.ToInt16(iduser) < 210)
                {
                    existinBaseandAuthorized = true;
                }
            }
         
            //get list public Key allowed 
            AuthorizedPK apk = new AuthorizedPK();
            string[] tabapk = apk.ListAllowedPK;
            bool existinLIste = false;
            if (tabapk.ToList().Contains(model.PublicKey))
            {
                existinLIste = true;
            }

            CheckPK ck = new CheckPK();
            ck.authorized = existinLIste;

            if (existinLIste == true || existinBaseandAuthorized==true)
            {


                JwtToken jwt = new JwtToken();
                //session like asp.net get
                string? sessionData = HttpContext.Session.GetString("PublicKey");


                bool exist = true;
                if (exist == true)//à rajouter le booleen de la verif du hash
                {
                    var user = _jwtAuthenticationService.Authenticate(model.PublicKey);
                    if (user.Id != null)
                    {
                        var claims = new List<Claim>
                     {
                     new Claim(ClaimTypes.Authentication, model.PublicKey),
                     new Claim(ClaimTypes.NameIdentifier, user.Id),
                     new Claim(ClaimTypes.Name, user.LastName),
                     new Claim(ClaimTypes.GivenName, user.FirstName),
                     new Claim(ClaimTypes.Email, user.Email)
                     };
                        //implementer la logique pour retrouver le profil user(user+strongboxperso+strongboxForHeir+strongboxHeir+historiqueUser+ListHeir)
                        var token = _jwtAuthenticationService.GenerateToken(_config["Jwt:Key"], claims);

                        jwt.access_token = token;

                        HistoriqueUser hu = new HistoriqueUser();
                        Cryptage cr = new Cryptage();
                        string idUser = cr.DecryptHexa(user.Id);
                        string currentId = hu.InsertHistoriqueUser(idUser, "1", "Login successful");
                    }

                    else
                    {
                        var heir = _jwtAuthenticationService.AuthenticateHeir(model.PublicKey);
                        if (heir.IdHeir != null)
                        {
                            var claims = new List<Claim>
                     {
                     new Claim(ClaimTypes.Authentication, model.PublicKey),
                     new Claim(ClaimTypes.NameIdentifier, heir.IdHeir),
                     new Claim(ClaimTypes.Name, heir.LastName),
                     new Claim(ClaimTypes.GivenName, heir.FirstName),
                     new Claim(ClaimTypes.Email, heir.Email)
                     };
                            //implementer la logique pour retrouver le profil user(user+strongboxperso+strongboxForHeir+strongboxHeir+historiqueUser+ListHeir)
                            var token = _jwtAuthenticationService.GenerateToken(_config["Jwt:Key"], claims);

                            jwt.access_token = token;

                            HistoriqueUser hu = new HistoriqueUser();
                            Cryptage cr = new Cryptage();
                            string idHeir = cr.DecryptHexa(heir.IdHeir);
                            string currentId = hu.InsertHistoriqueUser(idHeir, "1", "Login successful (as Heir)");
                        }
                        else
                        {
                            jwt.access_token = "{}";
                        }

                    }


                }
                return Ok(jwt);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "This User is not allowed to Login.";
                return StatusCode(401, messageError);
            }
        }
    


        /// <summary>
        /// Get Boolean to allowed publicKey.
        /// </summary>
        /// <remarks>
       
        /// </remarks>
        /// <response code="200">Return Boolean</response> 
        /// <response code="400">If the item is null</response> 
        /// <response code="500">Internal Error</response>  

        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckPK), StatusCodes.Status200OK)]
        [HttpPost]
        [Route("autorized")]
    
        public IActionResult VerifyAllowedPublicKeyl([FromBody] AllowedPK Code)
        {
            //get list public Key allowed 
            AuthorizedPK apk =new AuthorizedPK();
            string[] tabapk = apk.ListAllowedPK;
            bool exist = false;
            if (tabapk.ToList().Contains(Code.publicKey))
            {
                exist = true;
            }

            CheckPK ck = new CheckPK();
            ck.authorized = exist;


            return Ok(ck);
        }

        /// <summary>
        /// Get Boolean to verify code.
        /// </summary>
        /// <remarks>
        /// Need  need Bearer Token and code sended
        /// </remarks>
        /// <response code="200">Return Boolean</response> 
        /// <response code="400">If the item is null</response> 
        /// <response code="500">Internal Error</response>  

        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]
        [HttpPost]
        [Route("verify-code-email")]
        [Authorize]
        public IActionResult VerifyCodeEmail([FromBody] NewLoginModelCheckCode Code)
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
            bool exist = us.IfExistCodeEmail(Code.Code, idUser);
            CheckValueModel ck = new CheckValueModel();
            ck.success = exist;


            return Ok(ck);
        }

        /// <summary>
        /// Get Boolean to verify code.
        /// </summary>
        /// <remarks>
        /// Need  need Bearer Token and code sended
        /// </remarks>
        /// <response code="200">Return Boolean</response> 
        /// <response code="400">If the item is null</response> 
        /// <response code="500">Internal Error</response>  
        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]
        [HttpPost]
        [Route("verify-code-sms")]
        [Authorize]
        public IActionResult VerifyCodeSms([FromBody] NewLoginModelCheckCode Code)
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
            bool exist = us.IfExistCodeSMS(Code.Code, idUser);
            CheckValueModel ck = new CheckValueModel();
            ck.success = exist;

            return Ok(ck);
        }


        /// <summary>
        /// Get Boolean to verify code.
        /// </summary>
        /// <remarks>
        /// Need  need Bearer Token (after verify sms code and Email code from front)
        /// </remarks>
        /// <response code="200">Return Boolean</response> 
        /// <response code="400">If the item is null</response> 
        /// <response code="500">Internal Error</response>  
        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]
        [HttpPost]
        [Route("activate-after-verification")]
        public IActionResult ActivateUserAfterVerification()
        {  //Methode pour récupere le token Bearer du header
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
            us = us.GetUserByPublicKey(PublicKey);

            string IdUser = cr.DecryptHexa(us.Id);
            string res = us.UpdateVerificationUser(IdUser);

            CheckValueModel ck = new CheckValueModel();
            if (res == "1")
            {
                ck.success = true;

            }
            else
            {
                ck.success = false;
            }


            return Ok(ck);
        }
    }


}
