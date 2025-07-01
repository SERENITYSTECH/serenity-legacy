using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceInactivityPeriod.AppCode
{
    public class HistoriqueUser
    {
        public string IdHistorique { get; set; }
        public string IdUser { get; set; }
        public string Date_creation { get; set; }
        public string comment { get; set; }
        public string IdTypeHisto { get; set; }
        public string IsActiveHisto { get; set; }
        public TypeHisto typeHisto { get; set; }
        public List<HistoriqueUser> GethistoriqueUser(string idUser)
        {
            List<HistoriqueUser> historiqueUsers = new List<HistoriqueUser>();

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT  *"
                            + " FROM            [HISTORIQUE_USER]"
                            + "  where ID_USER='" + idUser + "' and HISTO_ACTIVE=1 ORDER BY  HISTO_DATE DESC", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "HISTORIQUE");


            Cryptage cr = new Cryptage();
            if (ds.Tables["HISTORIQUE"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["HISTORIQUE"].Rows.Count; i++)
                {
                    HistoriqueUser hu = new HistoriqueUser();
                    hu.IdUser = cr.EncryptHexa(ds.Tables["HISTORIQUE"].Rows[i]["ID_USER"].ToString());
                    hu.IdHistorique = cr.EncryptHexa(ds.Tables["HISTORIQUE"].Rows[i]["ID_HISTORIQUE"].ToString());
                    hu.IdTypeHisto = cr.EncryptHexa(ds.Tables["HISTORIQUE"].Rows[i]["ID_TYPE_HISTO"].ToString());
                    hu.comment = ds.Tables["HISTORIQUE"].Rows[i]["COMMENT"].ToString();
                    hu.Date_creation = ds.Tables["HISTORIQUE"].Rows[i]["HISTO_DATE"].ToString();
                    hu.IsActiveHisto = ds.Tables["HISTORIQUE"].Rows[i]["HISTO_ACTIVE"].ToString();
                    TypeHisto th = new TypeHisto();
                    hu.typeHisto = th.GetTypeHisto(ds.Tables["HISTORIQUE"].Rows[i]["ID_TYPE_HISTO"].ToString());
                    historiqueUsers.Add(hu);
                }


            }

            return historiqueUsers;
        }
        public string InsertHistoriqueUser(string idUser, string idtypeHisto, string comment)
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

                commDB = new OleDbCommand("INSERT INTO [dbo].[HISTORIQUE_USER]([ID_USER] ,[ID_TYPE_HISTO],[COMMENT],[HISTO_DATE] ,[HISTO_ACTIVE])"

                        + " OUTPUT INSERTED.ID_HISTORIQUE"
                        + " VALUES ('" + idUser + "','" + idtypeHisto + "','" + comment + "','" + dateNow + "',1)", connexDB);
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
    }
    public class TypeHisto
    {
        public string IdTypeHisto { get; set; }
        public string Label { get; set; }

        public TypeHisto GetTypeHisto(string idTypeHisto)
        {

            TypeHisto th = new TypeHisto();
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT  *"
                            + " FROM            [TYPE_HISTO]"
                            + "  where ID_TYPE_HISTO='" + idTypeHisto + "'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "TYPEHISTORIQUE");




            if (ds.Tables["TYPEHISTORIQUE"].Rows.Count > 0)
            {

                th.IdTypeHisto = ds.Tables["TYPEHISTORIQUE"].Rows[0]["ID_TYPE_HISTO"].ToString();
                th.Label = ds.Tables["TYPEHISTORIQUE"].Rows[0]["TYPE_LABEL"].ToString();

            }

            return th;
        }

        public TypeHisto()
        { }

    }

    public class IHistory
    {
        public string id { get; set; }
        public TypeHisto type { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string date { get; set; }
        public IHistory()
        { }

    }
}
