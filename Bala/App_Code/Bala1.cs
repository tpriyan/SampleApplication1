using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
/// <summary>
/// Summary description for Bala1
/// </summary>
namespace BusinessLogic
{
    public class Bala1
    {
        public enum URLStatus
        {
            AlreadyUpdate,
            Updated,
            Created
        }
        public Bala1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public System.Data.DataTable DataTable()
        {
            System.Data.DataTable dt = new System.Data.DataTable();
            dt.Columns.Add("ObjectGUID");
            dt.Columns.Add("URL");

            return dt;
        }

        public void LoadData(System.Web.UI.WebControls.GridView gridView)
        {
            string sqlQueryRead = "select * from InworldObjects";
            System.Data.DataTable dataTable = this.DataTable();
            System.Data.DataRow dataRow = null;


            using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("data source=" + HttpContext.Current.Server.MapPath("~/App_Data/Data.db")))
            {
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();
                    com.CommandText = sqlQueryRead;
                    using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            dataRow = dataTable.NewRow();
                            dataRow[0] = reader.GetValue(0);
                            dataRow[1] = reader.GetValue(1);

                            dataTable.Rows.Add(dataRow);
                        }
                    }
                }               
            }

            gridView.DataSource = dataTable;
            gridView.DataBind();

        }

        public URLStatus createOrUpdateURL(string objectGuid, string URL)
        {
            string sqlQueryRead = "select * from InworldObjects where ObjectGUID = '{0}'";
            string sqlQueryInsert = "insert into InworldObjects(ObjectGUID, URL) values ('{0}', '{1}')";
            string sqlQueryUpdate = "update InworldObjects set URL = '{0}' where ObjectGUID = '{1}'";

            URLStatus status;

            using (System.Data.SQLite.SQLiteConnection con = new System.Data.SQLite.SQLiteConnection("data source=" + HttpContext.Current.Server.MapPath("~/App_Data/Data.db")))
            {
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    con.Open();

                    com.CommandText = String.Format(sqlQueryRead, objectGuid);

                    using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if(reader["URL"].ToString() == URL)
                            {
                                status = URLStatus.AlreadyUpdate;
                            }
                            else
                            {
                                reader.Close();
                                com.CommandText = String.Format(sqlQueryUpdate, URL, objectGuid);
                                com.ExecuteNonQuery();
                                status = URLStatus.Updated;
                            }
                            
                        }
                        else
                        {
                            reader.Close();
                            com.CommandText = String.Format(sqlQueryInsert, objectGuid, URL);
                            com.ExecuteNonQuery();
                            status = URLStatus.Created;
                        }
                    }
                    con.Close();
                }
            }

            return status;
        }
        
    }
}