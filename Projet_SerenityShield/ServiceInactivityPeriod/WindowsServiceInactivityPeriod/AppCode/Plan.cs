using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceInactivityPeriod.AppCode
{
    public class Plan
    {
        public string Id { get; set; }
        public string Price { get; set; }
        public string Label { get; set; }
        public string HeirsLimit { get; set; }
        public string PersonalStrongBoxLimit { get; set; }
        public string HeirsStrongBoxLimit { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public Plan GetCurrentPlan(string IdUser)
        {
            Plan plan = new Plan();

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT     [PLAN].*,[STARTDATE],[ENDDATE] "
                      + " FROM[PLAN]"
                      + " INNER JOIN   CUSTOMER_PLAN ON[PLAN].ID_PLAN = CUSTOMER_PLAN.ID_PLAN"
                      + " INNER JOIN   CUSTOMER ON CUSTOMER_PLAN.ID_CUSTOMER = CUSTOMER.ID_CUSTOMER"
                      + " where Customer.ID_USER='" + IdUser + "'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "PLAN");



            if (ds.Tables["PLAN"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();
                plan.Id = ds.Tables["PLAN"].Rows[0]["ID_PLAN"].ToString();
                plan.Price = ds.Tables["PLAN"].Rows[0]["PRICE"].ToString();
                plan.Label = ds.Tables["PLAN"].Rows[0]["LABEL"].ToString();
                if (ds.Tables["PLAN"].Rows[0]["NB_HEIRSTRONGBOXES"].ToString() == "unlimited")
                {
                    plan.HeirsLimit = null;
                }
                else
                {
                    plan.HeirsLimit = ds.Tables["PLAN"].Rows[0]["NB_HEIRSTRONGBOXES"].ToString();
                }
                if (ds.Tables["PLAN"].Rows[0]["NB_PERSONALSTRONGBOXES"].ToString() == "unlimited")
                {
                    plan.PersonalStrongBoxLimit = null;
                }
                else
                {
                    plan.PersonalStrongBoxLimit = ds.Tables["PLAN"].Rows[0]["NB_PERSONALSTRONGBOXES"].ToString();
                }
                if (ds.Tables["PLAN"].Rows[0]["NB_HEIRPERSTRONGBOX"].ToString() == "unlimited")
                {
                    plan.HeirsStrongBoxLimit = null;
                }
                else
                {
                    plan.HeirsStrongBoxLimit = ds.Tables["PLAN"].Rows[0]["NB_HEIRPERSTRONGBOX"].ToString();
                }

                plan.StartDate = ds.Tables["PLAN"].Rows[0]["STARTDATE"].ToString();

                plan.EndDate = ds.Tables["PLAN"].Rows[0]["ENDDATE"].ToString();
            }

            return plan;

        }

        public List<Plan> GetListPlan()
        {

            List<Plan> lp = new List<Plan>();

            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT     [PLAN].* FROM [PLAN]", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "PLAN");



            if (ds.Tables["PLAN"].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables["PLAN"].Rows.Count; i++)
                {
                    Plan plan = new Plan();

                    Cryptage cr = new Cryptage();
                    plan.Id = ds.Tables["PLAN"].Rows[i]["ID_PLAN"].ToString();
                    plan.Price = ds.Tables["PLAN"].Rows[i]["PRICE"].ToString();
                    plan.Label = ds.Tables["PLAN"].Rows[i]["LABEL"].ToString();
                    if (ds.Tables["PLAN"].Rows[i]["NB_HEIRSTRONGBOXES"].ToString() == "unlimited")
                    {
                        plan.HeirsLimit = null;
                    }
                    else
                    {
                        plan.HeirsLimit = ds.Tables["PLAN"].Rows[i]["NB_HEIRSTRONGBOXES"].ToString();
                    }
                    if (ds.Tables["PLAN"].Rows[i]["NB_PERSONALSTRONGBOXES"].ToString() == "unlimited")
                    {
                        plan.PersonalStrongBoxLimit = null;
                    }
                    else
                    {
                        plan.PersonalStrongBoxLimit = ds.Tables["PLAN"].Rows[i]["NB_PERSONALSTRONGBOXES"].ToString();
                    }
                    if (ds.Tables["PLAN"].Rows[i]["NB_HEIRPERSTRONGBOX"].ToString() == "unlimited")
                    {
                        plan.HeirsStrongBoxLimit = null;
                    }
                    else
                    {
                        plan.HeirsStrongBoxLimit = ds.Tables["PLAN"].Rows[i]["NB_HEIRPERSTRONGBOX"].ToString();
                    }


                    lp.Add(plan);
                }

            }
            return lp;

        }
        public Plan GetPlanByID(string idPlan)
        {



            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            DataSet ds;
            commDB = new OleDbCommand("SELECT     [PLAN].* FROM [PLAN] WHERE ID_PLAN='" + idPlan + "'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "PLAN");
            Plan plan = new Plan();

            if (ds.Tables["PLAN"].Rows.Count > 0)
            {



                Cryptage cr = new Cryptage();
                plan.Id = ds.Tables["PLAN"].Rows[0]["ID_PLAN"].ToString();
                plan.Price = ds.Tables["PLAN"].Rows[0]["PRICE"].ToString();
                plan.Label = ds.Tables["PLAN"].Rows[0]["LABEL"].ToString();
                if (ds.Tables["PLAN"].Rows[0]["NB_HEIRSTRONGBOXES"].ToString() == "unlimited")
                {
                    plan.HeirsLimit = null;
                }
                else
                {
                    plan.HeirsLimit = ds.Tables["PLAN"].Rows[0]["NB_HEIRSTRONGBOXES"].ToString();
                }
                if (ds.Tables["PLAN"].Rows[0]["NB_PERSONALSTRONGBOXES"].ToString() == "unlimited")
                {
                    plan.PersonalStrongBoxLimit = null;
                }
                else
                {
                    plan.PersonalStrongBoxLimit = ds.Tables["PLAN"].Rows[0]["NB_PERSONALSTRONGBOXES"].ToString();
                }
                if (ds.Tables["PLAN"].Rows[0]["NB_HEIRPERSTRONGBOX"].ToString() == "unlimited")
                {
                    plan.HeirsStrongBoxLimit = null;
                }
                else
                {
                    plan.HeirsStrongBoxLimit = ds.Tables["PLAN"].Rows[0]["NB_HEIRPERSTRONGBOX"].ToString();
                }



            }
            return plan;

        }
    }

}
