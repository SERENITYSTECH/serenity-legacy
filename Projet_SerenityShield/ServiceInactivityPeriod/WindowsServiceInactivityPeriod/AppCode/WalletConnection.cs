using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceInactivityPeriod.AppCode
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


        public List<WalletConnection> SelectListWalletConnection()
        {
            List<WalletConnection> LWaC = new List<WalletConnection>();
            return LWaC;
        }
        public string InsertWalletConnectionCustomer(string IdUser, string publicKey, string type, string idTypeWallet)
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
        public string InsertWalletConnectionHeir(string IdUser, string publicKey, string IdUserCustomer, string idTypeWallet)
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
                        + " VALUES ('" + IdUser + "','" + publicKey + "',1,0,1,'" + IdUserCustomer + "','" + idTypeWallet + "')", connexDB);
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
                            + " FROM            [WALLET_CONNECTION] WHERE [ID_USER]='" + idUser + "'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "Wallet");

            for (int i = 0; i < ds.Tables["Wallet"].Rows.Count; i++)
            {
                PublicKeys.Add(ds.Tables["Wallet"].Rows[i]["WALLET_PUBLICKEY"].ToString());

            }
            return PublicKeys;

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
                            + " FROM            [WALLET_CONNECTION] WHERE [ID_USER]='" + idUser + "'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "Wallet");

            for (int i = 0; i < ds.Tables["Wallet"].Rows.Count; i++)

            {
                WalletConnection wac = new WalletConnection();
                wac.IdUser = ds.Tables["Wallet"].Rows[i]["ID_USER"].ToString();
                wac.IdWalletConnection = ds.Tables["Wallet"].Rows[i]["ID_WALLET"].ToString();
                wac.LabelWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_LABEL"].ToString();
                wac.PublicKeyWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_PUBLICKEY"].ToString();
                wac.TypeWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_TYPE"].ToString();
                wac.IsHeirWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_ISHEIR"].ToString();
                wac.IsCustomerWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_ISCUSTOMER"].ToString();
                wac.ActiveWalletConnection = ds.Tables["Wallet"].Rows[i]["WALLET_ACTIVE"].ToString();

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
                            + " FROM            [WALLET_CONNECTION] WHERE [WALLET_PUBLICKEY]='" + publicKey + "'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "Wallet");
            WalletConnection wac = new WalletConnection();
            for (int i = 0; i < ds.Tables["Wallet"].Rows.Count; i++)
            {
              
                wac.IdUser = ds.Tables["Wallet"].Rows[0]["ID_USER"].ToString();
                wac.IdWalletConnection = ds.Tables["Wallet"].Rows[0]["ID_WALLET"].ToString();
                wac.LabelWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_LABEL"].ToString();
                wac.PublicKeyWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_PUBLICKEY"].ToString();
                wac.TypeWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_TYPE"].ToString();
                wac.IsHeirWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_ISHEIR"].ToString();
                wac.IsCustomerWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_ISCUSTOMER"].ToString();
                wac.ActiveWalletConnection = ds.Tables["Wallet"].Rows[0]["WALLET_ACTIVE"].ToString();



            }
            connexDB.Close();
            return wac;

        }
        public string UpdateWalletConnectionHeir(string idWalletConnection, string newPublicKey)
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
        public WalletConnection()
        {
        }
    }
}
