using API_SerenityShield.Models.DAO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_SerenityShield.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class sendemailController : ControllerBase
    {
        [HttpPost]
        [Produces("application/json")]
        [Route("send-simple-mail")]
        public IActionResult SendSimpleMail([FromBody] SendEmailModel email)
        {
            string rs = string.Empty;
        SendEmail se=new SendEmail();
            try
            {
                se.SendMailSimple(email.Email, email.Subject, email.Body);
                rs = "OK";
            }
            catch (Exception e)
            {
                if (e.Message!=null)
                {
                    rs= (e.Message);
                }
               
            }

            return Ok(rs);

        }
        [HttpPost]
        [Produces("application/json")]
        [Route("send-mail-check")]
        public IActionResult SendMailCheck([FromBody] SendEmailCheckCodeModel email)
        {
            string rs = string.Empty;
            SendEmail se = new SendEmail();
            try
            {
                se.SendMailCheck(email.Email, email.FirstName, email.LastName);
                rs = "OK";
            }
            catch (Exception e)
            {
                if (e.Message != null)
                {
                    rs = (e.Message);
                }

            }

            return Ok(rs);

        }
    }
}
