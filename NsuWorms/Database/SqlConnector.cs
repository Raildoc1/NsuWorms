using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NsuWorms.Database
{
    public class SqlConnector
    {
        public string ConnectAndReadData(string name)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["localWindowsDatabase"].ConnectionString);

            try
            {
                connection.Open();
                if (connection.State != ConnectionState.Open)
                {
                    Console.WriteLine("Failed to open database!");
                    return string.Empty;
                }

                var adapter = new SqlDataAdapter($"SELECT [Behaviour] FROM [dbo].[Behaviours] WHERE [NAME]=N'{name}'", connection);
                var dataSet = new DataSet();
                adapter.Fill(dataSet);
                return (string)dataSet.Tables[0].Rows[0].ItemArray[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return string.Empty;
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
