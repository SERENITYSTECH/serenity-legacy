using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceInactivityPeriod.AppCode
{
    public class content
    {
        public List<IStrongboxContentWalletDex> walletDex { get; set; }
        public List<IStrongboxContentWalletCex> walletCex { get; set; }
        public List<IStrongboxContentWalletMobile> walletMobile { get; set; }
        public List<IStrongboxContentWalletDesktop> walletDesktop { get; set; }
        public List<IStrongboxContentWalletHardware> walletHardware { get; set; }

    }
    public class IStrongboxPersonnal
    {
        public string id { get; set; }
        public string idOwner { get; set; }

        public string walletPublicKeyOwner { get; set; }
        // Wallet used to paid the strongboxe creation and to manage the content

        public string label { get; set; }
        public string scPK { get; set; }
        public string codeID { get; set; }
        public string payingPK { get; set; }
        public string secretPK { get; set; }
        public string solanaPK { get; set; }
        public string solUsrNftPK { get; set; }
        public string solSereNftPK { get; set; }
        public string solHeirNftPKs { get; set; }

        public string dateAdded { get; set; }
        public content content { get; set; }

        public IStrongboxPersonnal() { }


        public string InsertStrongboxPersonnal(string idCustomerOwner, string label, string walletPublicKey, string scpk, string codeId, string payingPK, string secretPK, string solanaPK, string solUsrNftPK, string solSereNftPK, string solHeirNftPKs)
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

                commDB = new OleDbCommand("INSERT INTO [dbo].[StrongboxPersonnal] (  [ID_CUSTOMER],[LABEL] ,[WALLETPUBLICKEY],[ACTIVE],[SCPK] ,[CODEID],[USER_SECRETPK],[USER_PAYINGPK],[USER_SOLANAPK],[NFT_SHARD_SERENITY],[NFT_SHARD_USER],[NFT_SHARD_HEIR],[DATEADDED])"

                        + " OUTPUT INSERTED.ID_StrongboxPersonnal"
                        + " VALUES ('" + idCustomerOwner + "','" + label + "','" + walletPublicKey + "',1,'" + scpk + "','" + codeId + "','" + payingPK + "','" + secretPK + "','" + solanaPK + "','" + solUsrNftPK + "','" + solSereNftPK + "','" + solHeirNftPKs + "','" + dateNow + "')", connexDB);
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




        public string UpdateLabelStrongboxPersonnal(string idstrongbox, string label)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[STRONGBOXPERSONNAL]"
                    + "  SET [LABEL] = '" + label + "' "
                    + " WHERE ID_STRONGBOXPERSONNAL=" + idstrongbox + "", connexDB);

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
        public string DesactiveStrongboxPersonnal(string idstrongbox)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[STRONGBOXPERSONNAL]"
                    + "  SET [ACTIVE] = 0 "
                    + " WHERE ID_STRONGBOXPERSONNAL=" + idstrongbox + "", connexDB);

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
        public IStrongboxPersonnal GetPersonnalStrongboxById(string idPersonnalStrongbox)
        {
            IStrongboxPersonnal u;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT * FROM[dbo].[STRONGBOXPERSONNAL] WHERE ID_STRONGBOXPERSONNAL='" + idPersonnalStrongbox + "' AND ACTIVE=1", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "SB");

            u = new IStrongboxPersonnal();

            if (ds.Tables["SB"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();
                string idstrongox = ds.Tables["SB"].Rows[0]["ID_STRONGBOXPERSONNAL"].ToString();
                u.id = cr.EncryptHexa(ds.Tables["SB"].Rows[0]["ID_STRONGBOXPERSONNAL"].ToString());
                u.idOwner = cr.EncryptHexa(ds.Tables["SB"].Rows[0]["ID_CUSTOMER"].ToString());
                u.label = ds.Tables["SB"].Rows[0]["LABEL"].ToString();
                u.walletPublicKeyOwner = ds.Tables["SB"].Rows[0]["WALLETPUBLICKEY"].ToString();
                u.secretPK = ds.Tables["SB"].Rows[0]["USER_SECRETPK"].ToString();
                u.payingPK = ds.Tables["SB"].Rows[0]["USER_PAYINGPK"].ToString();
                u.solanaPK = ds.Tables["SB"].Rows[0]["USER_SOLANAPK"].ToString();
                u.scPK = ds.Tables["SB"].Rows[0]["SCPK"].ToString();
                u.codeID = ds.Tables["SB"].Rows[0]["CODEID"].ToString();
                u.solSereNftPK = ds.Tables["SB"].Rows[0]["NFT_SHARD_SERENITY"].ToString();
                u.solUsrNftPK = ds.Tables["SB"].Rows[0]["NFT_SHARD_USER"].ToString();
                u.solHeirNftPKs = ds.Tables["SB"].Rows[0]["NFT_SHARD_HEIR"].ToString();
                u.dateAdded = ds.Tables["SB"].Rows[0]["DATEADDED"].ToString();

                content cont = new content();
                IStrongboxContentWalletDex dex = new IStrongboxContentWalletDex();
                cont.walletDex = dex.GetListStrongboxContentWalletDexByIdSbpersonnal(true, idstrongox);
                IStrongboxContentWalletCex cex = new IStrongboxContentWalletCex();
                cont.walletCex = cex.GetListStrongboxContentWalletCexByIdSbpersonnal(true, idstrongox);
                IStrongboxContentWalletMobile mob = new IStrongboxContentWalletMobile();
                cont.walletMobile = mob.GetListStrongboxContentWalletMobileByIdSbpersonnal(true, idstrongox);
                IStrongboxContentWalletDesktop desk = new IStrongboxContentWalletDesktop();
                cont.walletDesktop = desk.GetListStrongboxContentWalletDesktopByIdSbpersonnal(true, idstrongox);
                IStrongboxContentWalletHardware hardware = new IStrongboxContentWalletHardware();
                cont.walletHardware = hardware.GetListStrongboxContentWalletHardwareByIdSbpersonnal(true, idstrongox);
                if (cont.walletDex.Count < 1 && cont.walletCex.Count < 1 && cont.walletMobile.Count < 1 && cont.walletDesktop.Count < 1 && cont.walletHardware.Count < 1)
                {
                    u.content = null;
                }
                else
                {
                    u.content = cont;
                }

            }

            return u;
        }

        public List<IStrongboxPersonnal> GetListPersonnalStrongboxByIdUser(string idUser)
        {
            List<IStrongboxPersonnal> lu = new List<IStrongboxPersonnal>();
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand(" SELECT distinct   STRONGBOXPERSONNAL.* "
                                      + " FROM            STRONGBOXPERSONNAL INNER JOIN"
                                      + " CUSTOMER ON STRONGBOXPERSONNAL.ID_CUSTOMER = CUSTOMER.ID_CUSTOMER INNER JOIN"
                                      + " _USER ON CUSTOMER.ID_USER = _USER.ID_USER where _user.ID_USER ='" + idUser + "' AND STRONGBOXPERSONNAL.ACTIVE=1  order by LABEL ASC ", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "SB");



            if (ds.Tables["SB"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();
                for (int i = 0; i < ds.Tables["SB"].Rows.Count; i++)
                {

                    IStrongboxPersonnal u = new IStrongboxPersonnal();
                    string idstrongox = ds.Tables["SB"].Rows[i]["ID_STRONGBOXPERSONNAL"].ToString();
                    u.id = cr.EncryptHexa(ds.Tables["SB"].Rows[i]["ID_STRONGBOXPERSONNAL"].ToString());
                    u.idOwner = cr.EncryptHexa(ds.Tables["SB"].Rows[i]["ID_CUSTOMER"].ToString());
                    u.label = ds.Tables["SB"].Rows[i]["LABEL"].ToString();
                    u.walletPublicKeyOwner = ds.Tables["SB"].Rows[i]["WALLETPUBLICKEY"].ToString();
                    u.secretPK = ds.Tables["SB"].Rows[i]["USER_SECRETPK"].ToString();
                    u.payingPK = ds.Tables["SB"].Rows[i]["USER_PAYINGPK"].ToString();
                    u.solanaPK = ds.Tables["SB"].Rows[i]["USER_SOLANAPK"].ToString();
                    u.scPK = ds.Tables["SB"].Rows[i]["SCPK"].ToString();
                    u.codeID = ds.Tables["SB"].Rows[i]["CODEID"].ToString();
                    u.solSereNftPK = ds.Tables["SB"].Rows[i]["NFT_SHARD_SERENITY"].ToString();
                    u.solUsrNftPK = ds.Tables["SB"].Rows[i]["NFT_SHARD_USER"].ToString();
                    u.solHeirNftPKs = ds.Tables["SB"].Rows[i]["NFT_SHARD_HEIR"].ToString();
                    u.dateAdded = ds.Tables["SB"].Rows[i]["DATEADDED"].ToString();

                    content cont = new content();
                    IStrongboxContentWalletDex dex = new IStrongboxContentWalletDex();
                    cont.walletDex = dex.GetListStrongboxContentWalletDexByIdSbpersonnal(true, idstrongox);
                    IStrongboxContentWalletCex cex = new IStrongboxContentWalletCex();
                    cont.walletCex = cex.GetListStrongboxContentWalletCexByIdSbpersonnal(true, idstrongox);
                    IStrongboxContentWalletMobile mob = new IStrongboxContentWalletMobile();
                    cont.walletMobile = mob.GetListStrongboxContentWalletMobileByIdSbpersonnal(true, idstrongox);
                    IStrongboxContentWalletDesktop desk = new IStrongboxContentWalletDesktop();
                    cont.walletDesktop = desk.GetListStrongboxContentWalletDesktopByIdSbpersonnal(true, idstrongox);
                    IStrongboxContentWalletHardware hardware = new IStrongboxContentWalletHardware();
                    cont.walletHardware = hardware.GetListStrongboxContentWalletHardwareByIdSbpersonnal(true, idstrongox);
                    if (cont.walletDex.Count < 1 && cont.walletCex.Count < 1 && cont.walletMobile.Count < 1 && cont.walletDesktop.Count < 1 && cont.walletHardware.Count < 1)
                    {
                        u.content = null;
                    }
                    else
                    {
                        u.content = cont;
                    }
                    lu.Add(u);
                }
            }

            return lu;
        }
    }
}
