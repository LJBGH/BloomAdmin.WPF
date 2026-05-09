using Microsoft.Data.SqlClient;

namespace BloomAdmin.Main.DataAccess
{
    public class LocalDataAccess
    {
        private static LocalDataAccess instance;
        private LocalDataAccess() { }

        private static LocalDataAccess GetInstance()
        {
            return instance ?? (instance = new LocalDataAccess { });
        }

        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter;
        private void Dispose()
        {
            if (adapter != null) { adapter.Dispose(); adapter = null; }
            if (cmd != null) { cmd.Dispose(); cmd = null; }
            if (conn != null) { conn.Close(); conn.Dispose(); conn = null; }
        }

        private bool DbConnection()
        {
            if (conn == null)
            {
                conn = new SqlConnection("");
            }

            try
            {
                conn.Open();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
