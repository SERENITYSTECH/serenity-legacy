using System.Data.OleDb;

namespace API_SerenityShield.Models.DAO
{
    public class Affiliate
    {
        public  string IdCustomer { get; set; }
        public string IdHeir { get; set; }
        public string AffiliateDate { get; set; }
        public string IdUSerCusotomer { get; set; }
        public string IdUserHeir { get; set; }



        public string InsertAffiliate(string idCustomer, string IDheir,string IdUserCustomer,string IdUserHeir,string HeirPublicKey)
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

                commDB = new OleDbCommand("INSERT INTO [dbo].[AFFILIATE] ([ID_CUSTOMER],[ID_HEIR],[AFFILIATE_DATE],[ID_USER_CUSTOMER],[ID_USER_HEIR],[HEIR_PUBLICKEY],[AFFILIATE_ACTIVE])"


                        + " VALUES ('" + idCustomer + "','" + IDheir + "','" + dateNow + "','"+ IdUserCustomer + "','"+ IdUserHeir + "','" + HeirPublicKey + "',1)", connexDB);
                CurrentID = commDB.ExecuteNonQuery().ToString();
                connexDB.Close();

                return "1";

            }
            catch (Exception ex)
            {
                connexDB.Close();
                return "-1";


            }

        }
        public string UpdateAffiliate(string idUserCustomer,string idHeir, string newPublicKey)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;
                Cryptage cr = new Cryptage();

                commDB = new OleDbCommand("UPDATE [dbo].[AFFILIATE]"

                       + " SET [HEIR_PUBLICKEY] ='" + newPublicKey + "'"

                    + " WHERE [ID_USER_CUSTOMER]='"+ idUserCustomer + "' and [ID_Heir]='" + idHeir + "'", connexDB);

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

        public string RemoveAffiliate(string idUserCustomer, string idHeir)
        {
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            try
            {

                connexDB.Open();
                OleDbCommand commDB;
                Cryptage cr = new Cryptage();

                commDB = new OleDbCommand("UPDATE [dbo].[AFFILIATE]"

                       + " SET [AFFILIATE_ACTIVE] =0"

                    + " WHERE [ID_USER_CUSTOMER]='" + idUserCustomer + "' and [ID_Heir]='" + idHeir + "'", connexDB);

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
    }
}
