using API_SerenityShield.Models.DAO;
using System.Data;
using System.Data.OleDb;

namespace API_SerenityShield.Models.DAO
{

    //export enum EnumStrongboxContentType
    //{
    //    walletDex, // ex : Phantom, Keplr, Metamask
    //    walletCex, // ex : Bitfinex, CoinBase
    //    walletMobile, // ex : Bridge Wallet, Edge, Trust Wallet
    //    walletDesktop, // ex : Ledger Live, Exodus, Atomic
    //    walletHardware, // ex : Ledger Key
    //    simCard,
    //    mobilePhone,
    //    emailBox,
    //    contactDetail,
    //    freeText,
    //    strongboxPhysical, // ex : Real strongbox in owner's housse (to share the code to unlock)
    //    bankAccount, // ex : Credit Agricole...
    //    socialAccount, // ex : Facebook, Insta...
    //    virtualBankAccount, // ex : Lydia, Stripe, Paypal
    //    insurance, // ex: life insurance, credit insurance, accident of life
    //  //}

    //  interface IStrongboxContentBaseApi
    //  {
    //      id: string;
    //label: string;
    //type: EnumStrongboxContentTypeApi;
    //dateAdded: Date;
    //dateUpdated: Date;
    //twoFA: null|EnumTwoFactorTypeApi
    //  }

    //    export interface IStrongboxBaseApi
    //    {
    //        id?: string;
    //  idOwner?: string;
    //  walletPublicKeyOwner: string; // Wallet used to paid the strongboxe creation and to manage the content
    //  label: string;
    //  scPK : string;
    //  codeID : string;
    //  payingPK : string;
    //  secretPK : string;
    //  solanaPK : string;
    //  solUsrNftPK : string;
    //  solSereNftPK : string;
    //  solHeirNftPKs : string;
    //  content: IStrongboxContentBaseApi[];
    //}





    public class IStrongboxContentWalletDex
    {
        public string id { get; set; }
        public string idStrongbox { get; set; }
        public string label { get; set; }
        public TypeWallet? type { get; set; }

