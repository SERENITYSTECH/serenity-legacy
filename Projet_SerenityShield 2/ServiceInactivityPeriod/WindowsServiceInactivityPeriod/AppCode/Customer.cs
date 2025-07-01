using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceInactivityPeriod.AppCode
{
    public class Customer
    {
        public string IdUser { get; set; }
        public string IdCustomer { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Creation_Date { get; set; }
        public bool IsActive { get; set; }
        public Plan CurrentPaymentPlan { get; set; }
        public List<Plan> ListPaymentPlan { get; set; }
  

        public Customer GetCustomerById(string id)
        {
            Customer u;
            OleDbConnection connexDB;
            Connexion connect = new Connexion();

            connexDB = new OleDbConnection(connect.connecLoc);

            connexDB.Open();
            OleDbCommand commDB;
            OleDbDataAdapter adaptDB;
            System.Data.DataSet ds;
            commDB = new OleDbCommand("SELECT * FROM[CUSTOMER] where ID_CUSTOMER = '" + id + "'", connexDB);

            adaptDB = new OleDbDataAdapter(commDB);
            ds = new DataSet();
            adaptDB.Fill(ds, "CUSTOMER");

            u = new Customer();

            if (ds.Tables["CUSTOMER"].Rows.Count > 0)
            {
                Cryptage cr = new Cryptage();
                u.IdUser = ds.Tables["CUSTOMER"].Rows[0]["ID_USER"].ToString();
                u.IdCustomer = ds.Tables["CUSTOMER"].Rows[0]["ID_CUSTOMER"].ToString();
                u.LastName = cr.Decrypt(ds.Tables["CUSTOMER"].Rows[0]["CUSTOMER_NAME"].ToString());
                u.FirstName = cr.Decrypt(ds.Tables["CUSTOMER"].Rows[0]["CUSTOMER_FIRSTNAME"].ToString());
                u.Email = cr.Decrypt(ds.Tables["CUSTOMER"].Rows[0]["CUSTOMER_EMAIL"].ToString());
            }

            return u;
        }
  
        public void DeleteCustomer() { }
        public Customer selectCustomer()
        {
            Customer c = new Customer();
            return c;
        }
        public Customer()
        {

        }

    }
}
