using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceInactivityPeriod.AppCode
{
    public class User
    {


        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber
        {
            get; set;
        }
        public string PublicKey
        {
            get; set;
        }
        public string IdCard
        {
            get; set;
        }
        public string Passsport
        {
            get; set;
        }
        public string AddedSecurity
        {
            get; set;
        }

        public List<string> ListPublicKey { get; set; }
        public List<WalletConnection> ListWalletConnection { get; set; }
        public List<Heir> ListHeir { get; set; }
        public bool IsCustomer { get; set; }
        public string IdCustomer { get; set; }
        public bool IsHeir { get; set; }
        public string IdHeir { get; set; }
        public bool IsActive { get; set; }

        public bool Verified { get; set; }

        public Plan CurrentPaymentPlan { get; set; }

        public string SubcriptionDate { get; set; }

        public User()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public User GetUserCustomerById(string id)
        {
            User u;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT        [_USER].*,[CUSTOMER_CREATION_DATE]"
                            + " FROM            [_USER] INNER JOIN CUSTOMER ON _USER.ID_USER=CUSTOMER.ID_USER  "
                            + " WHERE  CUSTOMER.ID_USER=" + id + " ", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "User");

            u = new User();

            if (ds.Tables["User"].Rows.Count > 0)
            {
             
                string IdUser = ds.Tables["User"].Rows[0]["ID_USER"].ToString();
                u.Id = ds.Tables["User"].Rows[0]["ID_USER"].ToString();
                u.LastName = ds.Tables["User"].Rows[0]["LASTNAME"].ToString();
                u.FirstName = ds.Tables["User"].Rows[0]["FIRSTNAME"].ToString();
                u.Email = ds.Tables["User"].Rows[0]["EMAIL"].ToString();
                if (!string.IsNullOrEmpty(ds.Tables["User"].Rows[0]["PASSPORT"].ToString()))
                {
                    u.Passsport = ds.Tables["User"].Rows[0]["PASSPORT"].ToString();
                }
                else
                {
                    u.Passsport = null;
                }
                if (!string.IsNullOrEmpty(ds.Tables["User"].Rows[0]["ADDSECURITY"].ToString()))
                {
                    u.AddedSecurity = ds.Tables["User"].Rows[0]["ADDSECURITY"].ToString();
                }
                else
                {
                    u.AddedSecurity = null;
                }
                if (!string.IsNullOrEmpty(ds.Tables["User"].Rows[0]["IDCARDNUMBER"].ToString()))
                {
                    u.IdCard = ds.Tables["User"].Rows[0]["IDCARDNUMBER"].ToString();
                }
                else
                {
                    u.IdCard = null;
                }

                u.PhoneNumber = ds.Tables["User"].Rows[0]["PHONENUMBER"].ToString();
                u.IsCustomer = (bool)ds.Tables["User"].Rows[0]["IS_CUSTOMER"];
                string IdCustomer = GetIdCustomer(IdUser);
                if (IdCustomer == "-1")
                {
                    u.IdCustomer = null;
                }
                else
                {
                    u.IdCustomer = IdCustomer;
                }

                u.IsHeir = (bool)ds.Tables["User"].Rows[0]["IS_HEIR"];
                string IdHeir = GetIdHeir(IdUser);
                if (IdHeir == "-1")
                {
                    u.IdHeir = null;
                }
                else
                {
                    u.IdHeir = IdHeir;
                }


                u.Verified = (bool)ds.Tables["User"].Rows[0]["VERIFIED"];
                u.IsActive = (bool)ds.Tables["User"].Rows[0]["ACTIVE"];
                WalletConnection wac = new WalletConnection();
                u.ListPublicKey = wac.GetListPublicKeys(IdUser);
                u.ListWalletConnection = wac.GetListWalletConnection(IdUser);
                if (!string.IsNullOrEmpty(u.IdCustomer))
                {
                    Heir h = new Heir();
                    u.ListHeir = h.GetListHeirByListIdUserCustomer(IdUser);
                }
                Plan plan = new Plan();
                if (plan != null)
                {
                    u.CurrentPaymentPlan = plan.GetCurrentPlan(IdUser);
                }
                else
                {
                    u.CurrentPaymentPlan = null;
                }

                u.SubcriptionDate = ds.Tables["User"].Rows[0]["CUSTOMER_CREATION_DATE"].ToString();
            }

            return u;
        }

        public User GetUserHeirById(string id)
        {
            User u;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT        [_USER].*,[HEIR_CREATION_DATE]"
                            + " FROM            [_USER] INNER JOIN HEIR ON _USER.ID_USER=HEIR.ID_USER  "
                            + " WHERE  HEIR.ID_USER=" + id + " and active=1", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "User");

            u = new User();

            if (ds.Tables["User"].Rows.Count > 0)
            {
              
                string IdUser = ds.Tables["User"].Rows[0]["ID_USER"].ToString();
                u.Id = ds.Tables["User"].Rows[0]["ID_USER"].ToString();
                u.LastName = ds.Tables["User"].Rows[0]["LASTNAME"].ToString();
                u.FirstName = ds.Tables["User"].Rows[0]["FIRSTNAME"].ToString();
                u.Email =ds.Tables["User"].Rows[0]["EMAIL"].ToString();
                if (!string.IsNullOrEmpty(ds.Tables["User"].Rows[0]["PASSPORT"].ToString()))
                {
                    u.Passsport = ds.Tables["User"].Rows[0]["PASSPORT"].ToString();
                }
                else
                {
                    u.Passsport = null;
                }
                if (!string.IsNullOrEmpty(ds.Tables["User"].Rows[0]["ADDSECURITY"].ToString()))
                {
                    u.AddedSecurity = ds.Tables["User"].Rows[0]["ADDSECURITY"].ToString();
                }
                else
                {
                    u.AddedSecurity = null;
                }
                if (!string.IsNullOrEmpty(ds.Tables["User"].Rows[0]["IDCARDNUMBER"].ToString()))
                {
                    u.IdCard = ds.Tables["User"].Rows[0]["IDCARDNUMBER"].ToString();
                }
                else
                {
                    u.IdCard = null;
                }

                u.PhoneNumber = ds.Tables["User"].Rows[0]["PHONENUMBER"].ToString();
                u.IsCustomer = (bool)ds.Tables["User"].Rows[0]["IS_CUSTOMER"];
                string IdCustomer = GetIdCustomer(IdUser);
                if (IdCustomer == "-1")
                {
                    u.IdCustomer = null;
                }
                else
                {
                    u.IdCustomer = IdCustomer;
                }


                u.IsHeir = (bool)ds.Tables["User"].Rows[0]["IS_HEIR"];
                if (IdHeir == "-1")
                {
                    u.IdHeir = null;
                }
                else
                {
                    u.IdHeir = IdHeir;
                }


                u.IsActive = (bool)ds.Tables["User"].Rows[0]["ACTIVE"];
                WalletConnection wac = new WalletConnection();
                u.ListPublicKey = wac.GetListPublicKeys(IdUser);
                u.ListWalletConnection = wac.GetListWalletConnection(IdUser);
                if (!string.IsNullOrEmpty(u.IdCustomer))
                {
                    Heir h = new Heir();
                    u.ListHeir = h.GetListHeirByListIdUserCustomer(IdUser);
                }
                Plan plan = new Plan();
                if (plan != null)
                {
                    u.CurrentPaymentPlan = plan.GetCurrentPlan(IdUser);
                }
                else
                {
                    u.CurrentPaymentPlan = null;
                }

                u.SubcriptionDate = ds.Tables["User"].Rows[0]["HEIR_CREATION_DATE"].ToString();
            }

            return u;
        }

        public User GetUserById(string id)
        {
            User u;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT        [_USER].*"
                            + " FROM            [_USER]  "
                            + " WHERE  ID_USER='" + id + "'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "User");

            u = new User();

            Cryptage cr = new Cryptage();
            string IdUser = ds.Tables["User"].Rows[0]["ID_USER"].ToString();
            u.Id = cr.EncryptHexa(ds.Tables["User"].Rows[0]["ID_USER"].ToString());
            u.LastName = cr.Decrypt(ds.Tables["User"].Rows[0]["LASTNAME"].ToString());
            u.FirstName = cr.Decrypt(ds.Tables["User"].Rows[0]["FIRSTNAME"].ToString());
            u.Email = cr.Decrypt(ds.Tables["User"].Rows[0]["EMAIL"].ToString());
            if (!string.IsNullOrEmpty(cr.Decrypt(ds.Tables["User"].Rows[0]["PASSPORT"].ToString())))
            {
                u.Passsport = cr.Decrypt(ds.Tables["User"].Rows[0]["PASSPORT"].ToString());
            }
            else
            {
                u.Passsport = null;
            }
            if (!string.IsNullOrEmpty(cr.Decrypt(ds.Tables["User"].Rows[0]["ADDSECURITY"].ToString())))
            {
                u.AddedSecurity = cr.Decrypt(ds.Tables["User"].Rows[0]["ADDSECURITY"].ToString());
            }
            else
            {
                u.AddedSecurity = null;
            }
            if (!string.IsNullOrEmpty(cr.Decrypt(ds.Tables["User"].Rows[0]["IDCARDNUMBER"].ToString())))
            {
                u.IdCard = cr.Decrypt(ds.Tables["User"].Rows[0]["IDCARDNUMBER"].ToString());
            }
            else
            {
                u.IdCard = null;
            }

            u.PhoneNumber = cr.Decrypt(ds.Tables["User"].Rows[0]["PHONENUMBER"].ToString());
            u.IsCustomer = (bool)ds.Tables["User"].Rows[0]["IS_CUSTOMER"];
            string IdCustomer = GetIdCustomer(IdUser);
            if (IdCustomer == "-1")
            {
                u.IdCustomer = null;
            }
            else
            {
                u.IdCustomer = cr.EncryptHexa(IdCustomer);
            }


            u.IsHeir = (bool)ds.Tables["User"].Rows[0]["IS_HEIR"];
            if (IdHeir == "-1")
            {
                u.IdHeir = null;
            }
            else
            {
                u.IdHeir = IdHeir;
            }


            u.Verified = (bool)ds.Tables["User"].Rows[0]["VERIFIED"];
            u.IsActive = (bool)ds.Tables["User"].Rows[0]["ACTIVE"];
            WalletConnection wac = new WalletConnection();
            u.ListPublicKey = wac.GetListPublicKeys(IdUser);
            u.ListWalletConnection = wac.GetListWalletConnection(IdUser);
            if (!string.IsNullOrEmpty(u.IdCustomer))
            {
                Heir h = new Heir();
                u.ListHeir = h.GetListHeirByListIdUserCustomer(IdUser);
            }
            Plan plan = new Plan();
            if (plan != null)
            {
                u.CurrentPaymentPlan = plan.GetCurrentPlan(IdUser);
            }
            else
            {
                u.CurrentPaymentPlan = null;
            }


        

            return u;
        }
        public User GetUserByPublicKey(string publicKey)
        {
            User u;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT        [_USER].*,[CUSTOMER_CREATION_DATE]"
                            + " FROM            [_USER] INNER JOIN"
                            + " WALLET_CONNECTION ON _USER.ID_USER = WALLET_CONNECTION.ID_USER"
                            + " INNER JOIN CUSTOMER ON _USER.ID_USER = CUSTOMER.ID_USER"
                            + "  where WALLET_CONNECTION.WALLET_PUBLICKEY='" + publicKey + "' ", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "User");

            u = new User();

            if (ds.Tables["User"].Rows.Count > 0)
            {
                string IdUser = ds.Tables["User"].Rows[0]["ID_USER"].ToString();
                u.Id = ds.Tables["User"].Rows[0]["ID_USER"].ToString();
                u.LastName = ds.Tables["User"].Rows[0]["LASTNAME"].ToString();
                u.FirstName = ds.Tables["User"].Rows[0]["FIRSTNAME"].ToString();
                u.Email = ds.Tables["User"].Rows[0]["EMAIL"].ToString();
                if (!string.IsNullOrEmpty(ds.Tables["User"].Rows[0]["PASSPORT"].ToString()))
                {
                    u.Passsport = ds.Tables["User"].Rows[0]["PASSPORT"].ToString();
                }
                else
                {
                    u.Passsport = null;
                }
                if (!string.IsNullOrEmpty(ds.Tables["User"].Rows[0]["ADDSECURITY"].ToString()))
                {
                    u.AddedSecurity = ds.Tables["User"].Rows[0]["ADDSECURITY"].ToString();
                }
                else
                {
                    u.AddedSecurity = null;
                }
                if (!string.IsNullOrEmpty(ds.Tables["User"].Rows[0]["IDCARDNUMBER"].ToString()))
                {
                    u.IdCard = ds.Tables["User"].Rows[0]["IDCARDNUMBER"].ToString();
                }
                else
                {
                    u.IdCard = null;
                }

                u.PhoneNumber = ds.Tables["User"].Rows[0]["PHONENUMBER"].ToString();
                u.IsCustomer = (bool)ds.Tables["User"].Rows[0]["IS_CUSTOMER"];
                string IdCustomer = GetIdCustomer(IdUser);
                if (IdCustomer == "-1")
                {
                    u.IdCustomer = null;
                }
                else
                {
                    u.IdCustomer = IdCustomer;
                }


                u.IsHeir = (bool)ds.Tables["User"].Rows[0]["IS_HEIR"];
                if (IdHeir == "-1")
                {
                    u.IdHeir = null;
                }
                else
                {
                    u.IdHeir = IdHeir;
                }


                u.Verified = (bool)ds.Tables["User"].Rows[0]["VERIFIED"];
                u.IsActive = (bool)ds.Tables["User"].Rows[0]["ACTIVE"];
                WalletConnection wac = new WalletConnection();
                u.ListPublicKey = wac.GetListPublicKeys(IdUser);
                u.ListWalletConnection = wac.GetListWalletConnection(IdUser);
                if (!string.IsNullOrEmpty(u.IdCustomer))
                {
                    Heir h = new Heir();
                    u.ListHeir = h.GetListHeirByListIdUserCustomer(IdUser);
                }
                Plan plan = new Plan();
                if (plan != null)
                {
                    u.CurrentPaymentPlan = plan.GetCurrentPlan(IdUser);
                }
                else
                {
                    u.CurrentPaymentPlan = null;
                }

        
                u.SubcriptionDate = ds.Tables["User"].Rows[0]["CUSTOMER_CREATION_DATE"].ToString();
            }

            return u;
        }
       
      
 

        
        public string UpdateCodeEmailUser(string IdUser, string code)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[_USER]"
                    + "  SET [EMAILVERIFICATIONCODE] = '" + code + "' "
                    + " WHERE ID_USER=" + IdUser + "", connexDB);

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
        public string UpdateCodeSmsUser(string IdUser, string code)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[_USER]"
                    + "  SET [SMSVERIFICATIONCODE] = '" + code + "' "
                    + " WHERE ID_USER=" + IdUser + "", connexDB);

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
        public string UpdateVerificationUser(string IdUser)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[_USER]"
                    + "  SET [VERIFIED] = 1,[ACTIVE]=1"
                    + " WHERE ID_USER=" + IdUser + "", connexDB);

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
        public string InactiveUser(string idUser)
        {

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[_USER]"
                    + "  SET [ACTIVE] = 0 "
                    + " WHERE ID_USER=" + idUser + "", connexDB);

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
        public string RemoveHeirStatus(string idUser)
        {

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[_USER]"
                    + "  SET [IS_HEIR] = 0 "
                    + " WHERE ID_USER=" + idUser + "", connexDB);

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

        public bool IfExistCodeEmail(string code, string IdUser)
        {
            bool exist = false;

            string isOk = string.Empty;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            commDB = new OleDbCommand("if exists (select * from  [dbo].[_USER] where EMAILVERIFICATIONCODE=" + code + " and Id_User='" + IdUser + "' ) "
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


        public bool IfExistCodeSMS(string code, string IdUser)
        {
            bool exist = false;

            string isOk = string.Empty;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            commDB = new OleDbCommand("if exists (select * from  [dbo].[_USER] where SMSVERIFICATIONCODE=" + code + "  and Id_User='" + IdUser + "' ) "
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
        public bool IfExistUserByInfos(string LastName, string Firstname, string Email)
        {
            bool exist = false;

            string isOk = string.Empty;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            commDB = new OleDbCommand("if exists (select * from  [dbo].[_USER] where [LASTNAME]=" + LastName + " and [FIRSTNAME]='" + Firstname + "' and [EMAIL]='" + Email + "' ) "
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
        public bool IfExistUser(string idUser)
        {
            bool exist = false;

            string isOk = string.Empty;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            commDB = new OleDbCommand("if exists (select * from  [dbo].[_USER] where [ID_USER]='" + idUser + "' ) "
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
        public bool IfExistEmail(string Email)
        {
            bool exist = false;

            string isOk = string.Empty;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            commDB = new OleDbCommand("if exists (select * from  [dbo].[_USER] where  [EMAIL]='" + Email + "' and active=1 ) "
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
        public string GetIdCustomer(string idUser)
        {
            string IdCust = string.Empty; ;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT        [ID_CUSTOMER]"
                            + " FROM            [CUSTOMER] WHERE [ID_USER]=" + idUser, connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "CUSTOMER");

            if (ds.Tables["CUSTOMER"].Rows.Count > 0)
            {
                IdCust = ds.Tables["CUSTOMER"].Rows[0]["ID_CUSTOMER"].ToString();
            }

            if (!string.IsNullOrEmpty(IdCust))
            {
                return IdCust;
            }
            else
            {
                return "-1";
            }

        }

        public string GetIdHeir(string idUser)
        {
            string IdHeir = string.Empty; ;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT        [ID_HEIR]"
                            + " FROM            [HEIR] WHERE [ID_USER]=" + idUser, connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "HEIR");

            if (ds.Tables["HEIR"].Rows.Count > 0)
            {
                IdHeir = ds.Tables["HEIR"].Rows[0]["ID_HEIR"].ToString();
            }

            if (!string.IsNullOrEmpty(IdHeir))
            {
                return IdHeir;
            }
            else
            {
                return "-1";
            }

        }
    }
}
