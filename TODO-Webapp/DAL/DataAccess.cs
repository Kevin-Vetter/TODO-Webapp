using System.Data;
using System.Diagnostics;
using System;
using System.Data.SqlClient;
using TODO_Webapp.Model;

namespace TODO_Webapp.DAL
{

    public class DataAccess
    {
        public static IConfigurationRoot Configuration { get; private set; }
        static string connectionString;
        public DataAccess()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();
            connectionString = Configuration.GetConnectionString("DefaultConnection");
        }


        #region user
        public User GetUser(string username)
        {
            using (SqlConnection conn = new(connectionString))
            {
                SqlCommand cmd = new("GetUser", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Username", username);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new()
                        {
                            ID = reader.GetInt32("UserID"),
                            FirstName = reader.GetString("FirstName"),
                            LastName = reader.GetString("LastName"),
                            Username = reader.GetString("Username"),
                            Password = reader.GetString("Password")
                        };
                        if (user == null)
                            throw new UserNotFoundException($"No user with username: {username} exists.");
                        else
                            return user;
                    }
                    conn.Close();
                }
                catch (UserNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    conn.Close();
                }
            };
            throw new Exception("Shiiiiiiiit");
        }
        #endregion
    }
}
