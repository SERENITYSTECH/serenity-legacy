using System.Data;
using System.Data.OleDb;

namespace API_SerenityShield.Models.DAO
{
    public class Customer
    {
        public string IdUser { get; set; }
        public string IdCustomer { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Creation_Date { get; set; }
        public bool IsActive { get; set; }
        public Plan CurrentPaymentPlan { get; set; }
        public List<Plan> ListPaymentPlan { get; set; }
        public string InsertCustomer(User us)
        {
            string CurrentID = string.Empty;
            string idCurrent = string.Empty;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {
                string lastname = !string.IsNullOrEmpty(us.LastName) ? us.LastName : string.Empty;
                string firstname = !string.IsNullOrEmpty(us.FirstName) ? us.FirstName : string.Empty;
                string email = !string.IsNullOrEmpty(us.Email) ? us.Email : string.Empty;
                string passport = !string.IsNullOrEmpty(us.Passsport) ? us.Passsport : string.Empty;
                string addsecurity = !string.IsNullOrEmpty(us.AddedSecurity) ? us.AddedSecurity : string.Empty;
                string idcard = !string.IsNullOrEmpty(us.IdCard) ? us.IdCard : string.Empty;
                string phone = !string.IsNullOrEmpty(us.PhoneNumber) ? us.PhoneNumber : string.Empty;
                string dateNow= DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString();
                Cryptage cr = new Cryptage();
                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("INSERT INTO [dbo].[CUSTOMER]([ID_USER],[CUSTOMER_NAME],[CUSTOMER_FIRSTNAME] ,[CUSTOMER_EMAIL],[CUSTOMER_PHONENUMBER],[CUSTOMER_ACTIVE],[CUSTOMER_CREATION_DATE])"

                        + " OUTPUT INSERTED.ID_CUSTOMER"
                        + " VALUES ('"+us.Id+"','" + cr.Encrypt(lastname) + "','" + cr.Encrypt(firstname) + "','" + cr.Encrypt(email) + "','" + cr.Encrypt(phone) + "',1,'"+dateNow+"')", connexDB);
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

        public Customer GetCustomerById(string id)
        {
            Customer u;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            System.Data.DataSet ds;
            commDB = new OleDbCommand("SELECT * FROM[CUSTOMER] where ID_CUSTOMER = '"+id+"'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "CUSTOMER");

            u = new Customer();

            if (ds.Tables["CUSTOMER"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();
                string IdUser = ds.Tables["CUSTOMER"].Rows[0]["ID_USER"].ToString();
                u.IdCustomer = cr.EncryptHexa(ds.Tables["CUSTOMER"].Rows[0]["ID_CUSTOMER"].ToString());
                u.LastName = cr.Decrypt(ds.Tables["CUSTOMER"].Rows[0]["CUSTOMER_NAME"].ToString());
                u.FirstName = cr.Decrypt(ds.Tables["CUSTOMER"].Rows[0]["CUSTOMER_FIRSTNAME"].ToString());
                u.Email = cr.Decrypt(ds.Tables["CUSTOMER"].Rows[0]["CUSTOMER_EMAIL"].ToString());
            }

            return u;
        }
        public string UpdateCustomer(User us) 
        {

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {
                string lastname = !string.IsNullOrEmpty(us.LastName) ? us.LastName : string.Empty;
                string firstname = !string.IsNullOrEmpty(us.FirstName) ? us.FirstName : string.Empty;
                string email = !string.IsNullOrEmpty(us.Email) ? us.Email : string.Empty;
                string passport = !string.IsNullOrEmpty(us.Passsport) ? us.Passsport : string.Empty;
                string addsecurity = !string.IsNullOrEmpty(us.AddedSecurity) ? us.AddedSecurity : string.Empty;
                string idcard = !string.IsNullOrEmpty(us.IdCard) ? us.IdCard : string.Empty;
                string phone = !string.IsNullOrEmpty(us.PhoneNumber) ? us.PhoneNumber : string.Empty;

                connexDB.Open();
                OleDbCommand commDB;
                Cryptage cr = new Cryptage();

                commDB = new OleDbCommand("UPDATE [dbo].[CUSTOMER]"
                      + "   SET [CUSTOMER_NAME]= '" + cr.Encrypt(lastname) + "'"
                      + ",[CUSTOMER_FIRSTNAME] = '" + cr.Encrypt(firstname) + "'"
                      + ",[CUSTOMER_EMAIL] ='" + cr.Encrypt(email) + "'"
                      + ",[CUSTOMER_PHONENUMBER] ='" + cr.Encrypt(phone) + "'"
                 

                    + " WHERE ID_USER=" + us.Id + "", connexDB);

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
     
        public Customer selectCustomer()
        {
            Customer c = new Customer();
            return c;
        }
        public Customer()
        {

        }

    }
}
