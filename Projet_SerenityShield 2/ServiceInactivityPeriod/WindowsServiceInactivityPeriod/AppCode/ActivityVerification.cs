using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceInactivityPeriod.AppCode
{
    public class ActivityVerification
    {

        public string IdActivityVerification { get; set; }
        public string Id_User { get; set; }
        public string Id_StrongBoxForHeir { get; set; }
        public string Date_Email { get; set; }
        public string Date_Check { get; set; }
        public string Check { get; set; }
        public string Deceaded { get; set; }
        public string Date_Death { get; set; }
        public string IdPeriod { get; set; }
        public string Active { get; set; }
        public string InsertActivityVerification(string idUser, string idSbForHeir,string idPeriod)
        {
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString();
            string CurrentID = string.Empty;
            string idCurrent = string.Empty;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {


                // Log.Create();
                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("INSERT INTO [dbo].[ACTIVITY_VERIFICATION]([ID_USER],[ID_STRONGBOXFORHEIR],[DATE_EMAIL],[DATE_CHECK],[CHECK]  ,[DECEADED],[IDPERIOD],[ACTIVE],[DATE_DEATH] )"

                        + " OUTPUT INSERTED.ID_ACTIVITY_VERIFICATION"
                        + " VALUES ('" + idUser + "','" + idSbForHeir + "','" + dateNow + "',NULL,NULL,NULL,'"+idPeriod+"',1,NULL)", connexDB);
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


        public string UpdateCheckActivityVerification(string IdUser, string IdSbForHeir)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString();
            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[ACTIVITY_VERIFICATION] SET"
                    + "  [DATE_CHECK] = '" + dateNow + "'"
                    + " ,[CHECK] = 1"
                    + " WHERE ID_USER='" + IdUser + "' and ID_STRONGBOXFORHEIR='" + IdSbForHeir + "'", connexDB);

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
        public string UpdateDeceadedActivityVerification(string IdUser, string IdSbForHeir)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString();
            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[ACTIVITY_VERIFICATION] SET"
                    + "  [DECEADED] = 1"
                    + " ,[DATE_DEATH]='"+ dateNow + "'"
                    + " WHERE ID_USER='" + IdUser + "' and ID_STRONGBOXFORHEIR='" + IdSbForHeir + "'", connexDB);

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
        public string RemoveDeceadedActivityVerification(string IdUser, string IdSbForHeir)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString();
            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[ACTIVITY_VERIFICATION] SET"
                    + "  [DECEADED] = 0"
                    + " ,[DATE_DEATH]=NULL"
                    + " WHERE ID_USER='" + IdUser + "' and ID_STRONGBOXFORHEIR='" + IdSbForHeir + "'", connexDB);

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
        public string ActiveActivityVerification(string IdUser, string IdSbForHeir)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString();
            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[ACTIVITY_VERIFICATION] SET"
                    + "  [ACTIVE] = 1"

                    + " WHERE ID_USER='" + IdUser + "' and ID_STRONGBOXFORHEIR='" + IdSbForHeir + "'", connexDB);

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
        public string DesactiveActivityVerification(string IdUser, string IdSbForHeir)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();
            string dateNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").ToString();
            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;

                commDB = new OleDbCommand("UPDATE [dbo].[ACTIVITY_VERIFICATION] SET"
                    + "  [ACTIVE] = 0"

                    + " WHERE ID_USER='" + IdUser + "' and ID_STRONGBOXFORHEIR='" + IdSbForHeir + "'", connexDB);

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

        public ActivityVerification GetActivityVerificationByIdUserandIdSbForHeir(string idUser, string idSbForHeir)
        {
            ActivityVerification h = new ActivityVerification();

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT        [ACTIVITY_VERIFICATION].*"
                            + " FROM            [ACTIVITY_VERIFICATION]"
                            + " WHERE  ID_USER='" + idUser + "' and  ID_STRONGBOXFORHEIR='" + idSbForHeir + "' and DATE_CHECK is null", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "ACTIVITY");

            h = new ActivityVerification();

            if (ds.Tables["ACTIVITY"].Rows.Count > 0)
            {

                h.Id_User = ds.Tables["ACTIVITY"].Rows[0]["ID_USER"].ToString();
                h.Id_StrongBoxForHeir = ds.Tables["ACTIVITY"].Rows[0]["ID_STRONGBOXFORHEIR"].ToString();
                h.Date_Email = ds.Tables["ACTIVITY"].Rows[0]["DATE_EMAIL"].ToString();
                h.Date_Check = ds.Tables["ACTIVITY"].Rows[0]["DATE_CHECK"].ToString();
                h.Check = ds.Tables["ACTIVITY"].Rows[0]["CHECK"].ToString();
                h.Deceaded = ds.Tables["ACTIVITY"].Rows[0]["DECEADED"].ToString();
                h.IdPeriod = ds.Tables["ACTIVITY"].Rows[0]["IDPERIOD"].ToString();
                h.Active = ds.Tables["ACTIVITY"].Rows[0]["ACTIVE"].ToString();
                h.Date_Death = ds.Tables["ACTIVITY"].Rows[0]["DATE_DEATH"].ToString();
            }

            return h;
        }
        public bool IfEmailSended(string idUser, string idStrongboxForHeir)
        {
            bool exist = false;

            string isOk = string.Empty;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            commDB = new OleDbCommand("if exists (select * from  [dbo].[ACTIVITY_VERIFICATION] where  ID_USER='" + idUser + "' and  ID_STRONGBOXFORHEIR='" + idStrongboxForHeir + "' and DATE_CHECK is null ) "
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
    }
}
