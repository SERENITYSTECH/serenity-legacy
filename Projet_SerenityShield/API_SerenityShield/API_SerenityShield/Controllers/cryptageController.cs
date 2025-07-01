using API_SerenityShield.Models;
using API_SerenityShield.Models.DAO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_SerenityShield.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class cryptageController : ControllerBase
    {

        [HttpPost]
        [Produces("application/json")]
        [Route("encrypt")]
        public IActionResult EncryptData([FromBody] CryptageModel crm)
        {
            Cryptage cr = new Cryptage();
            string EncryptData1 = cr.Encrypt(crm.data1);
            string EncryptData2 = cr.Encrypt(crm.data2);
            CryptageModel m = new CryptageModel();
            m.data1 = EncryptData1;
            m.data2 = EncryptData2;
            return Ok(m);


        }
        [HttpPost]
        [Produces("application/json")]
        [Route("decrypt")]
        public IActionResult DecryptData([FromBody] CryptageModel crm)
        {
            Cryptage cr = new Cryptage();
            string EncryptData1 = cr.Decrypt(crm.data1);
            string EncryptData2 = cr.Decrypt(crm.data2);
            DeCryptageModel m = new DeCryptageModel();
            m.data1 = EncryptData1;
            m.data2 = EncryptData2;
            return Ok(m);

        }

        [HttpPost]
        [Produces("application/json")]
        [Route("encryptHexa")]
        public IActionResult EncryptDataHexa([FromBody] CryptageModel crm)
        {
            Cryptage cr = new Cryptage();
            string EncryptData1 = cr.EncryptHexa(crm.data1);
            string EncryptData2 = cr.EncryptHexa(crm.data2);
            CryptageModel m = new CryptageModel();
            m.data1 = EncryptData1;
            m.data2 = EncryptData2;
            return Ok(m);


        }
        [HttpPost]
        [Produces("application/json")]
        [Route("decryptHexa")]
        public IActionResult DecryptDataHexa([FromBody] CryptageModel crm)
        {
            Cryptage cr = new Cryptage();
         
            string EncryptData1 = cr.DecryptHexa(crm.data1);
            string EncryptData2 = cr.DecryptHexa(crm.data2);
            DeCryptageModel m = new DeCryptageModel();
            m.data1 = EncryptData1;
            m.data2 = EncryptData2;
            return Ok(m);

        }
    }
}
