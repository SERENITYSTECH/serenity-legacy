using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceInactivityPeriod.AppCode
{
    public class Heir
    {

        public string IdUser { get; set; }
        public string IdHeir { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string idTypeWallet { get; set; }
        public string PublicKey { get; set; }
        public TypeWallet walletType { get; set; }
        public string Creation_Date { get; set; }
        public bool IsActive { get; set; }





        public string RemoveHeir(string IdUser)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[HEIR]"
                    + "  SET [HEIR_ACTIVE]=0 "
                    + " WHERE ID_USER='" + IdUser + "'", connexDB);

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

        public Heir GetHeirById(string IdHeir)
        {
            Heir h = new Heir();

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT        [HEIR].*"
                            + " FROM            [HEIR]"
                            + " WHERE  ID_HEIR=" + IdHeir + " ", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "Heir");

            h = new Heir();

            if (ds.Tables["Heir"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();
                h.IdUser = cr.EncryptHexa(ds.Tables["Heir"].Rows[0]["ID_USER"].ToString());
                h.IdHeir = cr.EncryptHexa(ds.Tables["Heir"].Rows[0]["ID_HEIR"].ToString());
                h.LastName = cr.Decrypt(ds.Tables["Heir"].Rows[0]["HEIR_LASTNAME"].ToString());
                h.FirstName = cr.Decrypt(ds.Tables["Heir"].Rows[0]["HEIR_FIRSTNAME"].ToString());
                h.Email = cr.Decrypt(ds.Tables["Heir"].Rows[0]["HEIR_EMAIL"].ToString());
                h.PhoneNumber = cr.Decrypt(ds.Tables["Heir"].Rows[0]["HEIR_PHONENUMBER"].ToString());
                h.IsActive = (bool)ds.Tables["Heir"].Rows[0]["HEIR_ACTIVE"];
                h.Creation_Date = ds.Tables["Heir"].Rows[0]["HEIR_CREATION_DATE"].ToString();
                h.PublicKey = ds.Tables["Heir"].Rows[0]["HEIR_PUBLIC_KEY"].ToString();
                var isNumeric = int.TryParse(ds.Tables["Heir"].Rows[0]["HEIR_ID_TYPE_WALLET"].ToString(), out _);
                if (isNumeric && !string.IsNullOrEmpty(ds.Tables["Heir"].Rows[0]["HEIR_ID_TYPE_WALLET"].ToString()))
                {
                    h.idTypeWallet = cr.EncryptHexa(ds.Tables["Heir"].Rows[0]["HEIR_ID_TYPE_WALLET"].ToString());
                }
                else
                {
                    h.idTypeWallet = null;
                }
                TypeWallet tw = new TypeWallet();
                if (isNumeric && !string.IsNullOrEmpty(ds.Tables["Heir"].Rows[0]["HEIR_ID_TYPE_WALLET"].ToString()))
                {
                    tw = tw.GetTypeWallet(ds.Tables["Heir"].Rows[0]["HEIR_ID_TYPE_WALLET"].ToString());
                    if (tw.id != null)
                    {
                        h.walletType = tw;
                    }
                    else
                    {
                        h.walletType = null;
                    }
                }
            }
            return h;
        }
        public Heir GetHeirByPublicKey(string publickey)
        {
            Heir h = new Heir();

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT        [HEIR].*"
                            + " FROM            [HEIR]"
                            + " WHERE  HEIR_PUBLIC_KEY='" + publickey + "' ", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "Heir");

            h = new Heir();

            if (ds.Tables["Heir"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();
                h.IdUser = ds.Tables["Heir"].Rows[0]["ID_USER"].ToString();
                h.IdHeir = ds.Tables["Heir"].Rows[0]["ID_HEIR"].ToString();
                h.LastName = ds.Tables["Heir"].Rows[0]["HEIR_LASTNAME"].ToString();
                h.FirstName = ds.Tables["Heir"].Rows[0]["HEIR_FIRSTNAME"].ToString();
                h.Email = ds.Tables["Heir"].Rows[0]["HEIR_EMAIL"].ToString();
                h.PhoneNumber = ds.Tables["Heir"].Rows[0]["HEIR_PHONENUMBER"].ToString();
                h.IsActive = (bool)ds.Tables["Heir"].Rows[0]["HEIR_ACTIVE"];
                h.Creation_Date = ds.Tables["Heir"].Rows[0]["HEIR_CREATION_DATE"].ToString();
                h.PublicKey = ds.Tables["Heir"].Rows[0]["HEIR_PUBLIC_KEY"].ToString();
                var isNumeric = int.TryParse(ds.Tables["Heir"].Rows[0]["HEIR_ID_TYPE_WALLET"].ToString(), out _);
                if (isNumeric && !string.IsNullOrEmpty(ds.Tables["Heir"].Rows[0]["HEIR_ID_TYPE_WALLET"].ToString()))
                {
                    h.idTypeWallet = ds.Tables["Heir"].Rows[0]["HEIR_ID_TYPE_WALLET"].ToString();
                }
                else
                {
                    h.idTypeWallet = null;
                }
                TypeWallet tw = new TypeWallet();
                if (isNumeric && !string.IsNullOrEmpty(ds.Tables["Heir"].Rows[0]["HEIR_ID_TYPE_WALLET"].ToString()))
                {
                    tw = tw.GetTypeWallet(ds.Tables["Heir"].Rows[0]["HEIR_ID_TYPE_WALLET"].ToString());
                    if (tw.id != null)
                    {
                        h.walletType = tw;
                    }
                    else
                    {
                        h.walletType = null;
                    }
                }

            }

            return h;
        }
        public List<string> GetListIdHeirbyIdUserCustomer(string IdUserCustomer)
        {
            List<string> listIdHeir = new List<string>();
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT * FROM [dbo].[AFFILIATE] where id_user_customer='" + IdUserCustomer + "' order by ID_HEIR asc", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "Heir");

            for (int i = 0; i < ds.Tables["Heir"].Rows.Count; i++)
            {
                listIdHeir.Add(ds.Tables["Heir"].Rows[i]["ID_HEIR"].ToString());

            }
            connexDB.Close();

            return listIdHeir;
        }
        public List<Heir> GetListHeirByListIdHeir(List<string> listIdHeir)
        {
            List<Heir> ListH = new List<Heir>();
            foreach (string item in listIdHeir)
            {
                Heir h = new Heir();
                h = h.GetHeirById(item);
                ListH.Add(h);
            }

            return ListH;
        }

        public List<Heir> GetListHeirByListIdUserCustomer(string IdUserCustomer)
        {
            List<Heir> ListH = new List<Heir>();


            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT        [HEIR].*"
                            + " FROM            [HEIR]"
                            + " WHERE  ID_HEIR in (SELECT ID_Heir FROM [dbo].[AFFILIATE] where id_user_customer='" + IdUserCustomer + "' )  and heir_active=1  order by HEIR_FIRSTNAME ASC", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "Heir");



            if (ds.Tables["Heir"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["Heir"].Rows.Count; i++)
                {

                    Heir h = new Heir();
                    Cryptage cr = new Cryptage();
                    h.IdUser = ds.Tables["Heir"].Rows[i]["ID_USER"].ToString();
                    h.IdHeir = ds.Tables["Heir"].Rows[i]["ID_HEIR"].ToString();
                    h.LastName = ds.Tables["Heir"].Rows[i]["HEIR_LASTNAME"].ToString();
                    h.FirstName = ds.Tables["Heir"].Rows[i]["HEIR_FIRSTNAME"].ToString();
                    h.Email = ds.Tables["Heir"].Rows[i]["HEIR_EMAIL"].ToString();
                    h.PhoneNumber = ds.Tables["Heir"].Rows[i]["HEIR_PHONENUMBER"].ToString();
                    h.IsActive = (bool)ds.Tables["Heir"].Rows[i]["HEIR_ACTIVE"];
                    h.Creation_Date = ds.Tables["Heir"].Rows[i]["HEIR_CREATION_DATE"].ToString();
                    h.PublicKey = ds.Tables["Heir"].Rows[i]["HEIR_PUBLIC_KEY"].ToString();
                    var isNumeric = int.TryParse(ds.Tables["Heir"].Rows[i]["HEIR_ID_TYPE_WALLET"].ToString(), out _);
                    if (isNumeric && !string.IsNullOrEmpty(ds.Tables["Heir"].Rows[i]["HEIR_ID_TYPE_WALLET"].ToString()))
                    {
                        h.idTypeWallet = ds.Tables["Heir"].Rows[i]["HEIR_ID_TYPE_WALLET"].ToString();
                    }
                    else
                    {
                        h.idTypeWallet = null;
                    }
                    TypeWallet tw = new TypeWallet();

                    if (isNumeric && !string.IsNullOrEmpty(ds.Tables["Heir"].Rows[i]["HEIR_ID_TYPE_WALLET"].ToString()))
                    {
                        tw = tw.GetTypeWallet(ds.Tables["Heir"].Rows[i]["HEIR_ID_TYPE_WALLET"].ToString());
                        if (tw.id != null)
                        {
                            h.walletType = tw;
                        }
                        else
                        {
                            h.walletType = null;
                        }
                    }


                    ListH.Add(h);
                }
            }


            return ListH;
        }
        public List<Heir> GetListHeirByListFirstnameLastnameEmail(string cryptedfirstname, string cryptedlastname, string cryptedemail)
        {
            List<Heir> ListH = new List<Heir>();


            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;

            commDB = new OleDbCommand("SELECT  HEIR.*"
                                     + " FROM HEIR"
                                     + "  where HEIR_FIRSTNAME = '" + cryptedfirstname + "'"
                                     + "  and HEIR_LASTNAME = '" + cryptedlastname + "'"
                                     + "  and HEIR_EMAIL = '" + cryptedemail + "' and HEIR_ACTIVE = 1  order by HEIR_FIRSTNAME ASC", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "Heir");



            if (ds.Tables["Heir"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["Heir"].Rows.Count; i++)
                {

                    Heir h = new Heir();
                    Cryptage cr = new Cryptage();
                    h.IdUser = ds.Tables["Heir"].Rows[i]["ID_USER"].ToString();
                    h.IdHeir = ds.Tables["Heir"].Rows[i]["ID_HEIR"].ToString();
                    h.LastName = ds.Tables["Heir"].Rows[i]["HEIR_LASTNAME"].ToString();
                    h.FirstName = ds.Tables["Heir"].Rows[i]["HEIR_FIRSTNAME"].ToString();
                    h.Email = ds.Tables["Heir"].Rows[i]["HEIR_EMAIL"].ToString();
                    h.PhoneNumber = ds.Tables["Heir"].Rows[i]["HEIR_PHONENUMBER"].ToString();
                    h.IsActive = (bool)ds.Tables["Heir"].Rows[i]["HEIR_ACTIVE"];
                    h.Creation_Date = ds.Tables["Heir"].Rows[i]["HEIR_CREATION_DATE"].ToString();
                    h.PublicKey = ds.Tables["Heir"].Rows[i]["HEIR_PUBLIC_KEY"].ToString();
                    var isNumeric = int.TryParse(ds.Tables["Heir"].Rows[i]["HEIR_ID_TYPE_WALLET"].ToString(), out _);
                    if (isNumeric && !string.IsNullOrEmpty(ds.Tables["Heir"].Rows[i]["HEIR_ID_TYPE_WALLET"].ToString()))
                    {
                        h.idTypeWallet = ds.Tables["Heir"].Rows[i]["HEIR_ID_TYPE_WALLET"].ToString();
                    }
                    else
                    {
                        h.idTypeWallet = null;
                    }
                    TypeWallet tw = new TypeWallet();

                    if (isNumeric && !string.IsNullOrEmpty(ds.Tables["Heir"].Rows[i]["HEIR_ID_TYPE_WALLET"].ToString()))
                    {
                        tw = tw.GetTypeWallet(ds.Tables["Heir"].Rows[i]["HEIR_ID_TYPE_WALLET"].ToString());
                        if (tw.id != null)
                        {
                            h.walletType = tw;
                        }
                        else
                        {
                            h.walletType = null;
                        }
                    }


                    ListH.Add(h);
                }
            }


            return ListH;
        }




        public List<Heir> GetListHeirByPublicKey(string publicKey)
        {
            List<Heir> ListH = new List<Heir>();


            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;

            commDB = new OleDbCommand("SELECT  HEIR.*"
                                    + " FROM HEIR"
                                    + "   where HEIR_PUBLIC_KEY='" + publicKey + "'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "Heir");



            if (ds.Tables["Heir"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["Heir"].Rows.Count; i++)
                {

                    Heir h = new Heir();
                    Cryptage cr = new Cryptage();
                    h.IdUser = ds.Tables["Heir"].Rows[i]["ID_USER"].ToString();
                    h.IdHeir = ds.Tables["Heir"].Rows[i]["ID_HEIR"].ToString();
                    h.LastName = ds.Tables["Heir"].Rows[i]["HEIR_LASTNAME"].ToString();
                    h.FirstName = ds.Tables["Heir"].Rows[i]["HEIR_FIRSTNAME"].ToString();
                    h.Email = ds.Tables["Heir"].Rows[i]["HEIR_EMAIL"].ToString();
                    h.PhoneNumber = ds.Tables["Heir"].Rows[i]["HEIR_PHONENUMBER"].ToString();
                    h.IsActive = (bool)ds.Tables["Heir"].Rows[i]["HEIR_ACTIVE"];
                    h.Creation_Date = ds.Tables["Heir"].Rows[i]["HEIR_CREATION_DATE"].ToString();
                    h.PublicKey = ds.Tables["Heir"].Rows[i]["HEIR_PUBLIC_KEY"].ToString();
                    var isNumeric = int.TryParse(ds.Tables["Heir"].Rows[i]["HEIR_ID_TYPE_WALLET"].ToString(), out _);
                    if (isNumeric && !string.IsNullOrEmpty(ds.Tables["Heir"].Rows[i]["HEIR_ID_TYPE_WALLET"].ToString()))
                    {
                        h.idTypeWallet = ds.Tables["Heir"].Rows[i]["HEIR_ID_TYPE_WALLET"].ToString();
                    }
                    else
                    {
                        h.idTypeWallet = null;
                    }
                    TypeWallet tw = new TypeWallet();

                    if (isNumeric && !string.IsNullOrEmpty(ds.Tables["Heir"].Rows[i]["HEIR_ID_TYPE_WALLET"].ToString()))
                    {
                        tw = tw.GetTypeWallet(ds.Tables["Heir"].Rows[i]["HEIR_ID_TYPE_WALLET"].ToString());
                        if (tw.id != null)
                        {
                            h.walletType = tw;
                        }
                        else
                        {
                            h.walletType = null;
                        }
                    }


                    ListH.Add(h);
                }
            }


            return ListH;
        }



        public Heir()
        {

        }

    }
    public class IHeir
    {

        public string idUser { get; set; }
        public string idHeir { get; set; }

        public string lastName { get; set; }
        public string firstName { get; set; }
        public string phone { get; set; }

        public string email { get; set; }
        public string publicKey { get; set; }

        public TypeWallet walletType { get; set; }

        public string creation_Date { get; set; }



    }
}
