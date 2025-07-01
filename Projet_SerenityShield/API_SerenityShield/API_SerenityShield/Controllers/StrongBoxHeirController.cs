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
using static API_SerenityShield.Models.DAO.StrongboxForHeirInsertModel;

namespace API_SerenityShield.Controllers
{

    [ApiController]
    public class StrongBoxHeirController : ControllerBase
    {

        /// <summary>
        /// insert a  strongbox for Heir.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>insert a  strongbox for Heir.</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPost]
        [Route("strongbox-for-heir")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IStrongboxForHeirs), 201)]
        [Authorize]
        public IActionResult InsertStrongoxForHeir([FromBody] StrongboxForHeirInsertModel info)
        {
            string rs = string.Empty;

            string idUser = string.Empty;
            CustomerPlan cp = new CustomerPlan();
            CheckValueModel ck = new CheckValueModel();
            try
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
                idUser = cr.DecryptHexa(lclaims[1].Value);


                User us = new User();
                us = us.GetUserById(idUser);
                string IdCustomer = us.GetIdCustomer(idUser);
                IStrongboxForHeirs f = new IStrongboxForHeirs();


                try
                         

                {
                    string listIdHeir = String.Empty;
                 
                        foreach (var item in info.heirs)
                        {
                            listIdHeir += item.idHeir + ",";
                        }

                    listIdHeir = listIdHeir.Substring(0, listIdHeir.Length - 1);

                    rs = f.InsertStrongboxForHeir(IdCustomer,cr.DecryptHexa(info.idInactivePeriod),info.label,info.messageForHeirs,info.display, listIdHeir
                        , PublicKey, info.secretPK, info.payingPK,info.solanaPK
                        , info.solSereNftPK, info.solUsrNftPK,info.solHeirNftPKs,info.scPK, info.codeID, info.prepayedInherence);


                    List<IWallet> listwallet = new List<IWallet>();
                    listwallet = info.content;
                    if (rs != "-1")
                    {


                        foreach (IWallet item in listwallet)
                        {
                            switch (item.type.ToLower())
                            {
                                case "wallet dex":
                                    IStrongboxContentWalletDex dex = new IStrongboxContentWalletDex();
                                    string result = string.Empty;
                         
                                        result = dex.InsertStrongboxContentWalletDex(false, rs, item.label, cr.DecryptHexa(item.idTypeWallet), item.seed, item.provider);

                                    break;

                                case "wallet cex":

                                    IStrongboxContentWalletCex cex = new IStrongboxContentWalletCex();
                                    string resultcex = string.Empty;
                                    {
                               
                                            resultcex = cex.InsertStrongboxContentWalletCex(false, rs, item.label, cr.DecryptHexa(item.idTypeWallet), item.provider);

                                
                                    }
                                    break;
                                case "wallet desktop":
                                    IStrongboxContentWalletDesktop des = new IStrongboxContentWalletDesktop();
                                    string resultdes = string.Empty;

                                        resultdes = des.InsertStrongboxContentWalletDesktop(false, rs, item.label, cr.DecryptHexa(item.idTypeWallet), item.provider);

                                    break;
                                case "wallet mobile":
                                    IStrongboxContentWalletMobile mob = new IStrongboxContentWalletMobile();
                                    string resultmob = string.Empty;

                                        resultmob = mob.InsertStrongboxContentWalletMobile(false, rs, item.label, cr.DecryptHexa(item.idTypeWallet), item.provider);


                                    break;
                                case "wallet hardware":
                                    IStrongboxContentWalletHardware hard = new IStrongboxContentWalletHardware();

                                    string resulthard = string.Empty;

                                    resulthard = hard.InsertStrongboxContentWalletHardware(false, rs, item.label, cr.DecryptHexa(item.idTypeWallet), item.provider);

                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
                catch (Exception e)
                {

                    return StatusCode(403, e.Message);
                }



            }
            catch (Exception e)
            {
                if (e.Message != null)
                {
                    rs = (e.Message);

                }

            }

            if (rs != "-1")
            {
                ck.success = true;
                HistoriqueUser hu = new HistoriqueUser();
                string currentId = hu.InsertHistoriqueUser(idUser, "6", "add Heirs Strongbox");
             
                return StatusCode(201, ck);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "Insert Failed";
                return StatusCode(403, messageError);
            }

        }
    
              

           

          

        
        /// <summary>
        /// update a strongbox-for-heir.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>updtate a personnal strongbox</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPatch]
        [Route("strongbox-for-heir")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), 201)]
        [Authorize]
        public IActionResult UpdateStrongboxForHeir([FromBody] StrongboxForHeirUpdateModel info)
        {
            string rs = string.Empty;

            string idUser = string.Empty;
            
            CheckValueModel ck = new CheckValueModel();
            try
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
                idUser = cr.DecryptHexa(lclaims[1].Value);


                User us = new User();
                us = us.GetUserById(idUser);
                string IdCustomer = us.GetIdCustomer(idUser);
                IStrongboxForHeirs per = new IStrongboxForHeirs();

                try
                {
                    //get all list of content
                  
                    string idStrongbox = cr.DecryptHexa(info.id);
                    per = per.GetStrongboxForHeirById(idStrongbox);

                    if (per.content != null)
                    {


                        if (per.content.walletDex.Count > 0)
                        {
                            foreach (var item in per.content.walletDex)
                            {
                                item.DesactiveStrongboxContentWalletDex(cr.DecryptHexa(item.id));
                            }
                        }
                        if (per.content.walletDex.Count > 0)
                        {
                            foreach (var item in per.content.walletCex)
                            {
                                item.DesactiveStrongboxContentWalletCex(cr.DecryptHexa(item.id));
                            }
                        }
                        if (per.content.walletMobile.Count > 0)
                        {
                            foreach (var item in per.content.walletMobile)
                            {
                                item.DesactiveStrongboxContentWalletMobile(cr.DecryptHexa(item.id));
                            }
                        }
                        if (per.content.walletDesktop.Count > 0)
                        {
                            foreach (var item in per.content.walletDesktop)
                            {
                                item.DesactiveStrongboxContentWalletDesktop(cr.DecryptHexa(item.id));
                            }
                        }
                        if (per.content.walletHardware.Count > 0)
                        {
                            foreach (var item in per.content.walletHardware)
                            {
                                item.DesactiveStrongboxContentWalletHardware(cr.DecryptHexa(item.id));
                            }
                        }
                    }
                    List<IWallet> listwallet = new List<IWallet>();
                    listwallet = info.content;
               

                        foreach (IWallet item in listwallet)
                        {
                            switch (item.type.ToLower())
                            {
                                case "wallet dex":
                                    IStrongboxContentWalletDex dex = new IStrongboxContentWalletDex();
                                    string result = string.Empty;

                                    result = dex.InsertStrongboxContentWalletDex(false, idStrongbox, item.label, cr.DecryptHexa(item.idTypeWallet), item.seed, item.provider);


                                    break;

                                case "wallet cex":

                                    IStrongboxContentWalletCex cex = new IStrongboxContentWalletCex();
                                    string resultcex = string.Empty;
                                    {

                                        resultcex = cex.InsertStrongboxContentWalletCex(false, idStrongbox, item.label, cr.DecryptHexa(item.idTypeWallet), item.provider);


                                    }
                                    break;
                                case "wallet desktop":
                                    IStrongboxContentWalletDesktop des = new IStrongboxContentWalletDesktop();
                                    string resultdes = string.Empty;


                                    resultdes = des.InsertStrongboxContentWalletDesktop(false, idStrongbox, item.label, cr.DecryptHexa(item.idTypeWallet), item.provider);



                                    break;
                                case "wallet mobile":
                                    IStrongboxContentWalletMobile mob = new IStrongboxContentWalletMobile();
                                    string resultmob = string.Empty;

                                    resultmob = mob.InsertStrongboxContentWalletMobile(false, idStrongbox, item.label, cr.DecryptHexa(item.idTypeWallet), item.provider);


                                    break;
                                case "wallet hardware":
                                    IStrongboxContentWalletHardware hard = new IStrongboxContentWalletHardware();

                                    string resulthard = string.Empty;

                                    resulthard = hard.InsertStrongboxContentWalletHardware(false, idStrongbox, item.label, cr.DecryptHexa(item.idTypeWallet), item.provider);

                                    break;
                                default:
                                    break;
                            }
                        }
                    
                    string listIdHeir = String.Empty;
                    foreach (var item in info.heirs)
                    {
                        listIdHeir+=item.idHeir+",";
                    }
                    listIdHeir = listIdHeir.Substring(0, listIdHeir.Length-1);

                    rs = per.UpdateStrongboxForHeir(cr.DecryptHexa(info.idInactivePeriod),
                      idStrongbox, info.label, listIdHeir, info.messageForHeirs, info.display, info.prepayedInherence,info.walletPublicKeyOwner,info.secretPK,
                      info.payingPK,info.solSereNftPK,info.solUsrNftPK,info.solHeirNftPKs,info.solanaPK,info.scPK,info.codeID);
                }
                catch (Exception e)
                {

                    return StatusCode(403, e.Message);
                }



            }
            catch (Exception e)
            {
                if (e.Message != null)
                {
                    rs = (e.Message);

                }

            }

            if (rs != "-1")
            {
                ck.success = true;
                HistoriqueUser hu = new HistoriqueUser();
                string currentId = hu.InsertHistoriqueUser(idUser, "7", "update Strongbox Heirs");
               

                return StatusCode(201, ck);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "Update Failed";
                return StatusCode(403, messageError);
            }

        }
        /// <summary>
        /// delete a personnal strongbox.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>updtate a personnal strongbox</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpDelete]
        [Route("strongbox-for-heir/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), 201)]
        [Authorize]
        public IActionResult DeletePersonnalStrongbox(string id)
        {
            string rs = string.Empty;

            string idUser = string.Empty;
            CustomerPlan cp = new CustomerPlan();
            CheckValueModel ck = new CheckValueModel();
            try
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
                idUser = cr.DecryptHexa(lclaims[1].Value);


                User us = new User();
                us = us.GetUserById(idUser);
                string IdCustomer = us.GetIdCustomer(idUser);
                IStrongboxForHeirs per = new IStrongboxForHeirs();

                try
                {
                    rs = per.DesactiveStrongboxForHeir(cr.DecryptHexa(id));
                }
                catch (Exception e)
                {

                    return StatusCode(403, e.Message);
                }



            }
            catch (Exception e)
            {
                if (e.Message != null)
                {
                    rs = (e.Message);

                }

            }

            if (rs != "-1")
            {
                ck.success = true;
                HistoriqueUser hu = new HistoriqueUser();
                string currentId = hu.InsertHistoriqueUser(idUser, "5", "delete Personnal Strongbox");
               

                return StatusCode(201, ck);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "Deleted Failed";
                return StatusCode(403, messageError);
            }

        }

        /// <summary>
        /// Get List of stronbox for Heir.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name=""></param>
        /// <returns>Get List of stronbox for Heir.</returns>
        /// <response code="200">Returns the list</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpGet]
        [Route("strongbox-for-heir")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<IStrongboxForHeirs>), 201)]
        [Authorize]
        public IActionResult GetListStrongboxForHeir()
        {

            List<IStrongboxForHeirs>? liste = new List<IStrongboxForHeirs>();
            string idUser = string.Empty;


            try
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
                idUser = cr.DecryptHexa(lclaims[1].Value);


                User us = new User();
                us = us.GetUserById(idUser);
                string IdCustomer = us.GetIdCustomer(idUser);
                IStrongboxForHeirs per = new IStrongboxForHeirs();

                try
                {
                    liste = per.GetListStrongboxForHeirByIdUser(idUser);
                }
                catch (Exception e)
                {

                    return StatusCode(403, e.Message);
                }



            }
            catch (Exception e)
            {
                if (e.Message != null)
                {

                }

            }

            return Ok(liste);
        }
        /// <summary>
        /// Get stronbox for Heir.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name=""></param>
        /// <returns>Get List  for Heir.</returns>
        /// <response code="200">Returns the strongbox</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpGet]
        [Route("strongbox-for-heir/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IStrongboxForHeirs), 201)]
        [Authorize]
        public IActionResult GetListStrongboxForHeirById(string id)
        {

           
            string idUser = string.Empty;
            bool exist = false;
            IStrongboxForHeirs per = new IStrongboxForHeirs();
            try
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
                idUser = cr.DecryptHexa(lclaims[1].Value);


                User us = new User();
                us = us.GetUserById(idUser);
                string IdCustomer = us.GetIdCustomer(idUser);
            
                exist=per.IfExistStrongboxForHeirByIdUser(idUser);
                if (exist==true)
                {
                    try
                    {
                        per = per.GetStrongboxForHeirById(cr.DecryptHexa(id));
                    }
                    catch (Exception e)
                    {

                        return StatusCode(403, e.Message);
                    }
                }

                else
                {
                    IMessageError messageError = new IMessageError();
                    messageError.message = "This strongbox does not belong to you ";
                    return StatusCode(403, messageError);
                }


            }
            catch (Exception e)
            {
                if (e.Message != null)
                {

                }

            }
            if (exist == true)
            {
                return Ok(per);
            }

            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "This strongbox does not belong to you ";
                return StatusCode(403, messageError);
            }

    
        }


        /// <summary>
        /// Get List of stronbox heritage.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token 
        /// </remarks>
        /// <param name=""></param>
        /// <returns>Get List of stronboxheritage.</returns>
        /// <response code="200">Returns the list</response>
        /// <response code="400">If the item is null</response>    
        [HttpGet]
        [Route("strongbox-heritage")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<IStrongboxHeritage>), 201)]
        [Authorize]
        public IActionResult GetListStrongboxHeritage()
        {

            List<IStrongboxHeritage>? liste = new List<IStrongboxHeritage>();
            List<IStrongboxHeritage>? laliste = new List<IStrongboxHeritage>(); ;
            string idUser = string.Empty;


            try
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
                idUser = cr.DecryptHexa(lclaims[1].Value);
                //string lastname = lclaims[2].Value;
                //string firstname = lclaims[3].Value;
                //string email = lclaims[4].Value;

                IStrongboxHeritage per = new IStrongboxHeritage();
                Heir h=new Heir();  

                h = h.GetHeirByPublicKey(PublicKey);
                try
                {
                    if (h.IdHeir != null)
                    {

                        string idHeir = h.IdHeir;
                        liste = per.GetListStrongboxForHeirByIdHeir(idHeir);

                        if (liste.Count > 0)
                        {
                            laliste.AddRange(liste);
                        }
                    }
                    else
                    {
                        IMessageError messageError = new IMessageError();
                        messageError.message = "You have not been designated as heir, you have no inheritance.";
                        return StatusCode(403, messageError);
                    }
                }
                catch (Exception e)
                {

                    return StatusCode(403, e.Message);
                }



            }
            catch (Exception e)
            {
                if (e.Message != null)
                {

                }

            }
            return Ok(laliste);

        }
        /// <summary>
        /// Get stronbox heritage.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token 
        /// </remarks>
        /// <param name=""></param>
        /// <returns>Get List of stronboxheritage.</returns>
        /// <response code="200">Returns the list</response>
        /// <response code="400">If the item is null</response>    
        [HttpGet]
        [Route("strongbox-heritage/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IStrongboxHeritage), 201)]
        [Authorize]
        public IActionResult GetStrongboxHeritage(string id)
        {

         
      
            string idUser = string.Empty;

            IStrongboxHeritage per = new IStrongboxHeritage();
            try
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
                idUser = cr.DecryptHexa(lclaims[1].Value);
                //string lastname = lclaims[2].Value;
                //string firstname = lclaims[3].Value;
                //string email = lclaims[4].Value;

             
                Heir h = new Heir();

                h = h.GetHeirByPublicKey(PublicKey);
                try
                {
                    if (h.IdHeir != null)
                    {

                        string idHeir = h.IdHeir;
                        per = per.GetStrongboxForHeirByIdHeirAndStrongboxId(idHeir, id);
                        if (per.id==null)
                        {
                            IMessageError messageError = new IMessageError();
                            messageError.message = "You have not been designated as heir on this StrongBox;";
                            return StatusCode(403, messageError);
                        }
                   
                    }
                    else
                    {
                        IMessageError messageError = new IMessageError();
                        messageError.message = "You have not been designated as heir on this StrongBox;";
                        return StatusCode(403, messageError);
                    }
                }
                catch (Exception e)
                {

                    return StatusCode(403, e.Message);
                }



            }
            catch (Exception e)
            {
                if (e.Message != null)
                {

                }

            }
            return Ok(per);

        }

    }
}
