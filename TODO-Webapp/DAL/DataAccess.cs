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
        public User GetUser(string username)
        {
            using (SqlConnection conn = new(connectionString))
            {
                SqlCommand cmd = new("GetUser", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@Username", username);
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
                    return user;
                }
            };
            throw new UserNotFoundException($"No user with username: {username} exists.");
        }
    }
}
