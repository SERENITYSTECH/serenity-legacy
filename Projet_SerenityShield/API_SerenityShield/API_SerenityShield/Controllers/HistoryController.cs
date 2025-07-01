using API_SerenityShield.Models;
using API_SerenityShield.Models.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.IO;

using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace API_SerenityShield.Controllers
{
    public class HistoryUserController : Controller
    {
        /// <summary>
        /// Get User History
        /// </summary>
        /// <remarks>
        ///  Need Bearer in Header.
        /// </remarks>
        /// <response code="200">Return list History</response> 

        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<IHistory>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("listHistory")]
        [Authorize]
        public IActionResult GetListPlan()
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


            List<HistoriqueUser> lhu = new List<HistoriqueUser>();
            HistoriqueUser hu = new HistoriqueUser();

            lhu = hu.GethistoriqueUser(idUser);
            List<IHistory> Ihist= new List<IHistory>();
            foreach (HistoriqueUser h in lhu)
            {
                IHistory ih = new IHistory();
                ih.id = h.IdHistorique;
                ih.title = h.typeHisto.Label;
                TypeHisto th = new TypeHisto();
                th.IdTypeHisto = h.typeHisto.IdTypeHisto;
                th.Label = h.typeHisto.Label;
                ih.type = th;
                ih.description = h.comment;
                ih.date = h.Date_creation;
                Ihist.Add(ih);

            }

            return Ok(Ihist);
            
        }

        /// <summary>
        /// upate User History by clicking link in the Email sended for Activity Verification
        /// </summary>
        /// <remarks>
        ///  Need Bearer in Header.
        /// </remarks>
        /// <response code="200">Return list History</response> 

        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [Microsoft.AspNetCore.Mvc.HttpGet]
        [Route("InsertHistory/{id}")]
        
        public IActionResult InsertHistory(string id)
        {
            string[] tab = id.Split("_");
            Cryptage cr = new Cryptage();
            string iduser = cr.DecryptHexa(tab[0]);
            string idSbh = cr.DecryptHexa(tab[1]);
            CheckValueModel check = new CheckValueModel();
            //insert historique
            HistoriqueUser hu = new HistoriqueUser();
            string currentId = hu.InsertHistoriqueUser(iduser, "14", "Validate Activity by email");
            try
            {
                //Update Activity Verification
                ActivityVerification av = new ActivityVerification();
                av = av.GetActivityVerificationByIdUserandIdSbForHeir(iduser, idSbh);
                string result = av.UpdateCheckActivityVerification(iduser, idSbh);
                if (av.Deceaded == "True")
                {
                    IStrongboxForHeirs sb = new IStrongboxForHeirs();
                    sb = sb.GetStrongboxForHeirById(idSbh);
                    string period = sb.inactivityPeriod;
                    string idPeriod = string.Empty;
                    switch (period)
                    {
                        case "7d":
                            idPeriod = "1";
                            break;
                        case "15d":
                            idPeriod = "2";
                            break;
                        case "1m":
                            idPeriod = "3";
                            break;
                        case "3m":
                            idPeriod = "4";
                            break;
                        case "2m":
                            idPeriod = "5";
                            break;
                        default:
                            break;
                    }
                    string result2 = av.RemoveDeceadedActivityVerification(iduser, idSbh, idPeriod);
                }

                bool succes = false;
                User us = new User();
                us = us.GetUserById(iduser);
             


                if (currentId != "-1")
                {
                    succes = true;
                }
                if (succes == true)
                {
                    SendEmail mail = new SendEmail();
                    mail.SendMailSimple(us.Email, "Inactivity Period", "Dear User,<br>Your account has been verified.<br>We wish you a pleasant day.<br>The Serenity Shield team.");
                    check.success = succes;
                    return StatusCode(201, "Dear User,Your account has been verified. We wish you a pleasant day. The Serenity Shield team.");
                }
                else
                {
                    check.success = succes;
                    return StatusCode(403, check);
                }
            }
            catch (Exception)
            {

                return StatusCode(403, check);
            }
            

        }
    }
}