        public string dateAdded { get; set; }
        public string dateUpdated { get; set; }
        public string seed { get; set; } // words list separeted by comma
        public string provider { get; set; }
        public IStrongboxContentWalletDex() { }
        public string InsertStrongboxContentWalletDex(bool personnal, string idStrongbox, string label, string idTypeWalletDex, string seed,string provider)
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
                if (personnal == true)
                {

                        commDB = new OleDbCommand("INSERT INTO [dbo].[StrongboxContentWalletDex]([ID_STRONGBOXPERSONNAL],[LABEL],[IDTYPEWALLET],[DATEADDED],[SEED],[ACTIVE],[PROVIDER])"

                        + " OUTPUT INSERTED.ID_StrongboxContentWalletDex"
                        + " VALUES ('" + idStrongbox + "','" + label + "','" + idTypeWalletDex + "','" + dateNow + "','" + seed + "',1,'" + provider + "')", connexDB);

                }
                else
                {
                        commDB = new OleDbCommand("INSERT INTO [dbo].[StrongboxContentWalletDex]([ID_STRONGBOXFORHEIR],[LABEL],[IDTYPEWALLET],[DATEADDED],[SEED],[ACTIVE],[PROVIDER])"

                           + " OUTPUT INSERTED.ID_StrongboxContentWalletDex"
                           + " VALUES ('" + idStrongbox + "','" + label + "','" + idTypeWalletDex + "','" + dateNow + "','" + seed + "',1,'" + provider + "')", connexDB);
                }
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
        public string UpdateLabelStrongboxContentWalletDex(string idstrongbox, string label)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[StrongboxContentWalletDex]"
                    + "  SET [LABEL] = '" + label + "' "
                    + " WHERE ID_StrongboxContentWalletDex" + idstrongbox + "", connexDB);

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
        public string DesactiveStrongboxContentWalletDex(string idstrongbox)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[StrongboxContentWalletDex]"
                    + "  SET [ACTIVE] = 0 "
                    + " WHERE ID_StrongboxContentWalletDex=" + idstrongbox + "", connexDB);

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
        public IStrongboxContentWalletDex GetStrongboxContentWalletDexById(string idSbDex)
        {
            IStrongboxContentWalletDex u;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT * FROM[dbo].[STRONGBOXCONTENTWALLETDEX] WHERE ID_STRONGBOXCONTENTWALLETDEX='" + idSbDex + "' AND ACTIVE=1", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "DEX");

            u = new IStrongboxContentWalletDex();

            if (ds.Tables["DEX"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();

                u.id = cr.EncryptHexa(ds.Tables["DEX"].Rows[0]["ID_STRONGBOXCONTENTWALLETDEX"].ToString());
                string idtype = ds.Tables["DEX"].Rows[0]["IDTYPEWALLET"].ToString();
                u.idStrongbox = !string.IsNullOrEmpty(ds.Tables["DEX"].Rows[0]["ID_STRONGBOXPERSONNAL"].ToString()) ? cr.EncryptHexa(ds.Tables["DEX"].Rows[0]["ID_STRONGBOXPERSONNAL"].ToString()) : cr.EncryptHexa(ds.Tables["DEX"].Rows[0]["ID_STRONGBOXFORHEIR"].ToString());
                u.label = ds.Tables["DEX"].Rows[0]["LABEL"].ToString();
                u.dateAdded = ds.Tables["DEX"].Rows[0]["DATEADDED"].ToString();
                u.dateUpdated = ds.Tables["DEX"].Rows[0]["DATEUPDATED"].ToString();
                u.seed = ds.Tables["DEX"].Rows[0]["SEED"].ToString();
                u.provider = ds.Tables["DEX"].Rows[0]["PROVIDER"].ToString();
                TypeWallet tp = new TypeWallet();
                if (!string.IsNullOrEmpty(idtype))
                {
                    tp = tp.GetTypeWallet(idtype);
                    u.type = tp;
                }
                else
                {
                    u.type = null;
                }
            }

            return u;
        }

        public List<IStrongboxContentWalletDex> GetListStrongboxContentWalletDexByIdSbpersonnal(bool personnal, string idstrongbox)
        {
            List<IStrongboxContentWalletDex> lu = new List<IStrongboxContentWalletDex>();

            IStrongboxContentWalletDex u;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            if (personnal == true)
            {
                commDB = new OleDbCommand("SELECT * FROM[dbo].[STRONGBOXCONTENTWALLETDEX] WHERE ID_STRONGBOXPERSONNAL='" + idstrongbox + "' AND ACTIVE=1  order by LABEL ASC", connexDB);

            }
            else
            {
                commDB = new OleDbCommand("SELECT * FROM[dbo].[STRONGBOXCONTENTWALLETDEX] WHERE ID_STRONGBOXFORHEIR='" + idstrongbox + "' AND ACTIVE=1  order by LABEL ASC", connexDB);

            }
            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "DEX");



            if (ds.Tables["DEX"].Rows.Count > 0)
            {

                Cryptage cr = new Cryptage();
                for (int i = 0; i < ds.Tables["DEX"].Rows.Count; i++)
                {
                    u = new IStrongboxContentWalletDex();

                    u.id = cr.EncryptHexa(ds.Tables["DEX"].Rows[i]["ID_STRONGBOXCONTENTWALLETDEX"].ToString());
                    string idtype = ds.Tables["DEX"].Rows[i]["IDTYPEWALLET"].ToString();
                    if (personnal)
                    {
                        u.idStrongbox = cr.EncryptHexa(ds.Tables["DEX"].Rows[i]["ID_STRONGBOXPERSONNAL"].ToString());
                    }
                    else
                    {
                        u.idStrongbox = cr.EncryptHexa(ds.Tables["DEX"].Rows[i]["ID_STRONGBOXFORHEIR"].ToString());
                    }
                    u.label = ds.Tables["DEX"].Rows[i]["LABEL"].ToString();
                    u.dateAdded = ds.Tables["DEX"].Rows[i]["DATEADDED"].ToString();
                    u.dateUpdated = ds.Tables["DEX"].Rows[i]["DATEUPDATED"].ToString();
                    u.seed = ds.Tables["DEX"].Rows[i]["SEED"].ToString();
                    u.provider = ds.Tables["DEX"].Rows[i]["PROVIDER"].ToString();
                    TypeWallet tp = new TypeWallet();
                    if (!string.IsNullOrEmpty(idtype))
                    {
                        tp = tp.GetTypeWallet(idtype);
                        u.type = tp;
                    }
                    else
                    {
                        u.type = null;
                    }
                    lu.Add(u);
                }
            }
            return lu;
        }


    }



    public class IStrongboxContentWalletCex
    {
        public string id { get; set; }
        public string idStrongbox { get; set; }
        public string label { get; set; }
        public TypeWallet? type { get; set; }

        public string dateAdded { get; set; }
        public string dateUpdated { get; set; }
        public string provider { get; set; }
        public IStrongboxContentWalletCex() { }
        public string InsertStrongboxContentWalletCex(bool personnal, string idStrongbox, string label, string idTypeWalletCex,string provider)
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
                if (personnal == true)
                {
                        commDB = new OleDbCommand("INSERT INTO [dbo].[StrongboxContentWalletCex]([ID_STRONGBOXPERSONNAL],[LABEL],[IDTYPEWALLET],[DATEADDED],[ACTIVE],[PROVIDER])"

                         + " OUTPUT INSERTED.ID_StrongboxContentWalletCex"
                         + " VALUES ('" + idStrongbox + "','" + label + "','" + idTypeWalletCex + "','" + dateNow + "',1,'" + provider + "')", connexDB);
                }

                else
                {
                        commDB = new OleDbCommand("INSERT INTO [dbo].[StrongboxContentWalletCex]([ID_STRONGBOXFORHEIR],[LABEL],[IDTYPEWALLET],[DATEADDED],[ACTIVE],[PROVIDER])"

                        + " OUTPUT INSERTED.ID_StrongboxContentWalletCex"
                        + " VALUES ('" + idStrongbox + "','" + label + "','" + idTypeWalletCex + "','" + dateNow + "',1,'" + provider + "')", connexDB);
                }
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
        public string UpdateStrongboxContentWalletCex(string idstrongbox, string label)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[StrongboxContentWalletCex]"
                    + "  SET [LABEL] = '" + label + "' "

                    + " WHERE ID_StrongboxContentWalletCex" + idstrongbox + "", connexDB);

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
        public string DesactiveStrongboxContentWalletCex(string idstrongbox)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[StrongboxContentWalletCex]"
                    + "  SET [ACTIVE] = 0 "
                    + " WHERE ID_StrongboxContentWalletCex=" + idstrongbox + "", connexDB);

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
        public IStrongboxContentWalletCex GetStrongboxContentWalletCexById(string idSbCex)
        {
            IStrongboxContentWalletCex u;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT * FROM[dbo].[STRONGBOXCONTENTWALLETCEX] WHERE ID_STRONGBOXCONTENTWALLETCEX='" + idSbCex + "' AND ACTIVE=1", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "CEX");

            u = new IStrongboxContentWalletCex();

            if (ds.Tables["CEX"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();

                u.id = cr.EncryptHexa(ds.Tables["CEX"].Rows[0]["ID_STRONGBOXCONTENTWALLETCEX"].ToString());
                string idtype = ds.Tables["CEX"].Rows[0]["IDTYPEWALLET"].ToString();
                u.idStrongbox = !string.IsNullOrEmpty(ds.Tables["CEX"].Rows[0]["ID_STRONGBOXPERSONNAL"].ToString()) ? cr.EncryptHexa(ds.Tables["CEX"].Rows[0]["ID_STRONGBOXPERSONNAL"].ToString()) : ds.Tables["CEX"].Rows[0]["ID_STRONGBOXFORHEIR"].ToString();
                u.label = ds.Tables["CEX"].Rows[0]["LABEL"].ToString();
                u.dateAdded = ds.Tables["CEX"].Rows[0]["DATEADDED"].ToString();
                u.dateUpdated = ds.Tables["CEX"].Rows[0]["DATEUPDATED"].ToString();
                u.provider = ds.Tables["CEX"].Rows[0]["PROVIDER"].ToString();

                TypeWallet tp = new TypeWallet();
                if (!string.IsNullOrEmpty(idtype))
                {
                    tp = tp.GetTypeWallet(idtype);
                    u.type = tp;
                }
                else
                {
                    u.type = null;
                }

            }

            return u;
        }
        public List<IStrongboxContentWalletCex> GetListStrongboxContentWalletCexByIdSbpersonnal(bool personnal, string idstrongbox)
        {
            List<IStrongboxContentWalletCex> lu = new List<IStrongboxContentWalletCex>();
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            if (personnal == true)
            {
                commDB = new OleDbCommand("SELECT * FROM[dbo].[STRONGBOXCONTENTWALLETCEX] WHERE ID_STRONGBOXPERSONNAL='" + idstrongbox + "' AND ACTIVE=1  order by LABEL ASC", connexDB);

            }
            else
            {
                commDB = new OleDbCommand("SELECT * FROM[dbo].[STRONGBOXCONTENTWALLETCEX] WHERE ID_STRONGBOXFORHEIR='" + idstrongbox + "' AND ACTIVE=1  order by LABEL ASC", connexDB);

            }
            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "CEX");



            if (ds.Tables["CEX"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();
                for (int i = 0; i < ds.Tables["CEX"].Rows.Count; i++)
                {
                    IStrongboxContentWalletCex u = new IStrongboxContentWalletCex();

                    u.id = cr.EncryptHexa(ds.Tables["CEX"].Rows[i]["ID_STRONGBOXCONTENTWALLETCEX"].ToString());
                    string idtype = ds.Tables["CEX"].Rows[i]["IDTYPEWALLET"].ToString();
                    if (personnal == true)
                    {
                        u.idStrongbox = cr.EncryptHexa(ds.Tables["CEX"].Rows[i]["ID_STRONGBOXPERSONNAL"].ToString());
                    }
                    else
                    {
                        u.idStrongbox = cr.EncryptHexa(ds.Tables["CEX"].Rows[i]["ID_STRONGBOXFORHEIR"].ToString());
                    }
                    u.label = ds.Tables["CEX"].Rows[i]["LABEL"].ToString();
                    u.dateAdded = ds.Tables["CEX"].Rows[i]["DATEADDED"].ToString();
                    u.dateUpdated = ds.Tables["CEX"].Rows[i]["DATEUPDATED"].ToString();
                    u.provider = ds.Tables["CEX"].Rows[i]["PROVIDER"].ToString();

                    TypeWallet tp = new TypeWallet();
                    if (!string.IsNullOrEmpty(idtype))
                    {
                        tp = tp.GetTypeWallet(idtype);
                        u.type = tp;
                    }
                    else
                    {
                        u.type = null;
                    }
                    lu.Add(u);
                }
            }

            return lu;
        }
    }

    public class IStrongboxContentWalletMobile
    {
        public string id { get; set; }
        public string idStrongbox { get; set; }
        public string label { get; set; }
        public TypeWallet? type { get; set; }

        public string dateAdded { get; set; }
        public string dateUpdated { get; set; }
        public string provider { get; set; }
        public IStrongboxContentWalletMobile() { }
        public string InsertStrongboxContentWalletMobile(bool personnal, string idStrongbox, string label, string idTypeWalletMobile,string provider)
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
                if (personnal)
                {
                        commDB = new OleDbCommand("INSERT INTO [dbo].[StrongboxContentWalletMobile]([ID_STRONGBOXPERSONNAL],[LABEL],[IDTYPEWALLET],[DATEADDED],[ACTIVE],[PROVIDER])"

                              + " OUTPUT INSERTED.ID_StrongboxContentWalletMobile"
                              + " VALUES ('" + idStrongbox + "','" + label + "','" + idTypeWalletMobile + "','" + dateNow + "',1,'" + provider + "')", connexDB);
                }

                else
                {
                        commDB = new OleDbCommand("INSERT INTO [dbo].[StrongboxContentWalletMobile]([ID_STRONGBOXFORHEIR],[LABEL],[IDTYPEWALLET],[DATEADDED],[ACTIVE],[PROVIDER])"

                              + " OUTPUT INSERTED.ID_StrongboxContentWalletMobile"
                              + " VALUES ('" + idStrongbox + "','" + label + "','" + idTypeWalletMobile + "','" + dateNow + "',1,'" + provider + "')", connexDB);
                }

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
        public string UpdateStrongboxContentWalletMobile(string idstrongbox, string label)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[StrongboxContentWalletMobile]"
                    + "  SET [LABEL] = '" + label + "' "

                    + " WHERE id_StrongboxContentWalletMobile" + idstrongbox + "", connexDB);

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
        public string DesactiveStrongboxContentWalletMobile(string idstrongbox)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[StrongboxContentWalletMobile]"
                    + "  SET [ACTIVE] = 0 "
                    + " WHERE ID_StrongboxContentWalletMobile=" + idstrongbox + "", connexDB);

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
        public IStrongboxContentWalletMobile GetStrongboxContentWalletMobileById(string idSbMobile)
        {
            IStrongboxContentWalletMobile u;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT * FROM[dbo].[STRONGBOXCONTENTWALLETMOBILE] WHERE ID_STRONGBOXCONTENTWALLETMOBILE='" + idSbMobile + "' AND ACTIVE=1 ", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "MOBILE");

            u = new IStrongboxContentWalletMobile();

            if (ds.Tables["MOBILE"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();

                u.id = cr.EncryptHexa(ds.Tables["MOBILE"].Rows[0]["ID_STRONGBOXCONTENTWALLETMOBILE"].ToString());
                string idtype = ds.Tables["MOBILE"].Rows[0]["IDTYPEWALLET"].ToString();
                u.idStrongbox = !string.IsNullOrEmpty(ds.Tables["MOBILE"].Rows[0]["ID_STRONGBOXPERSONNAL"].ToString()) ? cr.EncryptHexa(ds.Tables["MOBILE"].Rows[0]["ID_STRONGBOXPERSONNAL"].ToString()) : cr.EncryptHexa(ds.Tables["MOBILE"].Rows[0]["ID_STRONGBOXFORHEIR"].ToString());
                u.label = ds.Tables["MOBILE"].Rows[0]["LABEL"].ToString();
                u.dateAdded = ds.Tables["MOBILE"].Rows[0]["DATEADDED"].ToString();
                u.dateUpdated = ds.Tables["MOBILE"].Rows[0]["DATEUPDATED"].ToString();
                u.provider = ds.Tables["MOBILE"].Rows[0]["PROVIDER"].ToString();
                TypeWallet tp = new TypeWallet();
                if (!string.IsNullOrEmpty(idtype))
                {
                    tp = tp.GetTypeWallet(idtype);
                    u.type = tp;
                }
                else
                {
                    u.type = null;
                }

            }

            return u;
        }
        public List<IStrongboxContentWalletMobile> GetListStrongboxContentWalletMobileByIdSbpersonnal(bool personnal, string idstrongbox)
        {
            List<IStrongboxContentWalletMobile> lu = new List<IStrongboxContentWalletMobile>();
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            if (personnal == true)
            {
                commDB = new OleDbCommand("SELECT * FROM[dbo].[STRONGBOXCONTENTWALLETMOBILE] WHERE ID_STRONGBOXPERSONNAL='" + idstrongbox + "' AND ACTIVE=1  order by LABEL ASC", connexDB);

            }
            else
            {
                commDB = new OleDbCommand("SELECT * FROM[dbo].[STRONGBOXCONTENTWALLETMOBILE] WHERE ID_STRONGBOXFORHEIR='" + idstrongbox + "' AND ACTIVE=1  order by LABEL ASC", connexDB);
            }
            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "MOBILE");



            if (ds.Tables["MOBILE"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();
                for (int i = 0; i < ds.Tables["MOBILE"].Rows.Count; i++)
                {
                    IStrongboxContentWalletMobile u = new IStrongboxContentWalletMobile();

                    u.id = cr.EncryptHexa(ds.Tables["MOBILE"].Rows[i]["ID_STRONGBOXCONTENTWALLETMOBILE"].ToString());
                    string idtype = ds.Tables["MOBILE"].Rows[i]["IDTYPEWALLET"].ToString();
                    if (personnal == true)
                    {
                        u.idStrongbox = cr.EncryptHexa(ds.Tables["MOBILE"].Rows[i]["ID_STRONGBOXPERSONNAL"].ToString());

                    }
                    else
                    {
                        u.idStrongbox = cr.EncryptHexa(ds.Tables["MOBILE"].Rows[i]["ID_STRONGBOXFORHEIR"].ToString());

                    }
                    u.label = ds.Tables["MOBILE"].Rows[i]["LABEL"].ToString();
                    u.dateAdded = ds.Tables["MOBILE"].Rows[i]["DATEADDED"].ToString();
                    u.dateUpdated = ds.Tables["MOBILE"].Rows[i]["DATEUPDATED"].ToString();
                    u.provider = ds.Tables["MOBILE"].Rows[i]["PROVIDER"].ToString();

                    TypeWallet tp = new TypeWallet();
                    if (!string.IsNullOrEmpty(idtype))
                    {
                        tp = tp.GetTypeWallet(idtype);
                        u.type = tp;
                    }
                    else
                    {
                        u.type = null;
                    }
                    lu.Add(u);
                }
            }

            return lu;
        }
    }
    public class IStrongboxContentWalletDesktop
    {
        public string id { get; set; }
        public string idStrongbox { get; set; }
        public string label { get; set; }
        public TypeWallet? type { get; set; }
        public string dateAdded { get; set; }
        public string dateUpdated { get; set; }
        public string provider { get; set; }
        public IStrongboxContentWalletDesktop() { }
        public string InsertStrongboxContentWalletDesktop(bool personnal, string idStrongbox,  string label, string idTypeWalletDesktop,string provider)
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
                if (personnal == true)
                {
                        commDB = new OleDbCommand("INSERT INTO [dbo].[StrongboxContentWalletDesktop]([ID_STRONGBOXPERSONNAL],[LABEL],[IDTYPEWALLET],[DATEADDED],[ACTIVE],[PROVIDER])"

                                                                    + " OUTPUT INSERTED.ID_StrongboxContentWalletDesktop"
                                                                    + " VALUES ('" + idStrongbox + "','" + label + "','" + idTypeWalletDesktop + "','" + dateNow + "',1,'" + provider + "')", connexDB);
                }
                else 
                   
                {


                        commDB = new OleDbCommand("INSERT INTO [dbo].[StrongboxContentWalletDesktop]([ID_STRONGBOXFORHEIR],[LABEL],[IDTYPEWALLET],[DATEADDED],[ACTIVE],[PROVIDER])"

                                                 + " OUTPUT INSERTED.ID_StrongboxContentWalletDesktop"
                                                 + " VALUES ('" + idStrongbox + "','" + label + "','" + idTypeWalletDesktop + "','" + dateNow + "',1,'" + provider + "')", connexDB);
                }
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
        public string UpdateStrongboxContentWalletDesktop(string idstrongbox, string label)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[StrongboxContentWalletDesktop]"
                    + "  SET [LABEL] = '" + label + "' "

                    + " WHERE id_StrongboxContentWalletDesktop" + idstrongbox + "", connexDB);

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
        public string DesactiveStrongboxContentWalletDesktop(string idstrongbox)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[StrongboxContentWalletDesktop]"
                    + "  SET [ACTIVE] = 0 "
                    + " WHERE ID_StrongboxContentWalletDesktop=" + idstrongbox + "", connexDB);

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
        public IStrongboxContentWalletDesktop GetStrongboxContentWalletDesktopById(string idSbDesktop)
        {
            IStrongboxContentWalletDesktop u;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT * FROM[dbo].[STRONGBOXCONTENTWALLETDESKTOP] WHERE ID_STRONGBOXCONTENTWALLETDESKTOP='" + idSbDesktop + "' AND ACTIVE=1", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "DESKTOP");

            u = new IStrongboxContentWalletDesktop();

            if (ds.Tables["DESKTOP"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();

                u.id = cr.EncryptHexa(ds.Tables["DESKTOP"].Rows[0]["ID_STRONGBOXCONTENTWALLETDESKTOP"].ToString());
                string idtype = ds.Tables["DESKTOP"].Rows[0]["IDTYPEWALLET"].ToString();
                u.idStrongbox = !string.IsNullOrEmpty(ds.Tables["DESKTOP"].Rows[0]["ID_STRONGBOXPERSONNAL"].ToString()) ? cr.EncryptHexa(ds.Tables["DESKTOP"].Rows[0]["ID_STRONGBOXPERSONNAL"].ToString()) : cr.EncryptHexa(ds.Tables["DESKTOP"].Rows[0]["ID_STRONGBOXFORHEIR"].ToString());
                u.label = ds.Tables["DESKTOP"].Rows[0]["LABEL"].ToString();
                u.dateAdded = ds.Tables["DESKTOP"].Rows[0]["DATEADDED"].ToString();
                u.dateUpdated = ds.Tables["DESKTOP"].Rows[0]["DATEUPDATED"].ToString();
                u.provider = ds.Tables["DESKTOP"].Rows[0]["PROVIDER"].ToString();
                TypeWallet tp = new TypeWallet();
                if (!string.IsNullOrEmpty(idtype))
                {
                    tp = tp.GetTypeWallet(idtype);
                    u.type = tp;
                }
                else
                {
                    u.type = null;
                }

            }

            return u;
        }
        public List<IStrongboxContentWalletDesktop> GetListStrongboxContentWalletDesktopByIdSbpersonnal(bool personnal, string idstrongbox)
        {
            List<IStrongboxContentWalletDesktop> lu = new List<IStrongboxContentWalletDesktop>();
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            if (personnal == true)
            {
                commDB = new OleDbCommand("SELECT * FROM[dbo].[STRONGBOXCONTENTWALLETDESKTOP] WHERE ID_STRONGBOXPERSONNAL='" + idstrongbox + "' AND ACTIVE=1  order by LABEL ASC ", connexDB);
            }
            else
            {
                commDB = new OleDbCommand("SELECT * FROM[dbo].[STRONGBOXCONTENTWALLETDESKTOP] WHERE ID_STRONGBOXFORHEIR='" + idstrongbox + "' AND ACTIVE=1  order by LABEL ASC", connexDB);

            }

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "DESKTOP");



            if (ds.Tables["DESKTOP"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();
                for (int i = 0; i < ds.Tables["DESKTOP"].Rows.Count; i++)
                {
                    IStrongboxContentWalletDesktop u = new IStrongboxContentWalletDesktop();

                    u.id = cr.EncryptHexa(ds.Tables["DESKTOP"].Rows[i]["ID_STRONGBOXCONTENTWALLETDESKTOP"].ToString());
                    string idtype = ds.Tables["DESKTOP"].Rows[i]["IDTYPEWALLET"].ToString();
                    if (personnal == true)
                    {
                        u.idStrongbox = cr.EncryptHexa(ds.Tables["DESKTOP"].Rows[i]["ID_STRONGBOXPERSONNAL"].ToString());

                    }
                    else
                    {
                        u.idStrongbox = cr.EncryptHexa(ds.Tables["DESKTOP"].Rows[i]["ID_STRONGBOXFORHEIR"].ToString());
                    }
                    u.label = ds.Tables["DESKTOP"].Rows[i]["LABEL"].ToString();
                    u.dateAdded = ds.Tables["DESKTOP"].Rows[i]["DATEADDED"].ToString();
                    u.dateUpdated = ds.Tables["DESKTOP"].Rows[i]["DATEUPDATED"].ToString();
                    u.provider = ds.Tables["DESKTOP"].Rows[i]["PROVIDER"].ToString();

                    TypeWallet tp = new TypeWallet();
                    if (!string.IsNullOrEmpty(idtype))
                    {
                        tp = tp.GetTypeWallet(idtype);
                        u.type = tp;
                    }
                    else
                    {
                        u.type = null;
                    }
                  //  ITwoFactor tf = new ITwoFactor();
                    lu.Add(u);
                }
            }

            return lu;
        }
    }

    public class IStrongboxContentWalletHardware
    {
        public string id { get; set; }
        public string idStrongbox { get; set; }
        public string label { get; set; }
        public TypeWallet? type { get; set; }
        public string dateAdded { get; set; }
        public string dateUpdated { get; set; }
        public string provider { get; set; }
        public IStrongboxContentWalletHardware() { }
        public string InsertStrongboxContentWalletHardware(bool personnal, string idStrongbox, string label, string idTypeWalletHardware,string provider)
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
                if (personnal == true)
                {
                    commDB = new OleDbCommand("INSERT INTO [dbo].[StrongboxContentWalletHardware]([ID_STRONGBOXPERSONNAL],[LABEL],[IDTYPEWALLET],[DATEADDED],[ACTIVE],[PROVIDER])"

                                    + " OUTPUT INSERTED.ID_StrongboxContentWalletHardware "
                                    + " VALUES ('" + idStrongbox + "','" + label + "','" + idTypeWalletHardware + "','" + dateNow + "',1,'"+provider+"')", connexDB);
                }
                else
                {


                    commDB = new OleDbCommand("INSERT INTO [dbo].[StrongboxContentWalletHardware]([ID_STRONGBOXFORHEIR],[LABEL],[IDTYPEWALLET],[DATEADDED],[ACTIVE],[PROVIDER])"

                                + " OUTPUT INSERTED.ID_StrongboxContentWalletHardware "
                                + " VALUES ('" + idStrongbox + "','" + label + "','" + idTypeWalletHardware + "','" + dateNow + "',1,'" + provider + "')", connexDB);
                }
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
        public string UpdateStrongboxContentWalletHardware(string idstrongbox, string label)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[StrongboxContentWalletHardware]"
                    + "  SET [LABEL] = '" + label + "' "

                    + " WHERE id_StrongboxContentWalletHardware" + idstrongbox + "", connexDB);

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
        public string DesactiveStrongboxContentWalletHardware(string idstrongbox)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[StrongboxContentWalletHardware]"
                    + "  SET [ACTIVE] = 0 "
                    + " WHERE ID_StrongboxContentWalletHardware=" + idstrongbox + "", connexDB);

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
        public IStrongboxContentWalletHardware GetStrongboxContentWalletHardwareById(string idSbDesktop)
        {
            IStrongboxContentWalletHardware u;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT * FROM[dbo].[STRONGBOXCONTENTWALLETHARDWARE] WHERE ID_STRONGBOXCONTENTWALLETHARDWARE='" + idSbDesktop + "' AND ACTIVE=1", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "HARDWARE");

            u = new IStrongboxContentWalletHardware();

            if (ds.Tables["HARDWARE"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();
               
                u.id = cr.EncryptHexa(ds.Tables["HARDWARE"].Rows[0]["ID_STRONGBOXCONTENTWALLETHARDWARE"].ToString());
                string idtype = ds.Tables["HARDWARE"].Rows[0]["IDTYPEWALLET"].ToString();
                u.idStrongbox = !string.IsNullOrEmpty(ds.Tables["HARDWARE"].Rows[0]["ID_STRONGBOXPERSONNAL"].ToString()) ? cr.EncryptHexa(ds.Tables["HARDWARE"].Rows[0]["ID_STRONGBOXPERSONNAL"].ToString()) : cr.EncryptHexa(ds.Tables["HARDWARE"].Rows[0]["ID_STRONGBOXFORHEIR"].ToString());

                u.label = ds.Tables["HARDWARE"].Rows[0]["LABEL"].ToString();
                u.dateAdded = ds.Tables["HARDWARE"].Rows[0]["DATEADDED"].ToString();
                u.dateUpdated = ds.Tables["HARDWARE"].Rows[0]["DATEUPDATED"].ToString();
                u.provider = ds.Tables["HARDWARE"].Rows[0]["PROVIDER"].ToString();
                TypeWallet tp = new TypeWallet();
                if (!string.IsNullOrEmpty(idtype))
                {
                    tp = tp.GetTypeWallet(idtype);
                    u.type = tp;
                }
                else
                {
                    u.type = null;
                }




            }

            return u;
        }

        public List<IStrongboxContentWalletHardware> GetListStrongboxContentWalletHardwareByIdSbpersonnal(bool personnal, string idstrongbox)
        {
            List<IStrongboxContentWalletHardware> lu = new List<IStrongboxContentWalletHardware>();
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            if (personnal == true)
            {
                commDB = new OleDbCommand("SELECT * FROM[dbo].[STRONGBOXCONTENTWALLETHARDWARE] WHERE ID_STRONGBOXPERSONNAL='" + idstrongbox + "' AND ACTIVE=1  order by LABEL ASC ", connexDB);

            }
            else
            {
                commDB = new OleDbCommand("SELECT * FROM[dbo].[STRONGBOXCONTENTWALLETHARDWARE] WHERE ID_STRONGBOXFORHEIR='" + idstrongbox + "' AND ACTIVE=1  order by LABEL ASC", connexDB);

            }
            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "HARDWARE");



            if (ds.Tables["HARDWARE"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();
                for (int i = 0; i < ds.Tables["HARDWARE"].Rows.Count; i++)
                {
                    IStrongboxContentWalletHardware u = new IStrongboxContentWalletHardware();

                    u.id = cr.EncryptHexa(ds.Tables["HARDWARE"].Rows[i]["ID_STRONGBOXCONTENTWALLETHARDWARE"].ToString());
                    string idtype = ds.Tables["HARDWARE"].Rows[i]["IDTYPEWALLET"].ToString();
                    if (personnal)
                    {
                        u.idStrongbox = cr.EncryptHexa(ds.Tables["HARDWARE"].Rows[i]["ID_STRONGBOXPERSONNAL"].ToString());
                    }
                    else
                    {
                        u.idStrongbox = cr.EncryptHexa(ds.Tables["HARDWARE"].Rows[i]["ID_STRONGBOXFORHEIR"].ToString());

                    }
                    u.label = ds.Tables["HARDWARE"].Rows[i]["LABEL"].ToString();
                    u.dateAdded = ds.Tables["HARDWARE"].Rows[i]["DATEADDED"].ToString();
                    u.dateUpdated = ds.Tables["HARDWARE"].Rows[i]["DATEUPDATED"].ToString();
                    u.provider = ds.Tables["HARDWARE"].Rows[i]["PROVIDER"].ToString();

                    TypeWallet tp = new TypeWallet();
                    if (!string.IsNullOrEmpty(idtype))
                    {
                        tp = tp.GetTypeWallet(idtype);
                        u.type = tp;
                    }
                    else
                    {
                        u.type = null;
                    }

                    lu.Add(u);
                }
            }

            return lu;
        }
    }

    public class IStrongboxContentSimCard
    {
        public string id { get; set; }
        public string label { get; set; }
        public TypeData? typeData { get; set; }
        public string dateAdded { get; set; }
        public string dateUpdated { get; set; }

        public string? pin { get; set; }

    }

    public class IStrongboxContentEmailBox
    {
        public string id { get; set; }
        public string label { get; set; }

        public TypeData? typeData { get; set; }
        public string dateAdded { get; set; }
        public string dateUpdated { get; set; }
        public string webmail { get; set; }
        public string email { get; set; }
        public string password { get; set; }
    }

    public class IStrongboxContentContactDetail
    {
        public string id { get; set; }
        public string label { get; set; }
        public TypeData? typeData { get; set; }
        public string dateAdded { get; set; }
        public string dateUpdated { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string? email { get; set; }
        public string? phone { get; set; }
        public string? address { get; set; }
    }

    public class IStrongboxContentFreeText
    {
        public string id { get; set; }
        public string label { get; set; }
        public TypeData? typeData { get; set; }
        public string dateAdded { get; set; }
        public string dateUpdated { get; set; }
        public string text { get; set; }
    }

    public class IStrongboxContentStrongboxPhysical
    {
        public string id { get; set; }
        public string label { get; set; }
        public TypeData? typeData { get; set; }
        public string dateAdded { get; set; }
        public string dateUpdated { get; set; }
        public string code { get; set; }
        public string? physicalKeyLocation { get; set; }
    }

    public class IStrongboxContentBankAccount
    {
        public string id { get; set; }
        public string label { get; set; }

        public TypeData? typeData { get; set; }
        public string dateAdded { get; set; }
        public string dateUpdated { get; set; }
        public string bankName { get; set; }
        public string webInterfaceUrl { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string accountNumber { get; set; }
    }

    public class IStrongboxContentSocialAccount
    {
        public string id { get; set; }
        public string label { get; set; }

        public TypeData? typeData { get; set; }
        public string dateAdded { get; set; }
        public string dateUpdated { get; set; }
        public string socialAccountType { get; set; } // not enum because it's impossible to list all socials accounts arround the world
        public string login { get; set; }
        public string password { get; set; }
     
    }

    public class IStrongboxContentVirtualBankAccount
    {
        public string id { get; set; }
        public string label { get; set; }
        public TypeData? typeData { get; set; }
        public string dateAdded { get; set; }
        public string dateUpdated { get; set; }
        public string accountType { get; set; } // not enum because it's impossible to list all virtuals Banks arround the world
        public string login { get; set; }
        public string password { get; set; }
      
    }

    public class IStrongboxContentInsurance
    {
        public string id { get; set; }
        public string label { get; set; }
        public TypeWallet? type { get; set; }
        public string dateAdded { get; set; }
        public string dateUpdated { get; set; }
        public string insuranceType { get; set; }//'Life insurance'|'Credit insurance'|'Accident of life insurance'|'Credit card insurance'|'Other';
        public string contractNumber { get; set; }
        public string contact { get; set; }
        public string compagny { get; set; }
    }
}
