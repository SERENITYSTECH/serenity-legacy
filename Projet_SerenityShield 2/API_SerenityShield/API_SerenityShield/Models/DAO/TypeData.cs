using System.Data;
using System.Data.OleDb;

namespace API_SerenityShield.Models.DAO
{
    public class TypeData
    {
        
        
            public string id { get; set; }
            public string label { get; set; }
           
            public TypeData()
            {
            }

            public List<TypeData> GetListSupportedWallet()
            {
                List<TypeData> ltw = new List<TypeData>();


                OleDbConnection connexDB;
                Connexion connect = new Connexion();

                connexDB = new OleDbConnection(connect.connecLoc);

                connexDB.Open();
                OleDbCommand commDB;
                OleDbDataAdapter adaptDB;
                DataSet ds;
                commDB = new OleDbCommand("SELECT        [TYPE_DATA].*"
                                + " FROM            [TYPE_DATA]  where supported=1", connexDB);

                adaptDB = new OleDbDataAdapter(commDB);
                ds = new DataSet();
                adaptDB.Fill(ds, "TypeData");



                if (ds.Tables["TypeData"].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables["TypeData"].Rows.Count; i++)
                    {

                        TypeData h = new TypeData();
                        Cryptage cr = new Cryptage();
                        h.id = cr.EncryptHexa(ds.Tables["TypeData"].Rows[i]["IDTYPEDATA"].ToString());
                        h.label = ds.Tables["TypeData"].Rows[i]["LABEL"].ToString();
                       
                        ltw.Add(h);
                    }
                }



                return ltw;
            }

            public List<TypeData> GetListWallet()
            {
                List<TypeData> ltw = new List<TypeData>();


                OleDbConnection connexDB;
                Connexion connect = new Connexion();

                connexDB = new OleDbConnection(connect.connecLoc);

                connexDB.Open();
                OleDbCommand commDB;
                OleDbDataAdapter adaptDB;
                DataSet ds;
                commDB = new OleDbCommand("SELECT        [TYPE_DATA].*"
                                + " FROM            [TYPE_DATA]", connexDB);

                adaptDB = new OleDbDataAdapter(commDB);
                ds = new DataSet();
                adaptDB.Fill(ds, "TypeData");



                if (ds.Tables["TypeData"].Rows.Count > 0)
                {
                    for (int i = 0; i < ds.Tables["TypeData"].Rows.Count; i++)
                    {

                        TypeData h = new TypeData();
                        Cryptage cr = new Cryptage();
                        h.id = cr.EncryptHexa(ds.Tables["TypeData"].Rows[i]["IDTYPEDATA"].ToString());
                        h.label = ds.Tables["TypeData"].Rows[i]["LABEL"].ToString();
                      
                        ltw.Add(h);
                    }
                }



                return ltw;
            }

            public TypeData GetTypeData(string idTypeData)
            {


                OleDbConnection connexDB;
                Connexion connect = new Connexion();

                connexDB = new OleDbConnection(connect.connecLoc);

                connexDB.Open();
                OleDbCommand commDB;
                OleDbDataAdapter adaptDB;
                DataSet ds;
                commDB = new OleDbCommand("SELECT        [TYPE_DATA].*"
                                + " FROM            [TYPE_DATA] where IDTypeData ='" + idTypeData + "'", connexDB);

                adaptDB = new OleDbDataAdapter(commDB);
                ds = new DataSet();
                adaptDB.Fill(ds, "TypeData");

                TypeData h = new TypeData();
                Cryptage cr = new Cryptage();

                if (ds.Tables["TypeData"].Rows.Count > 0)
                {

                    h.id = cr.EncryptHexa(ds.Tables["TypeData"].Rows[0]["IDTypeData"].ToString());
                    h.label = ds.Tables["TypeData"].Rows[0]["LABEL"].ToString();
                 
                }



                return h;
            }
        }
    }

