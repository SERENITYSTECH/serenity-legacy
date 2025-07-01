using API_SerenityShield.Models.DAO;
using System.Data;
using System.Data.OleDb;

namespace API_SerenityShield.Models
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
        public string? IdCard
        {
            get; set;
        }
        public string? Passsport
        {
            get; set;
        }
        public string? AddedSecurity
        {
            get; set;
        }

        public List<string> ListPublicKey { get; set; }
        public List<WalletConnection> ListWalletConnection { get; set; }
        public List<Heir> ListHeir { get; set; }
        public bool IsCustomer { get; set; }
        public string? IdCustomer { get; set; }
        public bool IsHeir { get; set; }
        public string? IdHeir { get; set; }
        public bool IsActive { get; set; }

        public bool Verified { get; set; }
      
        public Plan? CurrentPaymentPlan { get; set; }

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
                    u.IdCard= cr.Decrypt(ds.Tables["User"].Rows[0]["IDCARDNUMBER"].ToString());
                }
                else
                {
                    u.IdCard = null;
                }
              
                u.PhoneNumber = cr.Decrypt(ds.Tables["User"].Rows[0]["PHONENUMBER"].ToString());
                u.IsCustomer =(bool) ds.Tables["User"].Rows[0]["IS_CUSTOMER"];
                string IdCustomer = GetIdCustomer(IdUser);
                if (IdCustomer == "-1")
                {
                    u.IdCustomer = null;
                }
                else
                {
                    u.IdCustomer = cr.EncryptHexa(IdCustomer);
                }
             
                u.IsHeir =(bool) ds.Tables["User"].Rows[0]["IS_HEIR"];
                string IdHeir = GetIdHeir(IdUser);
                if (IdHeir=="-1")
                {
                    u.IdHeir = null;
                }
                else
                {
                    u.IdHeir = cr.EncryptHexa(IdHeir);
                }
            

                u.Verified = (bool)ds.Tables["User"].Rows[0]["VERIFIED"];
                u.IsActive =(bool) ds.Tables["User"].Rows[0]["ACTIVE"];
                WalletConnection wac = new WalletConnection();
                u.ListPublicKey = wac.GetListPublicKeys(IdUser);
                u.ListWalletConnection = wac.GetListWalletConnection(IdUser);
                if (!string.IsNullOrEmpty(u.IdCustomer))
                {
                    Heir h = new Heir();
                    u.ListHeir = h.GetListHeirByListIdUserCustomer(IdUser);
                }
                Plan plan = new Plan();
                if (plan!=null)
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
                            + " WHERE  ID_USER='" + id + "'" , connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "User");

            u = new User();

            if (ds.Tables["User"].Rows.Count > 0)
            {
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
                u.SubcriptionDate = ds.Tables["User"].Rows[0]["CUSTOMER_CREATION_DATE"].ToString();
            }

            return u;
        }
        public string InsertUserCustomer(User us,string codeEmail,string codesms)
        {
            string CurrentID = string.Empty;

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

                Cryptage cr = new Cryptage();
                // Log.Create();
                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("INSERT INTO [dbo].[_USER]([LASTNAME],[FIRSTNAME],[EMAIL],[PASSPORT],[ADDSECURITY] ,[IDCARDNUMBER] ,[PHONENUMBER],[IS_CUSTOMER],[IS_HEIR],[EMAILVERIFICATIONCODE],[SMSVERIFICATIONCODE],[VERIFIED],[ACTIVE])"

                        + " OUTPUT INSERTED.ID_USER"
                        + " VALUES ('" + cr.Encrypt(lastname) + "','" + cr.Encrypt(firstname) + "','" + cr.Encrypt(email) + "','" + cr.Encrypt(passport) + "','" + cr.Encrypt(addsecurity) + "','" + cr.Encrypt(idcard) + "','" + cr.Encrypt(phone) + "',1,0,'" + codeEmail + "','" + codesms + "',0,0)", connexDB);
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
        public string InsertUserHeir(User us)
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

                Cryptage cr = new Cryptage();
                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("INSERT INTO [dbo].[_USER]([LASTNAME],[FIRSTNAME],[EMAIL],[PASSPORT],[ADDSECURITY] ,[IDCARDNUMBER] ,[PHONENUMBER],[IS_CUSTOMER],[IS_HEIR],[VERIFIED],[ACTIVE])"

                        + " OUTPUT INSERTED.ID_USER"
                        + " VALUES ('" + cr.Encrypt(lastname) + "','" + cr.Encrypt(firstname) + "','" + cr.Encrypt(email) + "','" + cr.Encrypt(passport) + "','" + cr.Encrypt(addsecurity) + "','" + cr.Encrypt(idcard) + "','" + cr.Encrypt(phone) + "',0,1,0,1)", connexDB);
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
        public string UpdateUser(User us)
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

                commDB = new OleDbCommand("UPDATE [dbo].[_USER]"
                      + "   SET [LASTNAME]= '" + cr.Encrypt(lastname) + "'"
                      + ",[FIRSTNAME] = '" + cr.Encrypt(firstname) + "'"
                      + ",[EMAIL] ='" + cr.Encrypt(email) + "'"
                      + ",[PHONENUMBER] ='" + cr.Encrypt(phone) + "'"
                      + ",[PASSPORT] ='" + cr.Encrypt(passport) + "'"
                      + ",[IDCARDNUMBER]  ='" + cr.Encrypt(idcard) + "'"
                      + ",[ADDSECURITY]='" + cr.Encrypt(addsecurity) + "'"
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
        public bool IfExistUserByInfos(string LastName, string Firstname,string Email)
        {
            bool exist = false;

            string isOk = string.Empty;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            commDB = new OleDbCommand("if exists (select * from  [dbo].[_USER] where [LASTNAME]=" + LastName + " and [FIRSTNAME]='" + Firstname + "' and [EMAIL]='"+Email+"' ) "
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
    public class IUser
    {
        public string id { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string email { get; set; }
        public string phone
        {
            get; set;
        }
 
        public string? idCard
        {
            get; set;
        }
        public string? passport
        {
            get; set;
        }
        public string? addedSecurity
        {
            get; set;
        }

       
        public bool activate { get; set; }
       
     

        public IPlan? plan { get; set; }

        public string subcriptionDate { get; set; }

        public IUser()
        { 
        }
    }
    public class JwtToken
    {
        public string access_token { get; set; }
   

        public JwtToken()
        {
        }
    }
    public class CheckValueModel
    {
        public bool success { get; set; }


        public CheckValueModel()
        {
        }
    }
    public class CheckPK
    {
        public bool authorized { get; set; }


        public CheckPK()
        {
        }
    }
    public class SecretPublicKey
    {
        public string secretPublicKey { get; set; }


        public SecretPublicKey()
        {
        }
    }
    public class MetamaskModel
    {
        public string metamaskPublicKey { get; set; }

        public string chainId { get; set; }
        public MetamaskModel()
        {
        }

    }
    public class MetamaskPK
    {
        public string metamaskPublicKey { get; set; }

        public MetamaskPK()
        {
        }
    }
    public class ExistSecretPublicKey
    {
        public string secretPublicKey { get; set; }
        public string solanaPublicKey { get; set; }
        public string idUser { get; set; }
        public ExistSecretPublicKey()
        {
        }
    }
    public class ExistMetamaskPublicKey
    {
        public string metamaskPublicKey { get; set; }
        public string solanaPublicKey { get; set; }
        public string idUser { get; set; }
        public string chainId { get; set; }
        public ExistMetamaskPublicKey()
        {
        }
    }
}
