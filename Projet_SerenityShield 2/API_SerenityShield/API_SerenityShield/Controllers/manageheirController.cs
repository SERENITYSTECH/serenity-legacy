using API_SerenityShield.Models;
using API_SerenityShield.Models.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace API_SerenityShield.Controllers
{

    [ApiController]
    public class manageheirController : ControllerBase
    {

        /// <summary>
        /// Select IHeir
        /// </summary>
        /// <remarks>
        /// param idHeir
        /// </remarks>
        /// <response code="200">Return IHeir</response> 
        /// <response code="400">If the item is null</response> 
        [HttpGet]
        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IHeir), StatusCodes.Status200OK)]
        [Route("heir/{id}")]
        [CustomAuthorizationFilter]
        public IActionResult GetHeir(string id)
        {
          
            Cryptage cr = new Cryptage();

            Heir h=new Heir();  
            id=cr.DecryptHexa(id);   
            h = h.GetHeirById(id);
            IHeir ih = new IHeir();
            ih.idUser=h.IdUser;
            ih.idHeir = h.IdHeir;
            ih.lastName=h.LastName;
            ih.firstName=h.FirstName;   
            ih.email=h.Email;
            ih.phone = h.PhoneNumber;
            ih.creation_Date = h.Creation_Date;
            ih.publicKey=h.PublicKey;
            TypeWallet tw = new TypeWallet();
            tw.id = h.idTypeWallet;
            tw.label = h.walletType.label;
            
            ih.walletType = tw;
            return Ok(ih);

        }


        /// <summary>
        /// Select ListIHeir
        /// </summary>
        /// <remarks>
        /// Just Need Bearer in HTTP REQUEST HEADER
        /// </remarks>
        /// <response code="200">Return List IHeir</response> 
        /// <response code="400">If the item is null</response> 
        /// <response code="401">If there is not Bearer</response> 
        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<IHeir>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("heir")]
        [CustomAuthorizationFilter]
        public IActionResult GetListHeir()
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
            string idUserCustomer = cr.DecryptHexa(lclaims[1].Value);
         


            List<Heir> lh = new List<Heir>();
            Heir h=new Heir();
            lh = h.GetListHeirByListIdUserCustomer(idUserCustomer);
            List<IHeir> list = new List<IHeir>();
            foreach (Heir he in lh)
            { 
            IHeir heir = new   IHeir();
                heir.idUser = he.IdUser;
                heir.idHeir = he.IdHeir;   
                heir.lastName = he.LastName;
                heir.firstName = he.FirstName;
                heir.phone = he.PhoneNumber;
                heir.email = he.Email;
                heir.publicKey = he.PublicKey;
                heir.walletType = he.walletType;
                heir.creation_Date = he.Creation_Date;
                list.Add(heir);
            }
          
            return Ok(list);

        }



        /// <summary>
        /// Insert New Heir
        /// </summary>
        /// <remarks>
        ///  Need Bearer in HTTP REQUEST HEADER an Heir 's params
        /// </remarks>
        /// <response code="200">Return  IHeir</response> 
        /// <response code="400">If the item is null</response> 
        /// <response code="401">If there is not Bearer</response> 
        [ProducesResponseType(201)]
        [Produces("application/json")]
        [ProducesResponseType(typeof (IHeir), StatusCodes.Status201Created)]
        [HttpPost]
        [Route("heir")]
        [CustomAuthorizationFilter]
        public IActionResult New_Heir([FromBody] NewHeirModel model)
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
            string idUserCustomer = cr.DecryptHexa(lclaims[1].Value);
            User UserCustomer=new User();
            UserCustomer = UserCustomer.GetUserCustomerById(idUserCustomer);
            string idCustomer=cr.DecryptHexa(UserCustomer.IdCustomer);

            User userHeir = new User();
            userHeir.LastName = model.LastName;
            userHeir.FirstName = model.FirstName;
            userHeir.Email = model.Email;
            userHeir.PhoneNumber = model.Phone;
            userHeir.PublicKey = model.PublicKey;
           
            // code to insert user
            string idUserHeir = userHeir.InsertUserHeir(userHeir);

            //code insert Heir
            // userHeir=userHeir.GetUserById(idUserHeir);
      

            userHeir.Id = idUserHeir;
                Heir h = new Heir();
            string result = h.InsertHeir(userHeir, model.PublicKey,cr.DecryptHexa(model.idTypeWallet));
            h=h.GetHeirById(result);
            string idHeir = cr.DecryptHexa(h.IdHeir);
            //insert affiliate
            Affiliate af=new Affiliate();
           
            string r = af.InsertAffiliate(idCustomer, idHeir, idUserCustomer, idUserHeir, model.PublicKey);
            string idWac = string.Empty;
            try
            {
                //code insert Wallet
                WalletConnection wac = new WalletConnection();
                idWac = wac.InsertWalletConnectionHeir(idUserHeir, model.PublicKey, idUserCustomer, cr.DecryptHexa(model.idTypeWallet));
            }
            catch (Exception e)
            {

                idWac = e.Message;
            }

            h = h.GetHeirById(result);
            IHeir ih = new IHeir();
            ih.idUser = h.IdUser;
            ih.idHeir = h.IdHeir;
            ih.lastName = h.LastName;
            ih.firstName = h.FirstName;
            ih.email = h.Email;
            ih.phone = h.PhoneNumber;
            ih.creation_Date = h.Creation_Date;
            ih.publicKey = h.PublicKey;
            TypeWallet tw = new TypeWallet();
            tw.id = h.idTypeWallet;
            tw.label = h.walletType.label;
            tw.type = h.walletType.type;
            ih.walletType = tw;

            //insert historique
            HistoriqueUser hu = new HistoriqueUser();
            string currentId = hu.InsertHistoriqueUser(idUserCustomer, "11", "Creation of heir account: "+ h.LastName + " "+ h.FirstName + "");


            return StatusCode(201, ih);

        }




        /// <summary>
        /// Update  Heir
        /// </summary>
        /// <remarks>
        ///  Need Bearer in HTTP REQUEST HEADER an Heir 's params
        /// </remarks>
        /// <response code="200">Return  IHeir</response> 
        /// <response code="400">If the item is null</response> 
        /// <response code="401">If there is not Bearer</response> 
        [ProducesResponseType(201)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IHeir), StatusCodes.Status201Created)]
        [HttpPatch]
        [Route("heir")]
        [CustomAuthorizationFilter]
        public IActionResult UpdateHeirAccount([FromBody] UpdateHeirModel model)
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
            string idUserCustomer = cr.DecryptHexa(lclaims[1].Value);

            Heir h = new Heir();
            User user = new User();
            string idHeir = cr.DecryptHexa(model.IdHeir);
            h = h.GetHeirById(idHeir);
            string idUserHeir = cr.DecryptHexa(h.IdUser);
            user.Id = idUserHeir;
            user.IdHeir = idHeir;
            user.LastName = model.LastName;
            user.FirstName = model.FirstName;
            user.Email = model.Email;
            user.PhoneNumber = model.Phone;
            //compare publicKey
            if (h.PublicKey==model.PublicKey)
            {
                string res = user.UpdateUser(user);

                string result2 = h.UpdateHeir(user, model.PublicKey,cr.DecryptHexa(model.idTypeWallet));
            }
            else
            {
                //update connectionWallet
                WalletConnection wac = new WalletConnection();
                wac = wac.GetWalletConnectionByPublicKey(h.PublicKey);
                string idWac = cr.DecryptHexa(wac.IdWalletConnection);
                string result = wac.UpdateWalletConnectionHeir(idWac, model.PublicKey);
                //update affiliate
                Affiliate af=new Affiliate();
                string rsa = af.UpdateAffiliate(idUserCustomer, idHeir, model.PublicKey);
                //update user
                string res = user.UpdateUser(user);
                //update Heir
                string result2 = h.UpdateHeir(user, model.PublicKey, cr.DecryptHexa(model.idTypeWallet));
              
            }

            h = h.GetHeirById(idHeir);
