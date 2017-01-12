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
        public Bala1()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void createOrUpdateURL(string objectGuid, string URL)
        {
            string sqlQueryRead = "select * from Objects where ObjectGUID = '{0}'";
            string sqlQueryInsert = "insert into Objects(ObjectGUID, URL) values ('{0}', '{1}'";
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
                                
                            }
                            
                        }
                        else
                        {

                        }
                    }

                    // Ok the record does not exist
                    // Insert the record here
                    com.CommandText = sqlQueryInsert;
                    com.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        }
    }
}