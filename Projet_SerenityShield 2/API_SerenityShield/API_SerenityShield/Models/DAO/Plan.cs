using System.Data;
using System.Data.OleDb;

namespace API_SerenityShield.Models.DAO
{
    public class Plan
    {
        public string Id { get; set; }
        public string Price { get; set; }
        public string Label { get; set; }
        public string? HeirsLimit { get; set; }
        public string? PersonalStrongBoxLimit { get; set; }
        public string? HeirsStrongBoxLimit { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Plan GetCurrentPlan(string IdUser)
        {
            Plan plan = new Plan();

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT     [PLAN].*,[STARTDATE],[ENDDATE] "
                      + " FROM[PLAN]"
                      + " INNER JOIN   CUSTOMER_PLAN ON[PLAN].ID_PLAN = CUSTOMER_PLAN.ID_PLAN"
                      + " INNER JOIN   CUSTOMER ON CUSTOMER_PLAN.ID_CUSTOMER = CUSTOMER.ID_CUSTOMER"
                      + " where Customer.ID_USER='" + IdUser + "'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "PLAN");



            if (ds.Tables["PLAN"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();
                plan.Id = cr.EncryptHexa(ds.Tables["PLAN"].Rows[0]["ID_PLAN"].ToString());
                plan.Price = ds.Tables["PLAN"].Rows[0]["PRICE"].ToString();
                plan.Label = ds.Tables["PLAN"].Rows[0]["LABEL"].ToString();
                if (ds.Tables["PLAN"].Rows[0]["NB_HEIRSTRONGBOXES"].ToString() == "unlimited")
                {
                    plan.HeirsLimit = null;
                }
                else
                {
                    plan.HeirsLimit = ds.Tables["PLAN"].Rows[0]["NB_HEIRSTRONGBOXES"].ToString();
                }
                if (ds.Tables["PLAN"].Rows[0]["NB_PERSONALSTRONGBOXES"].ToString() == "unlimited")
                {
                    plan.PersonalStrongBoxLimit = null;
                }
                else
                {
                    plan.PersonalStrongBoxLimit = ds.Tables["PLAN"].Rows[0]["NB_PERSONALSTRONGBOXES"].ToString();
                }
                if (ds.Tables["PLAN"].Rows[0]["NB_HEIRPERSTRONGBOX"].ToString() == "unlimited")
                {
                    plan.HeirsStrongBoxLimit = null;
                }
                else
                {
                    plan.HeirsStrongBoxLimit = ds.Tables["PLAN"].Rows[0]["NB_HEIRPERSTRONGBOX"].ToString();
                }

                plan.StartDate = ds.Tables["PLAN"].Rows[0]["STARTDATE"].ToString();

                plan.EndDate = ds.Tables["PLAN"].Rows[0]["ENDDATE"].ToString();
            }

            return plan;

        }

        public List<Plan> GetListPlan()
        {

            List<Plan> lp = new List<Plan>();

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT     [PLAN].* FROM [PLAN]", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "PLAN");



            if (ds.Tables["PLAN"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["PLAN"].Rows.Count; i++)
                {
                    Plan plan = new Plan();

                    Cryptage cr = new Cryptage();
                    plan.Id = cr.EncryptHexa(ds.Tables["PLAN"].Rows[i]["ID_PLAN"].ToString());
                    plan.Price = ds.Tables["PLAN"].Rows[i]["PRICE"].ToString();
                    plan.Label = ds.Tables["PLAN"].Rows[i]["LABEL"].ToString();
                    if (ds.Tables["PLAN"].Rows[i]["NB_HEIRSTRONGBOXES"].ToString() == "unlimited")
                    {
                        plan.HeirsLimit = null;
                    }
                    else
                    {
                        plan.HeirsLimit = ds.Tables["PLAN"].Rows[i]["NB_HEIRSTRONGBOXES"].ToString();
                    }
                    if (ds.Tables["PLAN"].Rows[i]["NB_PERSONALSTRONGBOXES"].ToString() == "unlimited")
                    {
                        plan.PersonalStrongBoxLimit = null;
                    }
                    else
                    {
                        plan.PersonalStrongBoxLimit = ds.Tables["PLAN"].Rows[i]["NB_PERSONALSTRONGBOXES"].ToString();
                    }
                    if (ds.Tables["PLAN"].Rows[i]["NB_HEIRPERSTRONGBOX"].ToString() == "unlimited")
                    {
                        plan.HeirsStrongBoxLimit = null;
                    }
                    else
                    {
                        plan.HeirsStrongBoxLimit = ds.Tables["PLAN"].Rows[i]["NB_HEIRPERSTRONGBOX"].ToString();
                    }


                    lp.Add(plan);
                }

            }
            return lp;

        }
        public Plan GetPlanByID(string idPlan)
        {

           

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT     [PLAN].* FROM [PLAN] WHERE ID_PLAN='"+idPlan+"'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "PLAN");
            Plan plan = new Plan();

            if (ds.Tables["PLAN"].Rows.Count > 0)
            {
                
                    

                    Cryptage cr = new Cryptage();
                    plan.Id = cr.EncryptHexa(ds.Tables["PLAN"].Rows[0]["ID_PLAN"].ToString());
                    plan.Price = ds.Tables["PLAN"].Rows[0]["PRICE"].ToString();
                    plan.Label = ds.Tables["PLAN"].Rows[0]["LABEL"].ToString();
                    if (ds.Tables["PLAN"].Rows[0]["NB_HEIRSTRONGBOXES"].ToString() == "unlimited")
                    {
                        plan.HeirsLimit = null;
                    }
                    else
                    {
                        plan.HeirsLimit = ds.Tables["PLAN"].Rows[0]["NB_HEIRSTRONGBOXES"].ToString();
                    }
                    if (ds.Tables["PLAN"].Rows[0]["NB_PERSONALSTRONGBOXES"].ToString() == "unlimited")
                    {
                        plan.PersonalStrongBoxLimit = null;
                    }
                    else
                    {
                        plan.PersonalStrongBoxLimit = ds.Tables["PLAN"].Rows[0]["NB_PERSONALSTRONGBOXES"].ToString();
                    }
                    if (ds.Tables["PLAN"].Rows[0]["NB_HEIRPERSTRONGBOX"].ToString() == "unlimited")
                    {
                        plan.HeirsStrongBoxLimit = null;
                    }
                    else
                    {
                        plan.HeirsStrongBoxLimit = ds.Tables["PLAN"].Rows[0]["NB_HEIRPERSTRONGBOX"].ToString();
                    }



            }
            return plan;

        }
    }


