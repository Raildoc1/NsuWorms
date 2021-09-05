using Microsoft.EntityFrameworkCore;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace NsuWormsWorldBehaviourGenerator.Database
{
    public class SqlConnector
    {
        public static void WriteToDatabase(string name, string data)
        {
            SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["localWindowsDatabase"].ConnectionString);

            try
            {
                connection.Open();
                if (connection.State != ConnectionState.Open)
                {
                    Console.WriteLine("Failed to open database!");
                    return;
                }

                try
                {
                    SqlCommand createTableCommand =
                        new SqlCommand("CREATE TABLE [dbo].[Behaviours] ([Name] CHAR(10) NOT NULL PRIMARY KEY, [Behaviour] TEXT NOT NULL)", connection);

                    createTableCommand.ExecuteNonQuery();
                }
                catch (Exception) { }

                try
                {
                    SqlCommand createTableCommand =
                        new SqlCommand($"INSERT INTO [dbo].[Behaviours] (Name, Behaviour) VALUES (N'{name}', N'{data}')", connection);

                    createTableCommand.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            catch (Exception)
            {

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
