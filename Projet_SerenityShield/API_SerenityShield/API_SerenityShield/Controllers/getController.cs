using API_SerenityShield.Models;
using API_SerenityShield.Models.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Web.Http.Description;

namespace API_SerenityShield.Controllers
{

    [ApiController]
    public class getController : Controller
    {

        private readonly IJwtAuthenticationService _jwtAuthenticationService;
        private readonly IConfiguration _config;

        public getController(IJwtAuthenticationService JwtAuthenticationService, IConfiguration config)
        {
            _jwtAuthenticationService = JwtAuthenticationService;
            _config = config;
        }

        /// <summary>
        /// Select IUser
        /// </summary>
        /// <remarks>
        /// Just Need Bearer in HTTP REQUEST HEADER
        /// </remarks>
        /// <response code="200">Return IUser</response> 
        /// <response code="400">If the item is null</response> 
        /// <response code="500">Internal Error</response>  
        [HttpGet]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IUser), StatusCodes.Status200OK)]
        [Route("me")]
        [CustomAuthorizationFilter]

        public IActionResult Me()
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

            string returnValue = string.Empty;

            User user = new User();
            user = user.GetUserByPublicKey(PublicKey);
            IUser iuser = new IUser();
            if (user.Id != null)
            {
                iuser.id = user.Id;
                iuser.firstName = user.FirstName;
                iuser.lastName = user.LastName;
                iuser.email = user.Email;
                iuser.phone = user.PhoneNumber;
                iuser.idCard = user.IdCard;
                iuser.passport = user.Passsport;
                iuser.addedSecurity = user.AddedSecurity;
                iuser.subcriptionDate = user.SubcriptionDate;
                iuser.activate = user.IsActive;
                IPlan? ip = new IPlan();
                if (user.CurrentPaymentPlan.Id != null)
                {
                    ip.id = user.CurrentPaymentPlan.Id;
                    ip.label = user.CurrentPaymentPlan.Label;
                    ip.price = user.CurrentPaymentPlan.Price;
                    ip.heirsLimit = user.CurrentPaymentPlan.HeirsLimit;
                    ip.heirsStrongBoxLimit = user.CurrentPaymentPlan.HeirsStrongBoxLimit;
                    ip.personalStrongBoxLimit = user.CurrentPaymentPlan.PersonalStrongBoxLimit;
                    iuser.plan = ip;
                }
                else
                {
                    iuser.plan = null;
                }

            }
            else
            {

                Heir he = new Heir();
                he = he.GetHeirByPublicKey(PublicKey);
                iuser.id = he.IdHeir;
                iuser.firstName = he.FirstName;
                iuser.lastName = he.LastName;
                iuser.email = he.Email;
                iuser.phone = he.PhoneNumber;
            }
            return Ok(iuser);

        }

        /// <summary>
        /// Creates New User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     POST /me
        ///     {                
        ///       "publicKey": "000000",
        ///       "typeWallet": "METAMASK",
        ///       "idTypeWallet": "xxxxxx",
        ///       "phone": "+33645213254",
        ///       "email": "john.doe@test.com",
        ///       "firstName": "John",
        ///       "lastname": "Doe",
        ///       "idCard": "789456321",
        ///       "passport": "U32456",
        ///       "addedSecurity": "185059500036"       
        ///     }
        /// </remarks>
        /// <param name="newLoginModel"></param>
        /// <returns>return Bearer for verification code</returns>
        /// <response code="200">Returns the newly created User</response>
        /// <response code="400">If the user is not inserted</response>
        /// <response code="500">Internal Error</response>    
        [HttpPost]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IMessageError), StatusCodes.Status200OK)]
        [Route("me")]
        public IActionResult New_Customer([FromBody] NewUserModel newLoginModel)
        {
            //TO DO verification email before insert


            User user = new User();
            //verify if email or public key exist before insert
            bool existMail = user.IfExistEmail(newLoginModel.email);
            WalletConnection walletConnection = new WalletConnection();
            bool existPublicKey = walletConnection.IfExistWalletConnection(newLoginModel.publicKey);
            string res = string.Empty;
            IMessageError messageError = new IMessageError();
            if (existMail == true && existPublicKey == false)
            {
                messageError.message = " Your account cannot be created because an account already exists with this email address.";
                return StatusCode(403, messageError);

            }
            else if (existMail == false && existPublicKey == true)
            {
                messageError.message = "Your account cannot be created because an account already exists with this wallet public key";
                return StatusCode(403, messageError);
            }
            else if (existMail == true && existPublicKey == true)
            {
                messageError.message = "Your account cannot be created because an account already exists with this wallet public key and with this email address.";
                return StatusCode(403, messageError);
            }
            user.LastName = newLoginModel.lastName;
            user.FirstName = newLoginModel.firstName;
            user.Email = newLoginModel.email;
            user.PhoneNumber = newLoginModel.phone;
            user.PublicKey = newLoginModel.publicKey;
            user.IdCard = newLoginModel.idCard;
            user.Passsport = newLoginModel.passport;
            user.AddedSecurity = newLoginModel.addedSecurity;
            //generate random code for check
            RandomCode rc = new RandomCode();
            string codeEmail = rc.CreateRandomCode(4);
            string codeSms = rc.CreateRandomCode(4);


            SendEmail send = new SendEmail();
            send.SendMailCheckCode(user.Email, user.LastName, user.FirstName, codeEmail);
            //method to Send SMS

            send.SendSmsCheckCode(user.Email, user.LastName, user.FirstName, codeSms);
            // code to insert user
            res = user.InsertUserCustomer(user, codeEmail, codeSms);

            //code insert customer
            user.Id = res;
            Customer cus = new Customer();
            string result = cus.InsertCustomer(user);

            Cryptage cr = new Cryptage();
            string idWac = string.Empty;
            string idtypewallet = cr.DecryptHexa(newLoginModel.idTypeWallet);
            try
            {
                //code insert Wallet
                WalletConnection wac = new WalletConnection();
                idWac = wac.InsertWalletConnectionCustomer(user.Id, newLoginModel.publicKey, newLoginModel.typeWallet, idtypewallet);
            }
            catch (Exception e)
            {

                idWac = e.Message;
            }
            User us = new User();
            us = us.GetUserCustomerById(res);
            //insert first plan at 0

            string IdCustomer = us.GetIdCustomer(res);
            string idPlan = "4";
            // verification if exit a plan

            CustomerPlan cp = new CustomerPlan();
            string rs = cp.InsertCustomerPlan(IdCustomer, idPlan);
            Plan p = new Plan();
            p = p.GetPlanByID(idPlan);
            string pyear = (Convert.ToDouble(p.Price.Replace('.', ',')) * 12).ToString();
            PaymentHistory paymentHistory = new PaymentHistory();
            try
            {
                string idpayment = paymentHistory.InsertPaymentHistory(rs, p.Price, pyear, "$", newLoginModel.publicKey, "USDT", us);
            }
            catch (Exception e)
            {

                return StatusCode(403, e.Message);
            }

            JwtToken jwt = new JwtToken();

            if (us.Id != null)
            {
                var claims = new List<Claim>
                    {
                     new Claim(ClaimTypes.Authentication, newLoginModel.publicKey),
                     new Claim(ClaimTypes.NameIdentifier, us.Id),
                     new Claim(ClaimTypes.Name, us.LastName),
                     new Claim(ClaimTypes.GivenName, us.FirstName),
                     new Claim(ClaimTypes.Email, us.Email)
                    };

                jwt.access_token = _jwtAuthenticationService.GenerateToken(_config["Jwt:Key"], claims);

            }
            HistoriqueUser hu = new HistoriqueUser();
            string idUser = cr.DecryptHexa(us.Id);
            string currentId = hu.InsertHistoriqueUser(idUser, "1", "Login successful");
            return Ok(jwt);



        }

        /// <summary>
        /// Update User
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        ///     PATCH /me
        ///     {        
        ///        "phone": "+33645213254",
        ///        "email": "john.doe@test.com",
        ///        "firstName": "John",
        ///        "lastname": "Doe",
        ///        "idCard": "789456321",
        ///        "passport": "U32456",
        ///        "addedSecurity": "185059500036"        
        ///     }
        /// </remarks>
        /// <param name="model"></param>
        /// <returns>return IUser Updated </returns>
        /// <response code="200">Returns the user updated</response>
        /// <response code="400">If the user is not updated</response>
        /// <response code="500">Internal Error</response>    
        [HttpPatch]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IUser), StatusCodes.Status200OK)]
        [Route("me")]
        [Authorize]
        public IActionResult UpdateMyAccount([FromBody] MyAccountModel model)
        {
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

            User user = new User();

            user.Id = idUser;
            user.LastName = model.lastName;
            user.FirstName = model.firstName;
            user.Email = model.email;
            user.PhoneNumber = model.phone;
            user.IdCard = model.idCard;
            user.Passsport = model.passport;
            user.AddedSecurity = model.addedSecurity;

            string res = user.UpdateUser(user);
            Customer customer = new Customer();
            string result = customer.UpdateCustomer(user);
            user = user.GetUserCustomerById(user.Id);

            IUser iuser = new IUser();
            if (user != null)
            {
                iuser.id = user.Id;
                iuser.firstName = user.FirstName;
                iuser.lastName = user.LastName;
                iuser.email = user.Email;
                iuser.phone = user.PhoneNumber;
                iuser.idCard = user.IdCard;
                iuser.passport = user.Passsport;
                iuser.addedSecurity = user.AddedSecurity;
                iuser.subcriptionDate = user.SubcriptionDate;
                iuser.activate = user.IsActive;
                IPlan ip = new IPlan();
                ip.id = user.CurrentPaymentPlan.Id;
                ip.label = user.CurrentPaymentPlan.Label;
                ip.price = user.CurrentPaymentPlan.Price;
                ip.heirsLimit = user.CurrentPaymentPlan.HeirsLimit;
                ip.heirsStrongBoxLimit = user.CurrentPaymentPlan.HeirsStrongBoxLimit;
                ip.personalStrongBoxLimit = user.CurrentPaymentPlan.PersonalStrongBoxLimit;
                iuser.plan = ip;

            }
            //insert historique
            HistoriqueUser hu = new HistoriqueUser();
            string currentId = hu.InsertHistoriqueUser(idUser, "2", "Account updated");
            return Ok(iuser);

        }


        /// <summary>
        /// Resend Email Code
        /// </summary>
        /// <remarks>
        /// Just Need Bearer in HTTP REQUEST HEADER
        /// </remarks>
        /// <response code="200">return a boolean </response> 
        /// <response code="400">If mail is not send</response> 
        /// <response code="500">Internal Error</response>  

        [HttpPost]
        [Route("me/send-code-email")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]
        [Authorize]
        public IActionResult ResendPinEmail()
        {

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

            RandomCode rc = new RandomCode();
            string codeEmail = rc.CreateRandomCode(4);
            User user = new User();

            user = user.GetUserCustomerById(idUser);
            string res = user.UpdateCodeEmailUser(idUser, codeEmail);
            bool succes = false;
            //change to sms system
            try
            {
                SendEmail send = new SendEmail();
                send.SendMailCheckCode(user.Email, user.LastName, user.FirstName, codeEmail);
                succes = true;
            }
            catch (Exception)
            {

                succes = false;
            }
            CheckValueModel check = new CheckValueModel();
            check.success = succes;


            return Ok(check);
        }




        /// <summary>
        /// Resend Sms Code
        /// </summary>
        /// <remarks>
        /// Just Need Bearer in HTTP REQUEST HEADER
        /// </remarks>
        /// <response code="200">return a boolean </response> 
        /// <response code="400">If sms is not send</response> 
        /// <response code="500">Internal Error</response>  
        [HttpPost]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]
        [Route("me/send-code-mobile")]
        [Authorize]
        public IActionResult ReSendPinSms()
        {
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
            RandomCode rc = new RandomCode();
            string codeSms = rc.CreateRandomCode(4);
            User user = new User();

            user = user.GetUserCustomerById(idUser);
            string res = user.UpdateCodeSmsUser(idUser, codeSms);

            bool succes = false;
            //change to sms system
            try
            {
                SendEmail send = new SendEmail();
                send.SendSmsCheckCode(user.Email, user.LastName, user.FirstName, codeSms);
                succes = true;
            }
            catch (Exception)
            {

                succes = false;
            }
            CheckValueModel check = new CheckValueModel();
            check.success = succes;


            return Ok(check);
        }
        /// <summary>
        /// Desactive User
        /// </summary>
        /// <remarks>
        /// Just Need Bearer in HTTP REQUEST HEADER
        /// </remarks>
        /// <response code="200">return a boolean </response> 
        /// <response code="403">If a verication is false</response> 
        /// <response code="500">Internal Error</response>  
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]
        [Route("me/desactive")]
        [Authorize]
        public IActionResult DesactiveUser()
        {
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
            bool succes = false;
            bool succesInactiveUser = false;
            bool successInactiveWallet = false;
            WalletConnection wac = new WalletConnection();
            List<WalletConnection> lwac = new List<WalletConnection>();
            try
            {
                string res = us.InactiveUser(idUser);
                if (res == "1")
                {
                    succesInactiveUser = true;

                }
                else
                {
                    succesInactiveUser = false;

                }

                //double verif

                lwac = wac.GetListWalletConnection(idUser);
                foreach (var item in lwac)
                {
                    string idWalletConnection = cr.DecryptHexa(item.IdWalletConnection);
                    string result = item.RemoveWalletConnection(idWalletConnection);
                    if (result == "1")
                    {
                        successInactiveWallet = true;
                    }
                    else
                    {
                        successInactiveWallet = false;
                        break;
                    }
                }
                if (succesInactiveUser == true && successInactiveWallet == true)
                {
                    succes = true;
                }
                else
                {
                    succes = false;
                }
            }
            catch (Exception)
            {

                succes = false;
            }
            CheckValueModel check = new CheckValueModel();
            check.success = succes;


            return Ok(check);
        }

        /// <summary>
        /// check if exist wallet secret , if no insert it 
        /// </summary>
        /// <remarks>
        /// Need Bearer in HTTP REQUEST HEADER and secret public key
        /// </remarks>
        /// <response code="200">return a boolean </response> 
        /// <response code="400">If mail is not send</response> 
        /// <response code="500">Internal Error</response>  

        [HttpPost]
        [Route("me/wallet/secret")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]
        [Authorize]
        public IActionResult InsertSecretPK(SecretPublicKey model)
        {

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

            //verif if exist secretPK with idUse
            WalletConnection wac = new WalletConnection();
            bool existSecretPk = wac.IfExistWalletConnectionbySolanaAndSecret(idUser, PublicKey, model.secretPublicKey);

            bool succes = false;
            if (existSecretPk == true)
            {
                succes = true;
            }
            else
            {
                try
                {
                    string res = wac.UpdateSecretPublicKeyForWalletConnection(idUser, model.secretPublicKey);
                    succes = true;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            CheckValueModel check = new CheckValueModel();
            check.success = succes;


            return Ok(check);
        }
        [HttpPost]
        [Route("me/wallet/metamask")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]
        [Authorize]
        public IActionResult InsertMetamaskPkAndChainID(MetamaskModel model)
        {

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

            //verif if exist secretPK with idUse
            WalletConnection wac = new WalletConnection();
            bool existSecretPk = wac.IfExistWalletConnectionbySolanaAndMetamaskAndCHAINID(idUser, PublicKey, model.metamaskPublicKey, model.chainId);

            bool succes = false;
            if (existSecretPk == true)
            {
                succes = true;
            }
            else
            {
                try
                {
                    string res = wac.UpdateMetamaskPublicKeyForWalletConnection(idUser, model.metamaskPublicKey, model.chainId);
                    succes = true;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            CheckValueModel check = new CheckValueModel();
            check.success = succes;


            return Ok(check);
        }

        /// <summary>
        /// check if exist wallet metamask 
        /// </summary>
        /// <remarks>
        /// Need Bearer in HTTP REQUEST HEADER and metamask public key
        /// </remarks>
        /// <response code="200">return a boolean </response> 
        /// <response code="400">If mail is not send</response> 
        /// <response code="500">Internal Error</response>  

        [HttpPost]
        [Route("me/wallet/exist-secret-wallet")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]

        public IActionResult ExistSecretWallet(ExistSecretPublicKey model)
        {


            Cryptage cr = new Cryptage();
            //verif if exist secretPK with idUse
            WalletConnection wac = new WalletConnection();
            bool existSecretPk = wac.IfExistWalletConnectionbySolanaAndSecret(cr.DecryptHexa(model.idUser), model.solanaPublicKey, model.secretPublicKey);

            bool succes = false;
            if (existSecretPk == true)
            {
                succes = true;
            }
            else
            {
                succes = false;
            }
            CheckValueModel check = new CheckValueModel();
            check.success = succes;


            return Ok(check);
        }




        /// <summary>
        /// check if exist wallet metamask 
        /// </summary>
        /// <remarks>
    
        /// </remarks>
        /// <response code="200">return a boolean </response> 
        /// <response code="400">If mail is not send</response> 
        /// <response code="500">Internal Error</response>  

        [HttpPost]
        [Route("me/wallet/exist-metamask-wallet")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]

        public IActionResult ExistMetamaskWallet(ExistMetamaskPublicKey model)
        {


            Cryptage cr = new Cryptage();
            //verif if exist secretPK with idUse
            WalletConnection wac = new WalletConnection();
            bool existSecretPk = wac.IfExistWalletConnectionbySolanaAndMetamaskAndCHAINID(cr.DecryptHexa(model.idUser), model.solanaPublicKey, model.metamaskPublicKey,model.chainId);

            bool succes = false;
            if (existSecretPk == true)
            {
                succes = true;
            }
            else
            {
                succes = false;
            }
            CheckValueModel check = new CheckValueModel();
            check.success = succes;


            return Ok(check);
        }

        [HttpGet]
        [Route("me/wallet/metamask")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(WalletConnection), StatusCodes.Status200OK)]
        [Authorize]
        public IActionResult GetMetamaskPkAndChainID()
        {

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

         
            WalletConnection wac = new WalletConnection();
            wac = wac.GetWalletConnectionByIdUser(idUser);

           


            return Ok(wac);
        }

    }
}

