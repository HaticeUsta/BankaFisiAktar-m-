using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace BankaFisiExcelAktarim
{
    public class SqlHelper
    {
        public static string GetConnectionStrings()
        {
            return ConfigurationManager.ConnectionStrings["EfContext"].ConnectionString;
        }

        public static SqlCommand GetCommand(string StoredProcedureName)
        {
            //Initialize connection
            SqlConnection cnn = new SqlConnection();
            cnn.ConnectionString = ConfigurationManager.ConnectionStrings["EfContext"].ConnectionString;
            // cnn.ConnectionString = "Data Source=31.10.52.62;Integrated Security=SSPI;User ID=sa;Password=Lbs12345;Initial Catalog=GoknilWebService";
            cnn.Open();

            //Create Command
            SqlCommand cmd = new SqlCommand(StoredProcedureName, cnn);
            cmd.CommandType = CommandType.StoredProcedure;

            //return new command instance
            return cmd;
        }

        public static DataTable ExecuteQuery(string spName, List<SqlParameter> Parameters)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {
                cmd = SqlHelper.GetCommand(spName);
                foreach (SqlParameter spParam in Parameters)
                    cmd.Parameters.Add(spParam);
                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(dt);
                return dt;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message + " SqlHelper.ExecuteQuery ");

            }
            finally
            {

                if (cmd != null && cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();

                if (cmd != null)
                    cmd.Dispose();
            }
        }

        public static SqlCommand GetSqlConnectionCMD(string SqlCommand, string baglanti)
        {
            SqlCommand cmd = null;
            try
            {
                //Sql connection
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = baglanti;
                cnn.Open();

                //Sql Command
                cmd = new SqlCommand(SqlCommand, cnn);
                cmd.CommandType = CommandType.Text;

                return cmd;
            }
            catch (Exception)
            {
                return cmd;
            }
            finally
            {
                if (cmd != null && cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();

                if (cmd != null)
                    cmd.Dispose();
            }
        }

        public static DataTable GetDataTableCMD(string SqlCommand, string baglanti)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {
                cmd = SqlHelper.GetSqlConnectionCMD(SqlCommand, baglanti);

                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd != null && cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();

                if (cmd != null)
                    cmd.Dispose();
            }
        }

        public static SqlCommand GetSqlConnectionSP(string StoredProcedureName, string baglanti)
        {
            SqlCommand cmd = null;
            try
            {
                //Initialize connection
                SqlConnection cnn = new SqlConnection();
                cnn.ConnectionString = baglanti;
                cnn.Open();

                //Create Command
                cmd = new SqlCommand(StoredProcedureName, cnn);
                cmd.CommandType = CommandType.StoredProcedure;

                //return new command instance
                return cmd;
            }
            catch (Exception)
            {
                return cmd;
            }
            finally
            {
                if (cmd != null && cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();

                if (cmd != null)
                    cmd.Dispose();
            }
        }

        public static DataTable GetDataTableSP(string spName, string baglanti)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = null;
            try
            {
                cmd = SqlHelper.GetSqlConnectionSP(spName, baglanti);

                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(dt);
                return dt;
            }
            catch (Exception)
            {
                return dt;
            }
            finally
            {
                if (cmd != null && cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();

                if (cmd != null)
                    cmd.Dispose();
            }
        }

        public static bool ExecuteNonQueryBySqlCmd(string SqlCommand, string baglanti)
        {
            if (string.IsNullOrEmpty(baglanti))
                baglanti = GetConnectionStrings();

            SqlCommand cmd = null;
            try
            {
                cmd = SqlHelper.GetSqlConnectionCMD(SqlCommand, baglanti);
                cmd.Connection.Open();
                cmd.CommandText = SqlCommand;
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                if (cmd != null && cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();

                if (cmd != null)
                    cmd.Dispose();
            }
        }
    }
}
