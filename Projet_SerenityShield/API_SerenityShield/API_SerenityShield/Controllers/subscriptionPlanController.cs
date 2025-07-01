using API_SerenityShield.Models;
using API_SerenityShield.Models.DAO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;

namespace API_SerenityShield.Controllers
{


    [ApiController]
    public class subscriptionplanController : ControllerBase
    {
        /// <summary>
        /// insert a customer plan.
        /// </summary>
        /// <remarks>
        ///Need  Bearer Token
        /// </remarks>
        /// <param name="plan"></param>
        /// <returns>insert a customer plan</returns>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="403">If customer plan exit</response>   
        /// <response code="400">If the item is null</response>    
        [HttpPost]
        [Route("plan")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), 201)]
        [Authorize]
        public IActionResult InsertCustomerPlan([FromBody] CustomerPlanModel plan)
        {
            string rs = string.Empty;
            string idpayment = string.Empty;
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
                string idPlan = cr.DecryptHexa(plan.idPlan);
                // verification if exit a plan
                bool exist = cp.IfExistCustomerPlan(IdCustomer);
                if (exist == false)
                {
                    rs = cp.InsertCustomerPlan(IdCustomer, idPlan);
                    Plan p = new Plan();
                    p = p.GetPlanByID(idPlan);
                    string pyear = (Convert.ToDouble(p.Price.Replace('.', ',')) * 12).ToString();
                    PaymentHistory paymentHistory = new PaymentHistory();
                    try
                    {
                        idpayment = paymentHistory.InsertPaymentHistory(rs, p.Price, pyear, plan.currency, PublicKey, plan.paiementType, us);
                    }
                    catch (Exception e)
                    {

                        return StatusCode(403, e.Message);
                    }

                }
                else
                {
                    IMessageError messageError = new IMessageError();
                    messageError.message = "You cannot do this action a payment plan already exists";
                    return StatusCode(403, messageError);
                }

            }
            catch (Exception e)
            {
                if (e.Message != null)
                {
                    rs = (e.Message);

                }

            }

            if (rs != "-1" && idpayment != "-1")
            {
                ck.success = true;
                HistoriqueUser hu = new HistoriqueUser();
                string currentId = hu.InsertHistoriqueUser(idUser, "9", "Payment plan subscription");
                return StatusCode(201, ck);
            }
            else
            {
                IMessageError messageError = new IMessageError();
                messageError.message = "Insert failed";
                return StatusCode(403,messageError);
            }

        }


        /// <summary>
        /// Update Plan in case of the customer want to change his plan
        /// </summary>
        /// <remarks>
        /// Need  Bearer Token 
        /// </remarks>
        /// <response code="201">Return Boolean</response> 
        /// <response code="400">If the item is null</response> 
        /// <response code="500">Internal Error</response>  

        [ProducesResponseType(201)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CheckValueModel), StatusCodes.Status201Created)]
        [HttpPatch]
        [Route("plan")]
        [CustomAuthorizationFilter]

        public IActionResult UpdateNewCustomerPlan([FromBody] CustomerPlanModel plan)
        {
            //To Do verify that we can change plan by downgrading
            //To Do verify that the transaction is verify with blockchain api
            string rs = string.Empty;
            string idUser = string.Empty;
            string IDNewCustomerPlan = string.Empty;
            try
            {
                Request.Headers.TryGetValue("Authorization", out var headerValue);
                string headerValues = headerValue;
                string? Token = string.Empty;
                if (headerValues != null)
                {
                    string[] tab = headerValues.Split(' ');
                    Token = tab[1];
                }
                //recupation IdUser du token 
                var handler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtSecurityToken = new JwtSecurityToken();

                string PublicKey = String.Empty;
                Cryptage cr = new Cryptage();

                handler = new JwtSecurityTokenHandler();
                jwtSecurityToken = handler.ReadJwtToken(Token);
                idUser = String.Empty;
                PublicKey = String.Empty;


                IEnumerable<Claim> claims = jwtSecurityToken.Claims;
                List<Claim> lclaims = claims.ToList();
                PublicKey = lclaims[0].Value;
                //decryptage idUSER

                idUser = cr.DecryptHexa(lclaims[1].Value);


                User us = new User();
                us = us.GetUserById(idUser);
                //gesttion case erreur et downgrade
                string IdCustomer = us.GetIdCustomer(idUser);

                CustomerPlan cp = new CustomerPlan();
                cp = cp.GetCustomerPlan(IdCustomer);
                string idPlan = cr.DecryptHexa(plan.idPlan);
                Plan p = new Plan();
                p = p.GetCurrentPlan(idUser);
                string pyear = (Convert.ToDouble(p.Price) * 12).ToString();
                rs = cp.UpdateCustomerPlan(cp.IdCustomerPlan);
                IDNewCustomerPlan = cp.InsertCustomerPlan(IdCustomer, idPlan);
                PaymentHistory paymentHistory = new PaymentHistory();
                string idpayment = paymentHistory.InsertPaymentHistory(rs, p.Price, pyear, PublicKey, plan.currency, plan.paiementType, us);
            }
            catch (Exception e)
            {
                if (e.Message != null)
                {
                    rs = (e.Message);
                }

            }

            CheckValueModel ck = new CheckValueModel();
            if (rs == "1")
            {
                ck.success = true;
                HistoriqueUser hu = new HistoriqueUser();
                string currentId = hu.InsertHistoriqueUser(idUser, "9", "Change of payment plan");
                return CreatedAtAction("UpdateNewCustomerPlan", ck);
            }
            else
            {
                ck.success = false;
                return BadRequest("{message: wrong parameters}");

            }



        }


        /// <summary>
        /// Get list Plan.
        /// </summary>
        /// <remarks>
        ///  No parameters it's open
        /// </remarks>
        /// <response code="200">Return list Plan</response> 

        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<IPlan>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("plan")]

        public IActionResult GetListPlan()
        {
            List<Plan> plans = new List<Plan>();
            Plan p = new Plan();

            plans = p.GetListPlan();
            List<IPlan> Iplans = new List<IPlan>();
            foreach (Plan pl in plans)
            {
                IPlan plan = new IPlan();
                plan.id = pl.Id;
                plan.label = pl.Label;
                plan.price = pl.Price;
                plan.heirsLimit = pl.HeirsLimit;
                plan.heirsStrongBoxLimit = pl.HeirsStrongBoxLimit;
                plan.personalStrongBoxLimit = pl.PersonalStrongBoxLimit;

                Iplans.Add(plan);

            }

            return Ok(Iplans);

        }

        /// <summary>
        /// Get list Iinvoice.
        /// </summary>
        /// <remarks>
        /// Need  Bearer Token and code sended
        /// </remarks>
        /// <response code="200">Return list Plan</response> 

        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<IInvoice>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("invoice")]
        [CustomAuthorizationFilter]
        public IActionResult GetListInvoice()
        {

            string rs = string.Empty;
            string idpayment = string.Empty;
            string idUser = string.Empty;
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
            List<PaymentHistory> lph = new List<PaymentHistory>();
            PaymentHistory p = new PaymentHistory();

            lph = p.GetListPaymentHistoryByIdUser(idUser);
            List<IInvoice> Iinv = new List<IInvoice>();
            foreach (PaymentHistory ph in lph)
            {
                IInvoice invoice = new IInvoice();
                invoice.id = ph.idPayment;
                invoice.total = ph.amontYear;
                invoice.totalWithTax = ((Convert.ToDouble(ph.amontYear)) + (Convert.ToDouble(ph.amontYear) * (0 / 100))).ToString();
                invoice.vat = "0";
                invoice.invoiceNumber = cr.DecryptHexa(ph.idPayment);
                invoice.currency = ph.currenccy;
                invoice.paymentType = ph.paiementType;
                invoice.dateIssue = ph.paymentDate;
                invoice.customerWalletPublicKey = ph.walletPublicKey;
                invoice.customerFirstName = us.FirstName;
                invoice.customerLastName = us.LastName;
                invoice.customerEmail = us.Email;
                invoice.providerName = ph.providerName;
                invoice.providerAddressLine1 = ph.providerAddressLine1;
                invoice.providerAddressLine2 = ph.providerAddressLine2;
                invoice.providerEmail = ph.providerEmail;
                invoice.providerRegistrationNumber = ph.providerRegistrationNumber;
                invoice.providerRegistrationPlace = ph.providerRegistrationPlace;
                invoice.providerVatNumber = ph.providerVatNumber;

                IInvoiceItem item = new IInvoiceItem();
                CustomerPlan cp = new CustomerPlan();
                cp = cp.GetCustomerPlanByID(cr.DecryptHexa(ph.idCustomerPlan));
                Plan pl = new Plan();
                pl = pl.GetPlanByID(cp.IdPlan);
                item.id = pl.Id;
                item.label = pl.Label;
                item.price = pl.Price;
                item.priceWithTax = pl.Price;
                invoice.item = item;
                Iinv.Add(invoice);

            }

            return Ok(Iinv);

        }

        /// <summary>
        /// Get  Iinvoice.
        /// </summary>
        /// <remarks>
        /// Need  Bearer Token 
        /// </remarks>
        /// <response code="200">Return Invoice</response> 

        [ProducesResponseType(200)]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<IInvoice>), StatusCodes.Status200OK)]
        [HttpGet]
        [Route("invoice/{id}")]
        [CustomAuthorizationFilter]
        public IActionResult GetInvoiceById(string id)
        {

            string rs = string.Empty;
            string idpayment = string.Empty;
            string idUser = string.Empty;
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

            PaymentHistory ph = new PaymentHistory();
            string idph = cr.DecryptHexa(id);
            ph = ph.GetPayment(idph);

            IInvoice invoice = new IInvoice();
            invoice.id = ph.idPayment;
            invoice.total = ph.amontYear;
            invoice.totalWithTax = ((Convert.ToDouble(ph.amontYear)) + (Convert.ToDouble(ph.amontYear) * (0 / 100))).ToString();
            invoice.vat = "0";
            invoice.invoiceNumber = cr.DecryptHexa(ph.idPayment);
            invoice.currency = ph.currenccy;
            invoice.paymentType = ph.paiementType;
            invoice.dateIssue = ph.paymentDate;
            invoice.customerWalletPublicKey = ph.walletPublicKey;
            invoice.customerFirstName = us.FirstName;
            invoice.customerLastName = us.LastName;
            invoice.customerEmail = us.Email;
            invoice.providerName = ph.providerName;
            invoice.providerAddressLine1 = ph.providerAddressLine1;
            invoice.providerAddressLine2 = ph.providerAddressLine2;
            invoice.providerEmail = ph.providerEmail;
            invoice.providerRegistrationNumber = ph.providerRegistrationNumber;
            invoice.providerRegistrationPlace = ph.providerRegistrationPlace;
            invoice.providerVatNumber = ph.providerVatNumber;

            IInvoiceItem item = new IInvoiceItem();
            CustomerPlan cp = new CustomerPlan();
            cp = cp.GetCustomerPlanByID(cr.DecryptHexa(ph.idCustomerPlan));
            Plan pl = new Plan();
            pl = pl.GetPlanByID(cp.IdPlan);
            item.id = pl.Id;
            item.label = pl.Label;
            item.price = pl.Price;
            item.priceWithTax = pl.Price;
            invoice.item = item;




            return Ok(invoice);

        }
    }
}