    public class PaymentHistory
    {
        public string idPayment { get; set; }
        public string idCustomerPlan { get; set; }
        public string cardNumber { get; set; }
        public string paymentDate { get; set; }
        public bool paid { get; set; }
        public string currenccy { get; set; }
        public string amontMonth { get; set; }
        public string amontYear { get; set; }
        public string amontYearWithTax { get; set; }
        public string walletPublicKey { get; set; }
        public string paiementType { get; set; }
        public string vat { get; set; }//pourcentage axe
        public string invoiceNumber { get; set; }
        public string customerFirstName { get; set; }
        public string customerLastName { get; set; }
        public string customerEmail { get; set; }
        public string providerName { get; set; }//        invoice.providerName = "SERSH LABS CORP.";
        public string providerAddressLine1 { get; set; }//        invoice.providerAddressLine1 = "P.O.Box 4342,Road Town,Tortala,British Virgin Island";
        public string providerAddressLine2 { get; set; }
        public string providerEmail { get; set; }//        invoice.providerEmail = "contact@serenityshield.io"
        public string providerRegistrationNumber { get; set; }// ex SIRET
        public string providerRegistrationPlace { get; set; }// ex RCS
        public string providerVatNumber { get; set; }// string|null tva number

       
        
        public PaymentHistory GetPayment(string idPayment) 
        {
            List<PaymentHistory> lph = new List<PaymentHistory>();

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand(" SELECT  [PAYMENT_HISTORY].*  FROM            [PAYMENT_HISTORY] "
                                    
                                      + "    WHERE ID_PAYMENT = '" + idPayment + "'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "PAYMENT");

            PaymentHistory ph = new PaymentHistory();
            Cryptage cr = new Cryptage();
            if (ds.Tables["PAYMENT"].Rows.Count > 0)
            {
               
                   

                    ph.idPayment = cr.EncryptHexa(ds.Tables["PAYMENT"].Rows[0]["ID_PAYMENT"].ToString());
                    ph.idCustomerPlan = cr.EncryptHexa(ds.Tables["PAYMENT"].Rows[0]["ID_CUST_PLAN"].ToString());
                    ph.cardNumber = ds.Tables["PAYMENT"].Rows[0]["CARDNUMBER"].ToString();
                    ph.paymentDate = ds.Tables["PAYMENT"].Rows[0]["PAYMENTDATE"].ToString();
                    ph.paid = (bool)ds.Tables["PAYMENT"].Rows[0]["ISPAID"];
                    ph.amontMonth = ds.Tables["PAYMENT"].Rows[0]["AMOUNT_MONTH"].ToString();
                    ph.amontYear = ds.Tables["PAYMENT"].Rows[0]["AMONT_YEAR"].ToString();
                    ph.amontYearWithTax = ds.Tables["PAYMENT"].Rows[0]["AMONT_YEAR_WITH_TAX"].ToString();
                    ph.currenccy = ds.Tables["PAYMENT"].Rows[0]["CURRENCY"].ToString();
                    ph.walletPublicKey = ds.Tables["PAYMENT"].Rows[0]["WALLETPUBLICKEY"].ToString();
                    ph.paiementType = ds.Tables["PAYMENT"].Rows[0]["PAIEMENTTYPE"].ToString();
                    ph.vat = ds.Tables["PAYMENT"].Rows[0]["VAT"].ToString();
                    ph.invoiceNumber = ds.Tables["PAYMENT"].Rows[0]["INVOICE_NUMBER"].ToString();
                    ph.customerFirstName = ds.Tables["PAYMENT"].Rows[0]["CUSTOMER_FIRSTNAME"].ToString();
                    ph.customerLastName = ds.Tables["PAYMENT"].Rows[0]["CUSTOMER_LASTNAME"].ToString();
                    ph.customerEmail = ds.Tables["PAYMENT"].Rows[0]["CUSTOMER_EMAIL"].ToString();
                    ph.providerName = ds.Tables["PAYMENT"].Rows[0]["PROVIDER_NAME"].ToString();
                    ph.providerEmail = ds.Tables["PAYMENT"].Rows[0]["PROVIDER_EMAIL"].ToString();
                    ph.providerAddressLine1 = ds.Tables["PAYMENT"].Rows[0]["PROVIDER_ADRESSLINE1"].ToString();
                    ph.providerAddressLine2 = ds.Tables["PAYMENT"].Rows[0]["PROVIDER_ADRESSLINE2"].ToString();
                    ph.providerRegistrationNumber = ds.Tables["PAYMENT"].Rows[0]["PROVIDER_REGISTRATIONNUMBER"].ToString();
                    ph.providerRegistrationPlace = ds.Tables["PAYMENT"].Rows[0]["PROVIDER_REGISTRATIONPLACE"].ToString();
                    ph.providerVatNumber = ds.Tables["PAYMENT"].Rows[0]["PROVIDER_VATNUMBER"].ToString();


               

            }

            return ph;
        }
        public List<PaymentHistory> GetListPaymentHistoryByIdUser(string idUser)
        {
            List<PaymentHistory> lph = new List<PaymentHistory>();

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand(" SELECT  [PAYMENT_HISTORY].*  FROM            [PAYMENT_HISTORY] "
                                      +"    INNER JOIN CUSTOMER_PLAN ON PAYMENT_HISTORY.ID_CUST_PLAN = CUSTOMER_PLAN.ID_CUST_PLAN"
                                      + "    INNER JOIN CUSTOMER ON CUSTOMER_PLAN.ID_CUSTOMER = CUSTOMER.ID_CUSTOMER"
                                      + "    INNER JOIN _USER ON CUSTOMER.ID_USER = _USER.ID_USER"
                                      + "    WHERE _USER.ID_USER = '"+idUser+"'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "PAYMENT");


            Cryptage cr = new Cryptage();
            if (ds.Tables["PAYMENT"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["PAYMENT"].Rows.Count; i++)
                {
                    PaymentHistory ph = new PaymentHistory();

                    ph.idPayment = cr.EncryptHexa(ds.Tables["PAYMENT"].Rows[i]["ID_PAYMENT"].ToString());
                    ph.idCustomerPlan = cr.EncryptHexa(ds.Tables["PAYMENT"].Rows[i]["ID_CUST_PLAN"].ToString());
                    ph.cardNumber = ds.Tables["PAYMENT"].Rows[i]["CARDNUMBER"].ToString();
                    ph.paymentDate = ds.Tables["PAYMENT"].Rows[i]["PAYMENTDATE"].ToString();
                    ph.paid = (bool)ds.Tables["PAYMENT"].Rows[i]["ISPAID"];
                    ph.amontMonth =ds.Tables["PAYMENT"].Rows[i]["AMOUNT_MONTH"].ToString();
                    ph.amontYear = ds.Tables["PAYMENT"].Rows[i]["AMONT_YEAR"].ToString();
                    ph.amontYearWithTax = ds.Tables["PAYMENT"].Rows[i]["AMONT_YEAR_WITH_TAX"].ToString();
                    ph.currenccy = ds.Tables["PAYMENT"].Rows[i]["CURRENCY"].ToString();
                    ph.walletPublicKey = ds.Tables["PAYMENT"].Rows[i]["WALLETPUBLICKEY"].ToString();
                    ph.paiementType = ds.Tables["PAYMENT"].Rows[i]["PAIEMENTTYPE"].ToString();
                    ph.vat = ds.Tables["PAYMENT"].Rows[i]["VAT"].ToString();
                    ph.invoiceNumber = ds.Tables["PAYMENT"].Rows[i]["INVOICE_NUMBER"].ToString();
                    ph.customerFirstName = ds.Tables["PAYMENT"].Rows[i]["CUSTOMER_FIRSTNAME"].ToString();
                    ph.customerLastName = ds.Tables["PAYMENT"].Rows[i]["CUSTOMER_LASTNAME"].ToString();
                    ph.customerEmail= ds.Tables["PAYMENT"].Rows[i]["CUSTOMER_EMAIL"].ToString();
                    ph.providerName = ds.Tables["PAYMENT"].Rows[i]["PROVIDER_NAME"].ToString();
                    ph.providerEmail = ds.Tables["PAYMENT"].Rows[i]["PROVIDER_EMAIL"].ToString();
                    ph.providerAddressLine1 = ds.Tables["PAYMENT"].Rows[i]["PROVIDER_ADRESSLINE1"].ToString();
                    ph.providerAddressLine2 = ds.Tables["PAYMENT"].Rows[i]["PROVIDER_ADRESSLINE2"].ToString();
                    ph.providerRegistrationNumber = ds.Tables["PAYMENT"].Rows[i]["PROVIDER_REGISTRATIONNUMBER"].ToString();
                    ph.providerRegistrationPlace = ds.Tables["PAYMENT"].Rows[i]["PROVIDER_REGISTRATIONPLACE"].ToString();
                    ph.providerVatNumber = ds.Tables["PAYMENT"].Rows[i]["PROVIDER_VATNUMBER"].ToString();
                   

                    lph.Add(ph);
                }


            }

            return lph;
        }

        public string InsertPaymentHistory(string idCustomerPlan, string payMonth, string payYear, string currency, string walletPublicKey, string paiementType,User us)
        {
            string CurrentID = string.Empty;

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString();
            try
            {


                Cryptage cr = new Cryptage();
                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("INSERT INTO [dbo].[PAYMENT_HISTORY] ([ID_CUST_PLAN] ,[PAYMENTDATE],[ISPAID],[AMOUNT_MONTH] ,[AMONT_YEAR],[CURRENCY],[WALLETPUBLICKEY],[PAIEMENTTYPE] ,[AMONT_YEAR_WITH_TAX],[VAT],[CUSTOMER_FIRSTNAME],[CUSTOMER_LASTNAME],[CUSTOMER_EMAIL],[PROVIDER_NAME],[PROVIDER_ADRESSLINE1],[PROVIDER_ADRESSLINE2],[PROVIDER_EMAIL])"

                        + " OUTPUT INSERTED.ID_PAYMENT"
                        + " VALUES ('" + idCustomerPlan + "','" + dateNow + "',1,'" + payMonth + "','" + payYear + "','" + currency + "','" + walletPublicKey + "','" + paiementType + "','"+ payYear + "','0','"+us.FirstName+ "','" + us.LastName + "','" + us.Email + "','SERSH LABS CORP','P.O.Box 4342,Road Town,Tortala,British Virgin Island',NULL,'contact@serenityshield.io')", connexDB);
                CurrentID = commDB.ExecuteScalar().ToString();
                connexDB.Close();

                return CurrentID;

            }
            catch (Exception ex)
            {
                connexDB.Close();
                return "-1";


            }
        }

    }


    public class CustomerPlan
    {
        public string IdCustomerPlan { get; set; }
        public string IdCustomer { get; set; }
        public string IdPlan
        {
            get; set;
        }
        public string StartDate { get; set; }
        public string EndDate { get; set; }


        public string InsertCustomerPlan(string idCustomer, string idPlan)
        {

            string CurrentID = string.Empty;

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString();
            try
            {


                Cryptage cr = new Cryptage();
                // Log.Create();
                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("INSERT INTO [dbo].[CUSTOMER_PLAN]([ID_CUSTOMER],[ID_PLAN],[STARTDATE])"

                        + " OUTPUT INSERTED.ID_CUST_PLAN"
                        + " VALUES ('" + idCustomer + "','" + idPlan + "','" + dateNow + "')", connexDB);
                //Log.WriteLine(commDB.CommandText);
                CurrentID = commDB.ExecuteScalar().ToString();
                connexDB.Close();
                //  Log.LineBreak();

                return CurrentID;

            }
            catch (Exception ex)
            {
                // Log.WriteLine(ex.Message);
                connexDB.Close();
                // Log.LineBreak();
                return "-1";


            }

        }

        public List<CustomerPlan> GetListCustomerPlan(string idCustomer)
        {
            CustomerPlan cp;
            List<CustomerPlan> lcp = new List<CustomerPlan>();
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT  *"
                            + " FROM            [CUSTOMER_PLAN]"
                            + "  where ID_CUSTOMER='" + idCustomer + "'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "PLAN");



            if (ds.Tables["PLAN"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["PLAN"].Rows.Count; i++)
                {
                    cp = new CustomerPlan();
                    cp.IdCustomerPlan = ds.Tables["PLAN"].Rows[i]["ID_CUST_PLAN"].ToString();
                    cp.IdCustomer = ds.Tables["PLAN"].Rows[i]["ID_CUSTOMER"].ToString();
                    cp.IdPlan = ds.Tables["PLAN"].Rows[i]["ID_PLAN"].ToString();
                    cp.StartDate = ds.Tables["PLAN"].Rows[i]["STARTDATE"].ToString();
                    cp.EndDate = ds.Tables["PLAN"].Rows[i]["ENDDATE"].ToString();
                    lcp.Add(cp);
                }


            }

            return lcp;

        }

        public CustomerPlan GetCustomerPlan(string idCustomer)
        {
            CustomerPlan cp;

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT  *"
                            + " FROM            [CUSTOMER_PLAN]"
                            + "  where ID_CUSTOMER='" + idCustomer + "' and ENDDATE IS NULL or ENDDATE=''", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "PLAN");

            cp = new CustomerPlan();

            if (ds.Tables["PLAN"].Rows.Count > 0)
            {


                cp.IdCustomerPlan = ds.Tables["PLAN"].Rows[0]["ID_CUST_PLAN"].ToString();
                cp.IdCustomer = ds.Tables["PLAN"].Rows[0]["ID_CUSTOMER"].ToString();
                cp.IdPlan = ds.Tables["PLAN"].Rows[0]["ID_PLAN"].ToString();
                cp.StartDate = ds.Tables["PLAN"].Rows[0]["STARTDATE"].ToString();
                cp.EndDate = ds.Tables["PLAN"].Rows[0]["ENDDATE"].ToString();



            }

            return cp;

        }
        public CustomerPlan GetCustomerPlanByID(string idCustomerPlan)
        {
            CustomerPlan cp;

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT  *"
                            + " FROM            [CUSTOMER_PLAN]"
                            + "  where ID_CUST_PLAN='" + idCustomerPlan + "' and ENDDATE IS NULL or ENDDATE=''", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "PLAN");

            cp = new CustomerPlan();

            if (ds.Tables["PLAN"].Rows.Count > 0)
            {


                cp.IdCustomerPlan = ds.Tables["PLAN"].Rows[0]["ID_CUST_PLAN"].ToString();
                cp.IdCustomer = ds.Tables["PLAN"].Rows[0]["ID_CUSTOMER"].ToString();
                cp.IdPlan = ds.Tables["PLAN"].Rows[0]["ID_PLAN"].ToString();
                cp.StartDate = ds.Tables["PLAN"].Rows[0]["STARTDATE"].ToString();
                cp.EndDate = ds.Tables["PLAN"].Rows[0]["ENDDATE"].ToString();



            }

            return cp;

        }
        public string UpdateCustomerPlan(string idCustomerPlan)
        {

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString();
            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[CUSTOMER_PLAN]"
                    + "  SET [ENDDATE] = '" + dateNow + "' "
                    + " WHERE ID_CUST_PLAN=" + idCustomerPlan + "", connexDB);

