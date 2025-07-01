using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;

namespace API_SerenityShield.Controllers
{
    public class SecretContractController : Controller
    {
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
        [ProducesResponseType(typeof(short[]), StatusCodes.Status200OK)]
        [Route("secret-sc")]
       
        public IActionResult GetSecretContract()
        {
            string fileName = @"C:\inetpub\wwwroot\API-test_SerenityShield\contract.wasm";
            byte[] bytes=FileToByteArray(fileName);
            short[] shorts = Array.ConvertAll(bytes, b => (short)b);
            return Ok(bytes); //return the byte data
        }

        /// <summary>
        /// Function to get byte array from a file
        /// </summary>
        /// <param name="_FileName">File name to get byte array</param>
        /// <returns>Byte Array</returns>
        public byte[] FileToByteArray(string _FileName)
        {
            byte[] _Buffer = null;

            try
            {
                // Open file for reading
                System.IO.FileStream _FileStream = new System.IO.FileStream(_FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);

                // attach filestream to binary reader
                System.IO.BinaryReader _BinaryReader = new System.IO.BinaryReader(_FileStream);

                // get total byte length of the file
                long _TotalBytes = new System.IO.FileInfo(_FileName).Length;

                // read entire file into buffer
                _Buffer = _BinaryReader.ReadBytes((Int32)_TotalBytes);

                // close file reader
                _FileStream.Close();
                _FileStream.Dispose();
                _BinaryReader.Close();
            }
            catch (Exception _Exception)
            {
                // Error
                Console.WriteLine("Exception caught in process: {0}", _Exception.ToString());
            }

            return _Buffer;
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
        [ProducesResponseType(typeof(short[]), StatusCodes.Status200OK)]
        [Route("secret-sc-64")]

        public IActionResult GetSecretContract64()
        {
             string fileName = @"C:\inetpub\wwwroot\API-test_SerenityShield\contract.wasm";
            Byte[] bytes = FileToByteArray(fileName);   
            string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
            return Ok(base64String); //return the byte data
        }
     
    }

}



