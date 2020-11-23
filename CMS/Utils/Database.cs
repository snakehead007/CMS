using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Utils
{
    public class Database
    {
        public static bool IsDatabaseOnline()
        {
            try
            {
                SqlConnection con = new SqlConnection();
                SqlCommand cmd = new SqlCommand("select 1", con);
                con.Open();
                var rowCount = cmd.ExecuteScalar();
                con.Close();
                return rowCount.ToString().Length>0;
            }
            catch
            {
                return false;
            }
        }
    }
}