;
            IHeir ih = new IHeir();
            ih.idUser = h.IdUser;
            ih.idHeir = h.IdHeir;
            ih.lastName = h.LastName;
            ih.firstName = h.FirstName;
            ih.email = h.Email;
            ih.phone = h.PhoneNumber;
            ih.creation_Date = h.Creation_Date;
            ih.walletType = h.walletType;
            ih.publicKey = h.PublicKey;

            //insert historique
            HistoriqueUser hu = new HistoriqueUser();
            string currentId = hu.InsertHistoriqueUser(idUserCustomer, "12", "updating of the heir account: " + h.LastName + " " + h.FirstName + "");


            return StatusCode(201,ih); 


        }


        /// <summary>
        /// Get Boolean to verify deleting.
        /// </summary>
        /// <remarks>
        /// Need  Bearer Token and idHeir
        /// </remarks>
        /// <response code="200">Return Boolean</response> 
        /// <response code="400">If the item is null</response> 
        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]
        [HttpPost]
        [Route("heir-delete")]
        [CustomAuthorizationFilter]
        public IActionResult DeleteHeirAccount([FromBody] HeirModel model)
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

            User user = new User();
    
            Heir h = new Heir();
            h = h.GetHeirById(cr.DecryptHexa(model.idHeir));
           string idHeir= cr.DecryptHexa(h.IdHeir);
            string idUserHeir= cr.DecryptHexa(h.IdUser);

            //update walletConnection
            WalletConnection wac = new WalletConnection();
            wac = wac.GetWalletConnectionByPublicKey(h.PublicKey);
            string result = wac.RemoveWalletConnection(wac.IdWalletConnection);
            //update affiliate
            Affiliate af = new Affiliate();
            string rsa = af.RemoveAffiliate(idUser, idHeir);
            //update user
            string resu = user.RemoveHeirStatus(idUserHeir);
            // update heir
            string res = h.RemoveHeir(idUserHeir);

            CheckValueModel ck = new CheckValueModel();
            if (res == "1")
            {
                ck.success = true;
                //insert historique
                HistoriqueUser hu = new HistoriqueUser();
                string currentId = hu.InsertHistoriqueUser(idUser, "13", "Deleting of the heir account: " + h.LastName + " " + h.FirstName + "");
            }
            else
            {
                ck.success = false;
            }
            return StatusCode(201, ck);


        }
    }
}
