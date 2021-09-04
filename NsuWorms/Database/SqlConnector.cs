using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NsuWorms.Database
{
    public class SqlConnector
    {
        public static void Connect()
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["localWindowsDatabase"].ConnectionString);

            try
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    Console.WriteLine("Opened!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
