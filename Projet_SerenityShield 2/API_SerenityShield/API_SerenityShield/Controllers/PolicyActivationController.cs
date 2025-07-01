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
    public class PolicyActivationController : Controller
    {
        [HttpPost]
        [Produces("application/json")]
        [Route("reactive")]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]
        public IActionResult ReactivePolicyActivation(ActivityVerificationReactiveModel model)
        {
            Cryptage cryptage = new Cryptage();
            string idUser = cryptage.DecryptHexa(model.idUser);
            string idStrongBox = cryptage.DecryptHexa(model.idStrongboxHeir);
            ActivityVerification av = new ActivityVerification();
            string res = av.ActiveActivityVerification(model.idUser, model.idStrongboxHeir);
            CheckValueModel check = new CheckValueModel();
            if (res == "1")
            {
                check.success = true;
            }
            else
            {
                check.success = false;
            }
            return Ok(check);


        }
        [HttpPost]
        [Produces("application/json")]
        [Route("declared-deceaded")]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]
        public IActionResult DeclaredDeceadedPolicyActivation(ActivityVerificationReactiveModel model)
        {
            Cryptage cryptage = new Cryptage();
            string idUser = cryptage.DecryptHexa(model.idUser);
            string idStrongBox = cryptage.DecryptHexa(model.idStrongboxHeir);
            ActivityVerification av = new ActivityVerification();
            string res = av.UpdateDeceadedActivityVerification(model.idUser, model.idStrongboxHeir);
            CheckValueModel check = new CheckValueModel();
            if (res == "1")
            {
                check.success = true;
            }
            else
            {
                check.success = false;
            }
            return Ok(check);


        }
        [HttpPost]
        [Produces("application/json")]
        [Route("unlock-strongboxforheir/{id}")]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]
        public IActionResult UnlockStrongboxForHeir(string id)
        {
            Cryptage cryptage = new Cryptage();

            string idStrongBox = cryptage.DecryptHexa(id);
            IStrongboxForHeirs sb = new IStrongboxForHeirs();
            string res = sb.UnlockStrongboxForHeir(idStrongBox);
            CheckValueModel check = new CheckValueModel();
            if (res == "1")
            {
                check.success = true;
            }
            else
            {
                check.success = false;
            }
            return Ok(check);


        }

        [HttpPost]
        [Produces("application/json")]
        [Route("lock-strongboxforheir/{id}")]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]
        public IActionResult LockStrongboxForHeir(string id)
        {
            Cryptage cryptage = new Cryptage();

            string idStrongBox = cryptage.DecryptHexa(id);
            IStrongboxForHeirs sb = new IStrongboxForHeirs();
            string res = sb.LockStrongboxForHeir(idStrongBox);
            CheckValueModel check = new CheckValueModel();
            if (res == "1")
            {
                check.success = true;
            }
            else
            {
                check.success = false;
            }
            return Ok(check);


        }
        [HttpPost]
        [Produces("application/json")]
        [Route("paid-strongboxforheir/{id}")]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]
        public IActionResult PaidStrongboxForHeir(string id)
        {
            Cryptage cryptage = new Cryptage();

            string idStrongBox = cryptage.DecryptHexa(id);
            IStrongboxForHeirs sb = new IStrongboxForHeirs();
            string res = sb.PaidStrongboxForHeir(idStrongBox);
            CheckValueModel check = new CheckValueModel();
            if (res == "1")
            {
                check.success = true;
            }
            else
            {
                check.success = false;
            }
            return Ok(check);


        }
        [HttpPost]
        [Produces("application/json")]
        [Route("unpaid-strongboxforheir/{id}")]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status200OK)]
        public IActionResult UnPaidStrongboxForHeir(string id)
        {
            Cryptage cryptage = new Cryptage();

            string idStrongBox = cryptage.DecryptHexa(id);
            IStrongboxForHeirs sb = new IStrongboxForHeirs();
            string res = sb.UnPaidStrongboxForHeir(idStrongBox);
            CheckValueModel check = new CheckValueModel();
            if (res == "1")
            {
                check.success = true;
            }
            else
            {
                check.success = false;
            }
            return Ok(check);


        }
    }


}
public class ActivityVerificationReactiveModel
{


    public string idUser
    {
        get; set;
    }

    public string idStrongboxHeir
    {
        get; set;
    }

}


