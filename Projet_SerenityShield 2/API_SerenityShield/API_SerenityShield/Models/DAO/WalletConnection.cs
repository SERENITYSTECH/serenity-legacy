using System.Data;
using System.Data.OleDb;

namespace API_SerenityShield.Models.DAO
{
    public class WalletConnection
    {
        public string IdWalletConnection { get; set; }
        public string IdUser { get; set; }
        public string LabelWalletConnection { get; set; }
        public string PublicKeyWalletConnection { get; set; }
        public string TypeWalletConnection { get; set; }
        public string IsHeirWalletConnection { get; set; }
        public string IsCustomerWalletConnection { get; set; }
        public string ActiveWalletConnection { get; set; }
        public string IDUserCustomer { get; set; }
        public string IDTypeWallet { get; set; }
        public string User_SecretPK { get; set; }
        public string metamaskPk { get; set; }
        public string chainId { get; set; }
        public TypeWallet type { get; set; }
        public List<WalletConnection> SelectListWalletConnection()
        {
            List<WalletConnection> LWaC = new List<WalletConnection>();
            return LWaC;
        }
        public string InsertWalletConnectionCustomer(string IdUser, string publicKey, string type,string idTypeWallet)
        {
            string CurrentID = string.Empty;
          
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                Cryptage cr = new Cryptage();
                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("INSERT INTO [dbo].[WALLET_CONNECTION] ([ID_USER],[WALLET_PUBLICKEY],[WALLET_TYPE],[ID_TYPE_WALLET],[WALLET_ISHEIR],[WALLET_ISCUSTOMER],[WALLET_ACTIVE])"

                        + " OUTPUT INSERTED.ID_WALLET"
                        + " VALUES ('" + IdUser + "','" + publicKey + "','" + type + "','" + idTypeWallet + "',0,1,1)", connexDB);
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
        public string InsertWalletConnectionHeir(string IdUser, string publicKey,string IdUserCustomer, string idTypeWallet)
        {
            string CurrentID = string.Empty;

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                Cryptage cr = new Cryptage();
                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("INSERT INTO [dbo].[WALLET_CONNECTION] ([ID_USER],[WALLET_PUBLICKEY],[WALLET_ISHEIR],[WALLET_ISCUSTOMER],[WALLET_ACTIVE],[ID_USERCUSTOMER],[ID_TYPE_WALLET])"

                        + " OUTPUT INSERTED.ID_WALLET"
                        + " VALUES ('" + IdUser + "','" + publicKey + "',1,0,1,'"+ IdUserCustomer + "','" + idTypeWallet + "')", connexDB);
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
        public List<string> GetListPublicKeys(string idUser)
        {
            List<string> PublicKeys = new List<string>();
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT        [WALLET_PUBLICKEY]"
                            + " FROM            [WALLET_CONNECTION] WHERE [ID_USER]='" + idUser+"'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "Wallet");

            for (int i = 0; i < ds.Tables["Wallet"].Rows.Count; i++)
            {
                PublicKeys.Add(ds.Tables["Wallet"].Rows[i]["WALLET_PUBLICKEY"].ToString());

            }
            return PublicKeys;

        }

        public List<WalletConnection> GetListWalletConnection()
        {
            List<WalletConnection> ListWallet = new List<WalletConnection>();
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT *"
                            + " FROM            [WALLET_CONNECTION] WHERE [ID_USER]<210", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "Wallet");

            for (int i = 0; i < ds.Tables["Wallet"].Rows.Count; i++)

            {
                Cryptage cr = new Cryptage();
                WalletConnection wac = new WalletConnection();
                wac.IdUser = cr.EncryptHexa(ds.Tables["Wallet"].Rows[i]["ID_USER"].ToString());
                wac.IdWalletConnection = cr.EncryptHexa(ds.Tables["Wallet"].Rows[i]["ID_WALLET"].ToString());
                wac.LabelWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_LABEL"].ToString();
                wac.PublicKeyWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_PUBLICKEY"].ToString();
                wac.TypeWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_TYPE"].ToString();
                wac.IsHeirWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_ISHEIR"].ToString();
                wac.IsCustomerWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_ISCUSTOMER"].ToString();
                wac.ActiveWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_ACTIVE"].ToString();
                wac.IDUserCustomer = ds.Tables["Wallet"].Rows[i]["ID_USERCUSTOMER"].ToString();
                wac.IDTypeWallet = ds.Tables["Wallet"].Rows[i]["ID_TYPE_WALLET"].ToString();
                wac.User_SecretPK = ds.Tables["Wallet"].Rows[i]["USER_SECRETPK"].ToString();
                wac.metamaskPk = ds.Tables["Wallet"].Rows[i]["METAMASKPK"].ToString();
                wac.chainId = ds.Tables["Wallet"].Rows[i]["CHAINID"].ToString();
                TypeWallet type = new TypeWallet();
                type = type.GetTypeWallet(wac.IDTypeWallet);
                wac.type = type;
                ListWallet.Add(wac);

            }
            return ListWallet;

        }
        public List<WalletConnection> GetListWalletConnection(string idUser)
        {
            List<WalletConnection> ListWallet = new List<WalletConnection>();
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT *"
                            + " FROM            [WALLET_CONNECTION] WHERE [ID_USER]='" + idUser+"'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "Wallet");

            for (int i = 0; i < ds.Tables["Wallet"].Rows.Count; i++)
            
            {
                Cryptage cr=new Cryptage();
                WalletConnection wac = new WalletConnection();
                wac.IdUser =cr.EncryptHexa( ds.Tables["Wallet"].Rows[i]["ID_USER"].ToString());
                wac.IdWalletConnection = cr.EncryptHexa(ds.Tables["Wallet"].Rows[i]["ID_WALLET"].ToString());
                wac.LabelWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_LABEL"].ToString();
                wac.PublicKeyWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_PUBLICKEY"].ToString();
                wac.TypeWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_TYPE"].ToString();
                wac.IsHeirWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_ISHEIR"].ToString();
                wac.IsCustomerWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_ISCUSTOMER"].ToString();
                wac.ActiveWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_ACTIVE"].ToString();
                wac.IDUserCustomer = ds.Tables["Wallet"].Rows[i]["ID_USERCUSTOMER"].ToString();
                wac.IDTypeWallet = ds.Tables["Wallet"].Rows[i]["ID_TYPE_WALLET"].ToString();
                wac.User_SecretPK = ds.Tables["Wallet"].Rows[i]["USER_SECRETPK"].ToString();
                wac.metamaskPk = ds.Tables["Wallet"].Rows[i]["METAMASKPK"].ToString();
                wac.chainId = ds.Tables["Wallet"].Rows[i]["CHAINID"].ToString();
                TypeWallet type=new TypeWallet();
                type = type.GetTypeWallet(wac.IDTypeWallet);
                wac.type = type;
                ListWallet.Add(wac);

            }
            return ListWallet;

        }
        public WalletConnection GetWalletConnectionByPublicKey(string publicKey)
        {

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT *"
                            + " FROM            [WALLET_CONNECTION] WHERE [WALLET_PUBLICKEY]='" + publicKey+"'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "Wallet");
            WalletConnection wac = new WalletConnection();
            for (int i = 0; i < ds.Tables["Wallet"].Rows.Count; i++)
            {
                Cryptage cr = new Cryptage();
                wac.IdUser = cr.EncryptHexa(ds.Tables["Wallet"].Rows[0]["ID_USER"].ToString());
                wac.IdWalletConnection = cr.EncryptHexa(ds.Tables["Wallet"].Rows[0]["ID_WALLET"].ToString());
                wac.LabelWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_LABEL"].ToString();
                wac.PublicKeyWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_PUBLICKEY"].ToString();
                wac.TypeWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_TYPE"].ToString();
                wac.IsHeirWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_ISHEIR"].ToString();
                wac.IsCustomerWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_ISCUSTOMER"].ToString();
                wac.ActiveWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_ACTIVE"].ToString();
                wac.IDUserCustomer = ds.Tables["Wallet"].Rows[0]["ID_USERCUSTOMER"].ToString();
                wac.IDTypeWallet = ds.Tables["Wallet"].Rows[0]["ID_TYPE_WALLET"].ToString();
                wac.User_SecretPK = ds.Tables["Wallet"].Rows[0]["USER_SECRETPK"].ToString();
                wac.metamaskPk = ds.Tables["Wallet"].Rows[0]["METAMASKPK"].ToString();
                wac.chainId = ds.Tables["Wallet"].Rows[0]["CHAINID"].ToString();
                TypeWallet type = new TypeWallet();
                type = type.GetTypeWallet(wac.IDTypeWallet);
                wac.type = type;

            }
            connexDB.Close();
            return wac;

        }
        public WalletConnection GetWalletConnectionByIdUser(string idUser)
        {

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT *"
                            + " FROM            [WALLET_CONNECTION] WHERE [ID_USER]='" + idUser + "'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "Wallet");
            WalletConnection wac = new WalletConnection();
            for (int i = 0; i < ds.Tables["Wallet"].Rows.Count; i++)
            {
                Cryptage cr = new Cryptage();
                wac.IdUser = cr.EncryptHexa(ds.Tables["Wallet"].Rows[0]["ID_USER"].ToString());
                wac.IdWalletConnection = cr.EncryptHexa(ds.Tables["Wallet"].Rows[0]["ID_WALLET"].ToString());
                wac.LabelWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_LABEL"].ToString();
                wac.PublicKeyWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_PUBLICKEY"].ToString();
                wac.TypeWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_TYPE"].ToString();
                wac.IsHeirWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_ISHEIR"].ToString();
                wac.IsCustomerWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_ISCUSTOMER"].ToString();
                wac.ActiveWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_ACTIVE"].ToString();
                wac.IDUserCustomer = ds.Tables["Wallet"].Rows[0]["ID_USERCUSTOMER"].ToString();
                wac.IDTypeWallet = ds.Tables["Wallet"].Rows[0]["ID_TYPE_WALLET"].ToString();
                wac.User_SecretPK = ds.Tables["Wallet"].Rows[0]["USER_SECRETPK"].ToString();
                wac.metamaskPk = ds.Tables["Wallet"].Rows[0]["METAMASKPK"].ToString();
                wac.chainId = ds.Tables["Wallet"].Rows[0]["CHAINID"].ToString();
                TypeWallet type = new TypeWallet();
                type = type.GetTypeWallet(wac.IDTypeWallet);
                wac.type = type;

            }
            connexDB.Close();
            return wac;

        }
        public WalletConnection GetWalletConnectionByPublicKeyAndMetamaskPKAndchaineId(string publicKey,string metamaskPk,string chaineId)
        {

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT *"
                            + " FROM            [WALLET_CONNECTION] WHERE [WALLET_PUBLICKEY]='" + publicKey + "' AND [METAMASKPK]='" + metamaskPk + "' AND [METAMASKPK]='" + chaineId + "'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "Wallet");
            WalletConnection wac = new WalletConnection();
            for (int i = 0; i < ds.Tables["Wallet"].Rows.Count; i++)
            {
                Cryptage cr = new Cryptage();
                wac.IdUser = cr.EncryptHexa(ds.Tables["Wallet"].Rows[0]["ID_USER"].ToString());
                wac.IdWalletConnection = cr.EncryptHexa(ds.Tables["Wallet"].Rows[0]["ID_WALLET"].ToString());
                wac.LabelWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_LABEL"].ToString();
                wac.PublicKeyWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_PUBLICKEY"].ToString();
                wac.TypeWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_TYPE"].ToString();
                wac.IsHeirWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_ISHEIR"].ToString();
                wac.IsCustomerWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_ISCUSTOMER"].ToString();
                wac.ActiveWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_ACTIVE"].ToString();
                wac.IDUserCustomer = ds.Tables["Wallet"].Rows[0]["ID_USERCUSTOMER"].ToString();
                wac.IDTypeWallet = ds.Tables["Wallet"].Rows[0]["ID_TYPE_WALLET"].ToString();
                wac.User_SecretPK = ds.Tables["Wallet"].Rows[0]["USER_SECRETPK"].ToString();
                wac.metamaskPk = ds.Tables["Wallet"].Rows[0]["METAMASKPK"].ToString();
                wac.chainId = ds.Tables["Wallet"].Rows[0]["CHAINID"].ToString();
                TypeWallet type = new TypeWallet();
                type = type.GetTypeWallet(wac.IDTypeWallet);
                wac.type = type;

            }
            connexDB.Close();
            return wac;

        }
        public string UpdateWalletConnectionHeir(string idWalletConnection,string newPublicKey)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;
                Cryptage cr = new Cryptage();

                commDB = new OleDbCommand("UPDATE [dbo].[WALLET_CONNECTION]"

                       + " SET [WALLET_PUBLICKEY] ='" + newPublicKey + "'"

                    + " WHERE [ID_WALLET]='" + idWalletConnection+"'", connexDB);

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
        public string RemoveWalletConnection(string idWalletConnection)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;
                Cryptage cr = new Cryptage();

                commDB = new OleDbCommand("UPDATE [dbo].[WALLET_CONNECTION]"

                       + " SET [WALLET_ACTIVE] =0"

                    + " WHERE [ID_WALLET]='" + idWalletConnection + "'", connexDB);

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
        public string UpdateSecretPublicKeyForWalletConnection(string idUser, string newPublicKey)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;
                Cryptage cr = new Cryptage();

                commDB = new OleDbCommand("UPDATE [dbo].[WALLET_CONNECTION]"

                       + " SET [USER_SECRETPK] ='" + newPublicKey + "'"

                    + " WHERE [ID_USER]='" + idUser + "'", connexDB);

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
        public string UpdateMetamaskPublicKeyForWalletConnection(string idUser, string newPublicKey,string chainId)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;
                Cryptage cr = new Cryptage();

                commDB = new OleDbCommand("UPDATE [dbo].[WALLET_CONNECTION]"

                       + " SET [METAMASKPK] ='" + newPublicKey + "'"
                         + " ,[CHAINID] ='" + chainId + "'"
                    + " WHERE [ID_USER]='" + idUser + "'", connexDB);

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
        public bool IfExistWalletConnection(string publicKey)
        {
            bool exist = false;

            string isOk = string.Empty;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            commDB = new OleDbCommand("if exists (select * from  [dbo].[WALLET_CONNECTION] where WALLET_PUBLICKEY='" + publicKey + "' AND WALLET_ACTIVE=1) "
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
        public bool IfExistWalletConnectionbySolanaAndSecret(string idUser,string publicKey,string secretPublicKey)
        {
            bool exist = false;

            string isOk = string.Empty;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            commDB = new OleDbCommand("if exists (select * from  [dbo].[WALLET_CONNECTION] where ID_USER='"+ idUser + "' AND WALLET_PUBLICKEY='" + publicKey + "' AND USER_SECRETPK='" + secretPublicKey + "' AND WALLET_ACTIVE=1) "
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
        public bool IfExistWalletConnectionbyIdUserAndSecret(string idUser, string secretPublicKey)
        {
            bool exist = false;

            string isOk = string.Empty;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            commDB = new OleDbCommand("if exists (select * from  [dbo].[WALLET_CONNECTION] where ID_USER='" + idUser + "' AND USER_SECRETPK='" + secretPublicKey + "' AND WALLET_ACTIVE=1) "
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
        public bool IfExistWalletConnectionbySolanaAndMetamaskAndCHAINID(string idUser, string publicKey, string metamaskPublicKey,string CHAINID)
        {
            bool exist = false;

            string isOk = string.Empty;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            commDB = new OleDbCommand("if exists (select * from  [dbo].[WALLET_CONNECTION] where ID_USER='" + idUser + "' AND WALLET_PUBLICKEY='" + publicKey + "' AND METAMASKPK='" + metamaskPublicKey + "' AND CHAINID='" + CHAINID + "' AND WALLET_ACTIVE=1) "
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
        public WalletConnection()
        {
        }
    }

    public class LoginModel
    {

        [Required]
        public string PublicKey
        {
            get; set;
        }
    }
    public class LoginModelCheckCode
    {

        [Required]
        public string PublicKey
        {
            get; set;
        }
        public string Code
        {
            get; set;
        }
        public IBuffer Buffer
        {
            get; set;
        }
    }
    public class NewUserModel
    {
        [Required]
        public string publicKey
        {
            get; set;
        }
        [Required]
        public string typeWallet
        {
            get; set;
        }
        [Required]
        public string idTypeWallet
        {
            get; set;
        }
        [Required]
        public string phone
        {
            get; set;
        }
        [Required]
        public string email
        {
            get; set;
        }
        [Required]
        public string firstName
        {
            get; set;
        }
        [Required]
        public string lastName
        {
            get; set;
        }

        public string idCard
        {
            get; set;
        }
        public string passport
        {
            get; set;
        }
        public string addedSecurity
        {
            get; set;
        }
    }
    public class NewLoginModelCheckCode
    {

        public string Code
        {
            get; set;


        }

    }
    public class AllowedPK
    {


        public string publicKey
        {
            get; set;


        }

    }
    public class ResultCheckCode
    {

        public bool success
        {
            get; set;


        }

    }
    public class IBuffer
    {

        public string type
        {
            get; set;


        }
        public int[] data
        {
            get; set;


        }
    }
    public class IMessageError
    {

        public string message
        {
            get; set;


        }

    }
}
