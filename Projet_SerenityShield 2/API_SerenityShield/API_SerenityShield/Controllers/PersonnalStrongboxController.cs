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
using static API_SerenityShield.Models.DAO.PersonnalStrongboxInsertModel;
using static API_SerenityShield.Models.DAO.StrongboxForHeirInsertModel;

namespace API_SerenityShield.Controllers
{

    [ApiController]
    public class PersonnalStrongboxController : ControllerBase
    {

        #region personnalStrongbox
        /// <summary>
        /// insert a personnal strongbox.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>insert a personnal strongbox</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPost]
        [Route("personnal-strongbox")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IStrongboxPersonnal), 201)]
        [Authorize]
        public IActionResult InsertPersonnalStrongbox([FromBody] PersonnalStrongboxInsertModel info)
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
                IStrongboxPersonnal per = new IStrongboxPersonnal();

                try
                {
                    rs = per.InsertStrongboxPersonnal(IdCustomer, info.label, PublicKey, info.scPK, info.codeID, info.payingPK, info.secretPK, info.solanaPK, info.solUsrNftPK, info.solSereNftPK, info.solHeirNftPKs);

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

                                    result = dex.InsertStrongboxContentWalletDex(true, rs, item.label, cr.DecryptHexa(item.idTypeWallet), item.seed, item.provider);


                                    break;

                                case "wallet cex":

                                    IStrongboxContentWalletCex cex = new IStrongboxContentWalletCex();
                                    string resultcex = string.Empty;
                                    {

                                        resultcex = cex.InsertStrongboxContentWalletCex(true, rs, item.label, cr.DecryptHexa(item.idTypeWallet), item.provider);


                                    }
                                    break;
                                case "wallet desktop":
                                    IStrongboxContentWalletDesktop des = new IStrongboxContentWalletDesktop();
                                    string resultdes = string.Empty;


                                    resultdes = des.InsertStrongboxContentWalletDesktop(true, rs, item.label, cr.DecryptHexa(item.idTypeWallet), item.provider);



                                    break;
                                case "wallet mobile":
                                    IStrongboxContentWalletMobile mob = new IStrongboxContentWalletMobile();
                                    string resultmob = string.Empty;

                                    resultmob = mob.InsertStrongboxContentWalletMobile(true, rs, item.label, cr.DecryptHexa(item.idTypeWallet), item.provider);


                                    break;
                                case "wallet hardware":
                                    IStrongboxContentWalletHardware hard = new IStrongboxContentWalletHardware();

                                    string resulthard = string.Empty;

                                    resulthard = hard.InsertStrongboxContentWalletHardware(true, rs, item.label, cr.DecryptHexa(item.idTypeWallet), item.provider);

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
                string currentId = hu.InsertHistoriqueUser(idUser, "3", "add Personnal Strongbox");
                IStrongboxPersonnal sbp = new IStrongboxPersonnal();
                sbp = sbp.GetPersonnalStrongboxById(rs);

                return StatusCode(201, sbp);
            }
            else
            {
                return StatusCode(403, "{\"message\":\"Insert failed\" }");
            }

        }
        /// <summary>
        /// updtate a personnal strongbox.
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
        [Route("personnal-strongbox")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), 201)]
        [Authorize]
        public IActionResult UpdatePersonnalStrongbox([FromBody] PersonnalStrongboxUpdateModel info)
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
                IStrongboxPersonnal per = new IStrongboxPersonnal();

                try
                {

                    //get all list of content
                    string idStrongbox = cr.DecryptHexa(info.id);
                    per = per.GetPersonnalStrongboxById(idStrongbox);


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
                        string idTypeWallet = cr.DecryptHexa(item.idTypeWallet);
                        switch (item.type.ToLower())
                        {
                            case "wallet dex":
                                IStrongboxContentWalletDex dex = new IStrongboxContentWalletDex();
                                string result = string.Empty;

                                result = dex.InsertStrongboxContentWalletDex(true, idStrongbox, item.label, idTypeWallet, item.seed, item.provider);


                                break;

                            case "wallet cex":

                                IStrongboxContentWalletCex cex = new IStrongboxContentWalletCex();
                                string resultcex = string.Empty;
                                {

                                    resultcex = cex.InsertStrongboxContentWalletCex(true, idStrongbox, item.label, idTypeWallet, item.provider);


                                }
                                break;
                            case "wallet desktop":
                                IStrongboxContentWalletDesktop des = new IStrongboxContentWalletDesktop();
                                string resultdes = string.Empty;


                                resultdes = des.InsertStrongboxContentWalletDesktop(true, idStrongbox, item.label, idTypeWallet, item.provider);



                                break;
                            case "wallet mobile":
                                IStrongboxContentWalletMobile mob = new IStrongboxContentWalletMobile();
                                string resultmob = string.Empty;

                                resultmob = mob.InsertStrongboxContentWalletMobile(true, idStrongbox, item.label, idTypeWallet, item.provider);


                                break;
                            case "wallet hardware":
                                IStrongboxContentWalletHardware hard = new IStrongboxContentWalletHardware();

                                string resulthard = string.Empty;

                                resulthard = hard.InsertStrongboxContentWalletHardware(true, idStrongbox, item.label, idTypeWallet, item.provider);

                                break;
                            default:
                                break;
                        }
                    }


                   rs = per.UpdateLabelStrongboxPersonnal(idStrongbox, info.label,info.walletPublicKeyOwner,
                       info.scPK,info.codeID,info.payingPK,info.secretPK,info.solanaPK,info.solSereNftPK,info.solUsrNftPK,info.solHeirNftPKs);
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
                string currentId = hu.InsertHistoriqueUser(idUser, "4", "update Personnal Strongbox");


                return StatusCode(201, ck);
            }
            else
            {
                return StatusCode(403, "{\"message\":\"Insert failed\" }");
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
        [Route("personnal-strongbox/{id}")]
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
                IStrongboxPersonnal per = new IStrongboxPersonnal();

                try
                {
                    per = per.GetPersonnalStrongboxById(cr.DecryptHexa(id));
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
                    rs = per.DesactiveStrongboxPersonnal(cr.DecryptHexa(id));
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
                messageError.message = "Deleted  failed";
                return StatusCode(403, messageError);
            }

        }

        /// <summary>
        /// Get List of personnal strongbox.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name=""></param>
        /// <returns>Get List of personnal strongbox</returns>
        /// <response code="200">Returns the list</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpGet]
        [Route("personnal-strongbox")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<IStrongboxPersonnal>), 201)]
        [Authorize]
        public IActionResult GetListPersonnalStrongbox()
        {

            List<IStrongboxPersonnal>? liste = new List<IStrongboxPersonnal>();
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
                IStrongboxPersonnal per = new IStrongboxPersonnal();

                try
                {
                    liste = per.GetListPersonnalStrongboxByIdUser(idUser);
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
        /// Get personnal strongbox.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name=""></param>
        /// <returns>Get personnal strongbox</returns>
        /// <response code="200">Returns the list</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpGet]
        [Route("personnal-strongbox/{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IStrongboxPersonnal), 201)]
        [Authorize]
        public IActionResult GetPersonnalStrongboxById(string id)
        {


            string idUser = string.Empty;
            bool exist = false;
            IStrongboxPersonnal per = new IStrongboxPersonnal();
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

                exist = per.IfExistStrongboxByIdUser(idUser);
                if (exist == true)
                {
                    try
                    {

                        per = per.GetPersonnalStrongboxById(cr.DecryptHexa(id));
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
        #endregion
        #region contentWalletDex

        /// <summary>
        /// insert a ContentWalletDex.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token ,param "personnal" the parameter is used to identify if the insertion is for a personal strongbox or a stronfbox for heir true for personnal false for Heir
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>insert a ContentWalletDex</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPost]
        [Route("content-wallet-dex")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IStrongboxContentWalletDex), 201)]
        [Authorize]
        public IActionResult InsertContentWalletDex([FromBody] ContentwalletDexInsertModel info)
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

                IStrongboxContentWalletDex per = new IStrongboxContentWalletDex();

                try
                {

                    rs = per.InsertStrongboxContentWalletDex(info.personnal, cr.DecryptHexa(info.idStrongbox), info.label, cr.DecryptHexa(info.idTypeWalletDex), info.seed, info.provider);

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

                IStrongboxContentWalletDex sbp = new IStrongboxContentWalletDex();
                sbp = sbp.GetStrongboxContentWalletDexById(rs);

                return StatusCode(201, sbp);
            }
            else
            {
                return StatusCode(403, "{\"message\":\"Insert failed\" }");
            }

        }
        /// <summary>
        /// updtate a IStrongboxContentWalletDex.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>updtate a  IStrongboxContentWalletDex</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPatch]
        [Route("content-wallet-Dex")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), 201)]
        [Authorize]
        public IActionResult UpdateIStrongboxContentWalletDex([FromBody] ContentwalletDexUpdateModel info)
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
                IStrongboxContentWalletDex per = new IStrongboxContentWalletDex();

                try
                {
                    rs = per.UpdateLabelStrongboxContentWalletDex(cr.DecryptHexa(info.idContentwalletDex), info.label);
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


                return StatusCode(201, ck);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "Insert failed";
                return StatusCode(403, messageError);
            }

        }
        /// <summary>
        /// delete a StrongboxContentWalletDex.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>delete a StrongboxContentWalletDex</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPost]
        [Route("content-wallet-dex_delete")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), 201)]
        [Authorize]
        public IActionResult DeleteIStrongboxContentWalletDex([FromBody] ContentwalletDexDeleteModel info)
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
                IStrongboxContentWalletDex per = new IStrongboxContentWalletDex();

                try
                {

                    rs = per.DesactiveStrongboxContentWalletDex(cr.DecryptHexa(info.idContentwalletDex));
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

                return StatusCode(201, ck);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "Delete failed";
                return StatusCode(403, messageError);
            }
        }
        #endregion
        #region ContentWalletCex
        /// <summary>
        /// insert a ContentWalletCex.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token ,param "personnal" the parameter is used to identify if the insertion is for a personal strongbox or a stronfbox for heir true for personnal false for Heir
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>insert a ContentWalletCex</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPost]
        [Route("content-wallet-cex")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IStrongboxContentWalletCex), 201)]
        [Authorize]
        public IActionResult InsertContentWalletCex([FromBody] ContentwalletCexInsertModel info)
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

                IStrongboxContentWalletCex per = new IStrongboxContentWalletCex();

                try
                {

                    rs = per.InsertStrongboxContentWalletCex(info.personnal, cr.DecryptHexa(info.idStrongbox), info.label, cr.DecryptHexa(info.idTypeWalletCex), info.provider);


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

                IStrongboxContentWalletCex sbp = new IStrongboxContentWalletCex();
                sbp = sbp.GetStrongboxContentWalletCexById(rs);

                return StatusCode(201, sbp);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "Insert failed";
                return StatusCode(403, messageError);
            }

        }
        /// <summary>
        /// updtate a IStrongboxContentWalletCex.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>updtate a  IStrongboxContentWalletDex</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPatch]
        [Route("content-wallet-Cex")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), 201)]
        [Authorize]
        public IActionResult UpdateIStrongboxContentWalletCex([FromBody] ContentwalletCexUpdateModel info)
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
                IStrongboxContentWalletCex per = new IStrongboxContentWalletCex();

                try
                {
                    rs = per.UpdateStrongboxContentWalletCex(cr.DecryptHexa(info.idContentwalletCex), info.label);
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

                return StatusCode(201, ck);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "Insert failed";
                return StatusCode(403, messageError);
            }

        }
        /// <summary>
        /// delete a StrongboxContentWalletCex.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>delete a StrongboxContentWalletCex</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPost]
        [Route("content-wallet-cex-delete")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), 201)]
        [Authorize]
        public IActionResult DeleteIStrongboxContentWalletCex([FromBody] ContentwalletCexDeleteModel info)
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
                IStrongboxContentWalletCex per = new IStrongboxContentWalletCex();

                try
                {

                    rs = per.DesactiveStrongboxContentWalletCex(cr.DecryptHexa(info.idContentwalletCex));
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

                return StatusCode(201, ck);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "Insert failed";
                return StatusCode(403, messageError);
            }

        }
        #endregion
        #region ContentWalletMobile
        /// <summary>
        /// insert a ContentWalletMobile.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token ,param "personnal" the parameter is used to identify if the insertion is for a personal strongbox or a stronfbox for heir true for personnal false for Heir
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>insert a ContentWalletMobile</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPost]
        [Route("content-wallet-mobile")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IStrongboxContentWalletMobile), 201)]
        [Authorize]
        public IActionResult InsertContentWalletMobile([FromBody] ContentwalletMobileInsertModel info)
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

                IStrongboxContentWalletMobile per = new IStrongboxContentWalletMobile();

                try
                {

                    rs = per.InsertStrongboxContentWalletMobile(info.personnal, cr.DecryptHexa(info.idStrongbox), info.label, cr.DecryptHexa(info.idTypeWalletMobile), info.provider);


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

                IStrongboxContentWalletMobile sbp = new IStrongboxContentWalletMobile();
                sbp = sbp.GetStrongboxContentWalletMobileById(rs);

                return StatusCode(201, sbp);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "Insert failed";
                return StatusCode(403, messageError);
            }

        }
        /// <summary>
        /// updtate a IStrongboxContentWalletMobile.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>updtate a  IStrongboxContentWalletMobile</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPatch]
        [Route("content-wallet-mobile")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), 201)]
        [Authorize]
        public IActionResult UpdateIStrongboxContentWalletMobile([FromBody] ContentwalletMobileUpdateModel info)
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
                IStrongboxContentWalletMobile per = new IStrongboxContentWalletMobile();

                try
                {
                    rs = per.UpdateStrongboxContentWalletMobile(cr.DecryptHexa(info.idContentwalletMobile), info.label);
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

                return StatusCode(201, ck);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "Insert failed";
                return StatusCode(403, messageError);
            }

        }
        /// <summary>
        /// delete a StrongboxContentWalletMobile.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>delete a StrongboxContentWalletMobile</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPost]
        [Route("content-wallet-mobile-delete")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), 201)]
        [Authorize]
        public IActionResult DeleteIStrongboxContentWalletMobile([FromBody] ContentwalletMobileDeleteModel info)
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
                IStrongboxContentWalletMobile per = new IStrongboxContentWalletMobile();

                try
                {

                    rs = per.DesactiveStrongboxContentWalletMobile(cr.DecryptHexa(info.idContentwalletMobile));
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

                return StatusCode(201, ck);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "Insert failed";
                return StatusCode(403, messageError);
            }

        }
        #endregion
        #region Desktop
        /// <summary>
        /// insert a ContentWalleDesktop.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token ,param "personnal" the parameter is used to identify if the insertion is for a personal strongbox or a stronfbox for heir true for personnal false for Heir
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>insert a ContentWalleDesktop</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPost]
        [Route("content-wallet-desktop")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IStrongboxContentWalletDesktop), 201)]
        [Authorize]
        public IActionResult InsertContentWalletDesktop([FromBody] ContentwalletDesktopInsertModel info)
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

                IStrongboxContentWalletDesktop per = new IStrongboxContentWalletDesktop();

                try
                {

                    rs = per.InsertStrongboxContentWalletDesktop(info.personnal, cr.DecryptHexa(info.idStrongbox), info.label, cr.DecryptHexa(info.idTypeWalletDesktop), info.provider);

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
                IStrongboxContentWalletDesktop sbp = new IStrongboxContentWalletDesktop();
                sbp = sbp.GetStrongboxContentWalletDesktopById(rs);

                return StatusCode(201, sbp);
            }
            else
            {
                return StatusCode(403, "{\"message\":\"Insert failed\" }");
            }

        }
        /// <summary>
        /// updtate a IStrongboxContentWalletMobile.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>updtate a  IStrongboxContentWalletMobile</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPatch]
        [Route("content-wallet-desktop")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), 201)]
        [Authorize]
        public IActionResult UpdateIStrongboxContentWalletDesktop([FromBody] ContentwalletDesktopUpdateModel info)
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
                IStrongboxContentWalletDesktop per = new IStrongboxContentWalletDesktop();

                try
                {
                    rs = per.UpdateStrongboxContentWalletDesktop(cr.DecryptHexa(info.idContentwalletDesktop), info.label);
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

                return StatusCode(201, ck);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "Insert failed";
                return StatusCode(403, messageError);
            }

        }
        /// <summary>
        /// delete a StrongboxContentWalletDesktop.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>delete a StrongboxContentWalletDesktop</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPost]
        [Route("content-wallet-desktop-delete")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), 201)]
        [Authorize]
        public IActionResult DeleteIStrongboxContentWalletDesktop([FromBody] ContentwalletDesktopDeleteModel info)
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
                IStrongboxContentWalletDesktop per = new IStrongboxContentWalletDesktop();

                try
                {

                    rs = per.DesactiveStrongboxContentWalletDesktop(cr.DecryptHexa(info.idContentwalletDesktop));
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

                return StatusCode(201, ck);
            }
            else
            {
                return StatusCode(403, "{\"message\":\"Insert failed\" }");
            }

        }
        #endregion
        #region hardware
        /// <summary>
        /// insert a ContentWallethardware.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>insert a ContentWallethardware</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPost]
        [Route("content-wallet-hardware")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IStrongboxContentWalletHardware), 201)]
        [Authorize]
        public IActionResult InsertContentWalletHardware([FromBody] ContentwalletHardwareInsertModel info)
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

                IStrongboxContentWalletHardware per = new IStrongboxContentWalletHardware();

                try
                {
                    rs = per.InsertStrongboxContentWalletHardware(info.personnal, cr.DecryptHexa(info.idStrongbox), info.label, cr.DecryptHexa(info.idTypeWalletHardware), info.provider);
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
                IStrongboxContentWalletHardware sbp = new IStrongboxContentWalletHardware();
                sbp = sbp.GetStrongboxContentWalletHardwareById(rs);

                return StatusCode(201, sbp);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "Insert failed";
                return StatusCode(403, messageError);
            }

        }

        /// <summary>
        /// updtate a IStrongboxContentWalletMobile.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>updtate a  IStrongboxContentWalletMobile</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPatch]
        [Route("content-wallet-hardware")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), 201)]
        [Authorize]
        public IActionResult UpdateIStrongboxContentWalletHardware([FromBody] ContentwalletHardwareUpdateModel info)
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
                IStrongboxContentWalletHardware per = new IStrongboxContentWalletHardware();

                try
                {
                    rs = per.UpdateStrongboxContentWalletHardware(cr.DecryptHexa(info.idContentwalletHardware), info.label);
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

                return StatusCode(201, ck);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "Insert failed";
                return StatusCode(403, messageError);
            }

        }
        /// <summary>
        /// delete a StrongboxContentWalletHardware.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name="info"></param>
        /// <returns>delete a StrongboxContentWalletHardware</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPost]
        [Route("content-wallet-hardware-delete")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), 201)]
        [Authorize]
        public IActionResult DeleteIStrongboxContentWalletHardware([FromBody] ContentwalletHardwareDeleteModel info)
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
                IStrongboxContentWalletHardware per = new IStrongboxContentWalletHardware();

                try
                {

                    rs = per.DesactiveStrongboxContentWalletHardware(cr.DecryptHexa(info.idContentwalletHardware));
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

                return StatusCode(201, ck);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "Insert failed";
                return StatusCode(403, messageError);
            }

        }

    }
    #endregion
}