                commDB.ExecuteNonQuery();
                connexDB.Close();

                return "1";
            }
            catch (Exception ex)
            {

                connexDB.Close();

                return "0";

            }

        }

        public bool IfExistCustomerPlan(string idCustomer)
        {
            bool exist = false;

            string isOk = string.Empty;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            commDB = new OleDbCommand("if exists (select * FROM  [CUSTOMER_PLAN] where ID_CUSTOMER='" + idCustomer + "' and ENDDATE IS NULL or ENDDATE='') "
                            + " select 'True'  "
                            + " else "
                            + " select 'False' return", connexDB);

            isOk = commDB.ExecuteScalar().ToString();
            if (isOk == "True")
            {
                exist = true;
            }
            connexDB.Close();
            return exist;
        }

    }
    public class IPlan
    {
        public string id { get; set; }
        public string price { get; set; }
        public string label { get; set; }
        public string? heirsLimit { get; set; }
        public string? personalStrongBoxLimit { get; set; }
        public string? heirsStrongBoxLimit { get; set; }



    }
    public class IInvoice
    {
        public string id { get; set; }
        public string total { get; set; }
        public string totalWithTax { get; set; }
        public string vat { get; set; }
 
        public string currency { get; set; }
        public string paymentType { get; set; }
    
        public string dateIssue
        { get; set; }

        public string invoiceNumber { get; set; }

        public string customerFirstName { get; set; }
        public string customerLastName { get; set; }
        public string customerEmail { get; set; }
        public string customerWalletPublicKey { get; set; }
        public string providerName { get; set; }//SERSH LABS CORP.
        public string providerAddressLine1 { get; set; }//P.O.Box 4342,Road Town,Tortala,British Virgin Island
        public string? providerAddressLine2 { get; set; }
        public string providerEmail { get; set; }//contact@serenityshield.io
        public string? providerRegistrationNumber { get; set; }
        public string? providerRegistrationPlace { get; set; }
        public string? providerVatNumber { get; set; }
        public IInvoiceItem item { get; set; }

    }

    public class IInvoiceItem
    {
        public string id { get; set; }
        public string price { get; set; }
        public string priceWithTax { get; set; }
        public string label { get; set; }
    

    }

    public class PlanModel
    {
        public string IdPlan { get; set; }
        public string Price { get; set; }
        public string Label { get; set; }
        public string NbHeirPerStrongbox { get; set; }
        public string NbPersonnalStrongboxes { get; set; }
        public string NbHeirStrongboxes { get; set; }
    }

    public class CustomerPlanModel
    {
        public string idPlan { get; set; }
        public string transaction { get; set; }
        public string currency { get; set; }
        public string paiementType { get; set; }


    }
}




    

