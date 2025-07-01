using System.Data;
using System.Data.OleDb;

namespace API_SerenityShield.Models.DAO
{
    public class TypeWallet
    {
        public string id { get; set; }
        public string label { get; set; }
        public string type { get; set; }
        public bool supported { get; set; }
        public TypeWallet()
        {
        }

        public List<TypeWallet> GetListSupportedWallet()
        {
            List<TypeWallet> ltw = new List<TypeWallet>();


            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT    DISTINCT    [TYPE_WALLET].*"
                            + " FROM            [TYPE_WALLET]  where supported=1 ORDER BY LABEL ASC", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "TypeWallet");



            if (ds.Tables["TypeWallet"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["TypeWallet"].Rows.Count; i++)
                {

                    TypeWallet h = new TypeWallet();
                    Cryptage cr = new Cryptage();
                    h.id = cr.EncryptHexa(ds.Tables["TypeWallet"].Rows[i]["IDTYPEWALLET"].ToString());
                    h.label =ds.Tables["TypeWallet"].Rows[i]["LABEL"].ToString();
                    h.type = ds.Tables["TypeWallet"].Rows[i]["TYPE"].ToString();
                   
                    h.supported =(bool) ds.Tables["TypeWallet"].Rows[i]["SUPPORTED"];
                    ltw.Add(h);
                }
            }



            return ltw;
        }
        public List<TypeWallet> GetListWalletDex()
        {
            List<TypeWallet> ltw = new List<TypeWallet>();


            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT     DISTINCT   [TYPE_WALLET].*"
                            + " FROM            [TYPE_WALLET]  where TYPE like '%DEX%'ORDER BY LABEL ASC", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "TypeWallet");



            if (ds.Tables["TypeWallet"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["TypeWallet"].Rows.Count; i++)
                {

                    TypeWallet h = new TypeWallet();
                    Cryptage cr = new Cryptage();
                    h.id = cr.EncryptHexa(ds.Tables["TypeWallet"].Rows[i]["IDTYPEWALLET"].ToString());
                    h.label = ds.Tables["TypeWallet"].Rows[i]["LABEL"].ToString();
                    h.type = ds.Tables["TypeWallet"].Rows[i]["TYPE"].ToString();
                  
                    h.supported = (bool)ds.Tables["TypeWallet"].Rows[i]["SUPPORTED"];
                    ltw.Add(h);
                }
            }



            return ltw;
        }
        public List<TypeWallet> GetListWalletCex()
        {
            List<TypeWallet> ltw = new List<TypeWallet>();


            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT   DISTINCT     [TYPE_WALLET].*"
                            + " FROM            [TYPE_WALLET]  where TYPE like '%CEX%' ORDER BY LABEL ASC", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "TypeWallet");



            if (ds.Tables["TypeWallet"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["TypeWallet"].Rows.Count; i++)
                {

                    TypeWallet h = new TypeWallet();
                    Cryptage cr = new Cryptage();
                    h.id = cr.EncryptHexa(ds.Tables["TypeWallet"].Rows[i]["IDTYPEWALLET"].ToString());
                    h.label = ds.Tables["TypeWallet"].Rows[i]["LABEL"].ToString();
                    h.type = ds.Tables["TypeWallet"].Rows[i]["TYPE"].ToString();
                   
                    h.supported = (bool)ds.Tables["TypeWallet"].Rows[i]["SUPPORTED"];
                    ltw.Add(h);
                }
            }



            return ltw;
        }
        public List<TypeWallet> GetListWalletCold()
        {
            List<TypeWallet> ltw = new List<TypeWallet>();


            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT    DISTINCT    [TYPE_WALLET].*"
                            + " FROM            [TYPE_WALLET]  where TYPE like '%COLD HARD WALLET%' ORDER BY LABEL ASC", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "TypeWallet");



            if (ds.Tables["TypeWallet"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["TypeWallet"].Rows.Count; i++)
                {

                    TypeWallet h = new TypeWallet();
                    Cryptage cr = new Cryptage();
                    h.id = cr.EncryptHexa(ds.Tables["TypeWallet"].Rows[i]["IDTYPEWALLET"].ToString());
                    h.label = ds.Tables["TypeWallet"].Rows[i]["LABEL"].ToString();
                    h.type = ds.Tables["TypeWallet"].Rows[i]["TYPE"].ToString();
                 
                    h.supported = (bool)ds.Tables["TypeWallet"].Rows[i]["SUPPORTED"];
                    ltw.Add(h);
                }
            }



            return ltw;
        }
        public List<TypeWallet> GetListWalletDesktop()
        {
            List<TypeWallet> ltw = new List<TypeWallet>();


            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT   DISTINCT     [TYPE_WALLET].*"
                            + " FROM            [TYPE_WALLET]  where TYPE like '%HOT WALLET%' ORDER BY LABEL ASC", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "TypeWallet");



            if (ds.Tables["TypeWallet"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["TypeWallet"].Rows.Count; i++)
                {

                    TypeWallet h = new TypeWallet();
                    Cryptage cr = new Cryptage();
                    h.id = cr.EncryptHexa(ds.Tables["TypeWallet"].Rows[i]["IDTYPEWALLET"].ToString());
                    h.label = ds.Tables["TypeWallet"].Rows[i]["LABEL"].ToString();
                    h.type = ds.Tables["TypeWallet"].Rows[i]["TYPE"].ToString();
                  
                    h.supported = (bool)ds.Tables["TypeWallet"].Rows[i]["SUPPORTED"];
                    ltw.Add(h);
                }
            }



            return ltw;
        }
        public List<TypeWallet> GetListWallet()
        {
            List<TypeWallet> ltw = new List<TypeWallet>();


            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT   DISTINCT     [TYPE_WALLET].*"
                            + " FROM            [TYPE_WALLET]  ORDER BY LABEL ASC", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "TypeWallet");



            if (ds.Tables["TypeWallet"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["TypeWallet"].Rows.Count; i++)
                {

                    TypeWallet h = new TypeWallet();
                    Cryptage cr = new Cryptage();
                    h.id = cr.EncryptHexa(ds.Tables["TypeWallet"].Rows[i]["IDTYPEWALLET"].ToString());
                    h.label = ds.Tables["TypeWallet"].Rows[i]["LABEL"].ToString();
                    h.type = ds.Tables["TypeWallet"].Rows[i]["TYPE"].ToString();
                    h.supported = (bool)ds.Tables["TypeWallet"].Rows[i]["SUPPORTED"];
                    ltw.Add(h);
                }
            }



            return ltw;
        }

        public TypeWallet GetTypeWallet(string idtypewallet)
        {


            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT   DISTINCT     [TYPE_WALLET].*"
                            + " FROM            [TYPE_WALLET] where IDTYPEWALLET ='"+ idtypewallet + "' ORDER BY LABEL ASC", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "TypeWallet");

            TypeWallet h = new TypeWallet();
            Cryptage cr = new Cryptage();

            if (ds.Tables["TypeWallet"].Rows.Count > 0)
            {

                h.id = cr.EncryptHexa(ds.Tables["TypeWallet"].Rows[0]["IDTYPEWALLET"].ToString());
                h.label = ds.Tables["TypeWallet"].Rows[0]["LABEL"].ToString();
                h.type = ds.Tables["TypeWallet"].Rows[0]["TYPE"].ToString();
                h.supported = (bool)ds.Tables["TypeWallet"].Rows[0]["SUPPORTED"];
            }



            return h;
        }
    }
}
public class ISupportedWallet
{
    public string id { get; set; }
    public string label
    {
        get; set;

    }
    public string provider
    {
        get; set;

    }
}