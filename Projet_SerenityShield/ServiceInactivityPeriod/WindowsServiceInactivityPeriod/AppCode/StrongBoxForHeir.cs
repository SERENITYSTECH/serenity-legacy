using ServiceInactivityPeriod.AppCode;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceInactivityPeriod.AppCode
{
    public class IStrongboxForHeirs
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
        public content content { get; set; } 

        public string messageForHeirs { get; set; }
        public List<IHeir> heirs { get; set; }
        public bool displayToHeirs { get; set; }
        public string inactivityPeriod { get; set; }// '7d'|'15d'|'1m'|'3m';
        public string idInactivityPeriod { get; set; }
        public bool prepayedInHeritence { get; set; }
        public string dateAdded { get; set; }
        public string InsertStrongboxForHeir(string idCustomer, string idActivationPolicy,
            string Label, string messageforHeirs, bool display, string listIdHeir,
             string walletPublicKey, string secretPK, string payingPK, string solanaPK,
             string solSereNftPK, string solUsrNftPK, string solHeirNftPKs, string scpk, string codeId,
              bool prepayedInHeritence)
        {
            string CurrentID = string.Empty;

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            string dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString();
            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {
                Cryptage cr = new Cryptage();
                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("INSERT INTO[dbo].[STRONGBOXFORHEIR]"

                           + "([ID_CUSTOMER],[ID_POLICY],[LABEL],[MESSAGE_FOR_HEIRS]"
                          + " ,[DISPLAY],[LIST_OF_HEIRS],[ACTIVE],[WALLETPUBLICKEY],[USER_SECRETPK],[USER_PAYINGPK],[USER_SOLANAPK]"
                          + " ,[NFT_SHARD_SERENITY],[NFT_SHARD_USER],[NFT_SHARD_HEIR]"
                          + " ,[SCPK],[CODEID],[PREPAYEDINHERENCE],[DATEADDED])"

                        + " OUTPUT INSERTED.ID_STRONGBOXFORHEIR"
                        + " VALUES ('" + idCustomer + "','" + idActivationPolicy + "','" + Label + "','" + messageforHeirs.Replace("'", "''") + "',"
                        + " '" + display + "','" + listIdHeir + "',1,'" + walletPublicKey + "', '" + secretPK + "','" + payingPK + "','" + solanaPK + "',"
                        + " '" + solSereNftPK + "','" + solUsrNftPK + "','" + solHeirNftPKs + "','" + scpk + "','" + codeId + "','" + prepayedInHeritence + "','" + dateNow + "')", connexDB);
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





        public string UpdateStrongboxForHeir(string idPolicy, string idstrongboxheir, string label, string listIdHeir, string Message, string display, string prepayedInHeritence)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[STRONGBOXFORHEIR]"
                                          + " SET[ID_POLICY] ='" + idPolicy + "'"

                                          + "    ,[LABEL] ='" + label + "'"
                                          + "    ,[MESSAGE_FOR_HEIRS] ='" + Message + "'"
                                          + "    ,[DISPLAY] = '" + display + "'"
                                           + "   ,[LIST_OF_HEIRS] ='" + listIdHeir + "'"
                                           + "   ,[PREPAYEDINHERENCE] = '" + prepayedInHeritence + "'"

                                            + " WHERE ID_STRONGBOXFORHEIR='" + idstrongboxheir + "'", connexDB);

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
        public string DesactiveStrongboxForHeir(string idstrongbox)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[STRONGBOXFORHEIR]"
                    + "  SET [ACTIVE] = 0 "
                    + " WHERE ID_STRONGBOXFORHEIR=" + idstrongbox + "", connexDB);

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



        public List<IStrongboxForHeirs> GetListStrongboxForHeir()
        {
            List<IStrongboxForHeirs> lu = new List<IStrongboxForHeirs>();
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT     STRONGBOXFORHEIR.* "
                         + " FROM            STRONGBOXFORHEIR"
                         + " where  STRONGBOXFORHEIR.ACTIVE=1  order by LABEL ASC", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "SB");

            Cryptage cr = new Cryptage();

            if (ds.Tables["SB"].Rows.Count > 0)
            {

                for (int i = 0; i < ds.Tables["SB"].Rows.Count; i++)
                {
                    IStrongboxForHeirs u = new IStrongboxForHeirs();

                    string idstrongox = ds.Tables["SB"].Rows[i]["ID_STRONGBOXFORHEIR"].ToString();
                    u.id = ds.Tables["SB"].Rows[i]["ID_STRONGBOXFORHEIR"].ToString();
                    u.idOwner = ds.Tables["SB"].Rows[i]["ID_CUSTOMER"].ToString();
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
                    cont.walletDex = dex.GetListStrongboxContentWalletDexByIdSbpersonnal(false, idstrongox);
                    IStrongboxContentWalletCex cex = new IStrongboxContentWalletCex();
                    cont.walletCex = cex.GetListStrongboxContentWalletCexByIdSbpersonnal(false, idstrongox);
                    IStrongboxContentWalletMobile mob = new IStrongboxContentWalletMobile();
                    cont.walletMobile = mob.GetListStrongboxContentWalletMobileByIdSbpersonnal(false, idstrongox);
                    IStrongboxContentWalletDesktop desk = new IStrongboxContentWalletDesktop();
                    cont.walletDesktop = desk.GetListStrongboxContentWalletDesktopByIdSbpersonnal(false, idstrongox);
                    IStrongboxContentWalletHardware hardware = new IStrongboxContentWalletHardware();
                    cont.walletHardware = hardware.GetListStrongboxContentWalletHardwareByIdSbpersonnal(false, idstrongox);
                    if (cont.walletDex.Count < 1 && cont.walletCex.Count < 1 && cont.walletMobile.Count < 1 && cont.walletDesktop.Count < 1 && cont.walletHardware.Count < 1)
                    {
                        u.content = null;
                    }
                    else
                    {
                        u.content = cont;
                    }
                    u.messageForHeirs = ds.Tables["SB"].Rows[i]["MESSAGE_FOR_HEIRS"].ToString();
                    string listeIdHeir = ds.Tables["SB"].Rows[i]["LIST_OF_HEIRS"].ToString();
                    string[] tabHeir = listeIdHeir.Split(',');
                    List<IHeir> lh = new List<IHeir>();

                    foreach (string item in tabHeir)
                    {
                        if (!string.IsNullOrEmpty(item))
                        {
                            Heir h = new Heir();
                            h = h.GetHeirById(cr.DecryptHexa(item));
                            IHeir ih = new IHeir();

                            ih.idUser = h.IdUser;
                            ih.idHeir = h.IdHeir;
                            ih.lastName = h.LastName;
                            ih.firstName = h.FirstName;
                            ih.email = h.Email;
                            ih.phone = h.PhoneNumber;
                            ih.creation_Date = h.Creation_Date;
                            ih.walletType = h.walletType;
                            ih.publicKey = h.PublicKey;
                            lh.Add(ih);
                        }
                    }
                    u.heirs = lh;
                    u.displayToHeirs = (bool)ds.Tables["SB"].Rows[i]["DISPLAY"];
                    string idperiod = ds.Tables["SB"].Rows[i]["ID_POLICY"].ToString();
                    InactivityPeriod ip = new InactivityPeriod();
                    ip = ip.GetInactivityPeriodById(idperiod);
                    u.inactivityPeriod = ip.period;
                    u.idInactivityPeriod = ip.id;
                    u.prepayedInHeritence = (bool)ds.Tables["SB"].Rows[i]["PREPAYEDINHERENCE"];
                    lu.Add(u);
                }
            }

            return lu;
        }
    }
    public class InactivityPeriod
    {
        public string id { get; set; }
        public string period { get; set; }

        public InactivityPeriod GetInactivityPeriodById(string idtInactivityPeriod)
        {
            InactivityPeriod u;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT * FROM [dbo].[POLICY] WHERE ID_POLICY='" + idtInactivityPeriod + "'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "InactivityPeriod");

            u = new InactivityPeriod();

            if (ds.Tables["InactivityPeriod"].Rows.Count > 0)
            {
             
                u.id = ds.Tables["InactivityPeriod"].Rows[0]["ID_POLICY"].ToString();
                u.period = ds.Tables["InactivityPeriod"].Rows[0]["POLICY_VALUE"].ToString();

            }

            return u;
        }

        public List<InactivityPeriod> GetListInactivityPeriod()
        {
            List<InactivityPeriod> lu = new List<InactivityPeriod>();
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT * FROM [dbo].[POLICY]", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "InactivityPeriod");



            if (ds.Tables["InactivityPeriod"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();
                for (int i = 0; i < ds.Tables["InactivityPeriod"].Rows.Count; i++)
                {
                    InactivityPeriod u = new InactivityPeriod();
                    u.id = ds.Tables["InactivityPeriod"].Rows[i]["ID_POLICY"].ToString();
                    u.period = ds.Tables["InactivityPeriod"].Rows[i]["POLICY_VALUE"].ToString();

                    lu.Add(u);
                }


            }

            return lu;
        }

    }

}
