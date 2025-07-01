using API_SerenityShield.Models.DAO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_SerenityShield.Controllers
{

    [ApiController]
    public class SupportedWalletController : ControllerBase
    {
        /// <summary>
        /// Get list Supported Wallet.
        /// </summary>
        /// <remarks>
        /// No Need  Bearer Token 
        /// </remarks>
        /// <response code="200">Return list Plan</response> 

        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<TypeWallet>), StatusCodes.Status200OK)]
        [HttpGet]

        [Route("supported-wallet")]
        public IActionResult GetSupportedWallet()
        {
            TypeWallet tp = new TypeWallet();
            List<TypeWallet> types = new List<TypeWallet>();
            types = tp.GetListSupportedWallet();
            return Ok(types);
        }


        /// <summary>
        /// Get list DEX.
        /// </summary>
        /// <remarks>
        ///  No parameters it's open
        /// </remarks>
        /// <response code="200">Return list DEX</response> 

        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<TypeWallet>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("dex")]

        public IActionResult GetListDex()
        {
            List<TypeWallet> ltw = new List<TypeWallet>();
            TypeWallet tw = new TypeWallet();

            ltw = tw.GetListWalletDex();

            return Ok(ltw);

        }


        /// <summary>
        /// Get list CEX.
        /// </summary>
        /// <remarks>
        ///  No parameters it's open
        /// </remarks>
        /// <response code="200">Return list CEX</response> 

        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<TypeWallet>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("cex")]

        public IActionResult GetListCex()
        {
            List<TypeWallet> ltw = new List<TypeWallet>();
            TypeWallet tw = new TypeWallet();

            ltw = tw.GetListWalletCex();

            return Ok(ltw);

        }

        /// <summary>
        /// Get list Hardware.
        /// </summary>
        /// <remarks>
        ///  No parameters it's open
        /// </remarks>
        /// <response code="200">Return list Hardware</response> 

        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<TypeWallet>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("hardware")]

        public IActionResult GetListHardware()
        {
            List<TypeWallet> ltw = new List<TypeWallet>();
            TypeWallet tw = new TypeWallet();

            ltw = tw.GetListWalletCold();

            return Ok(ltw);

        }

        /// <summary>
        /// Get list Desktop.
        /// </summary>
        /// <remarks>
        ///  No parameters it's open
        /// </remarks>
        /// <response code="200">Return list Desktop</response> 

        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<TypeWallet>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("desktop")]

        public IActionResult GetListDesktop()
        {
            List<TypeWallet> ltw = new List<TypeWallet>();
            TypeWallet tw = new TypeWallet();

            ltw = tw.GetListWalletDesktop();

            return Ok(ltw);

        }


        ///// <summary>
        ///// Get list 2FA type.
        ///// </summary>
        ///// <remarks>
        ///// No Need  Bearer Token 
        ///// </remarks>
        ///// <response code="200">Return list Plan</response> 

        //[ProducesResponseType(200)]
        //[Produces("application/json")]
        //[ProducesResponseType(typeof(List<ITwoFactor>), StatusCodes.Status200OK)]
        //[HttpGet]

        //[Route("two-factor")]
        //public IActionResult GetListTwoFactor()
        //{
        //    ITwoFactor tp = new ITwoFactor();
        //    List<ITwoFactor> types = new List<ITwoFactor>();
        //    types = tp.GetListTwoFactor();
        //    return Ok(types);
        //}


        /// <summary>
        /// Get list Inactivity period.
        /// </summary>
        /// <remarks>
        /// No Need  Bearer Token 
        /// </remarks>
        /// <response code="200">Return list period</response> 

        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<InactivityPeriod>), StatusCodes.Status200OK)]
        [HttpGet]

        [Route("inactivity-period")]
        public IActionResult GetListInactivityPeriod()
        {
            InactivityPeriod tp = new InactivityPeriod();
            List<InactivityPeriod> types = new List<InactivityPeriod>();
            types = tp.GetListInactivityPeriod();
            return Ok(types);
        }


    }

}
