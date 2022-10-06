using System.Data;
using System.Diagnostics;
using System;
using System.Data.SqlClient;
using TODO_Webapp.Model;
using Microsoft.AspNetCore.Http.Features;
using System.Runtime.Serialization.Formatters;

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

        #region ToDo

        public void UpdateToDo(ToDo toDo)
        {
            using (SqlConnection conn = new(connectionString))
            {
                SqlCommand cmd = new("UpdateToDo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ToDoID", toDo.Id);
                cmd.Parameters.AddWithValue("@Description", toDo.Description);
                cmd.Parameters.AddWithValue("@Priority", (int)toDo.Importance);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    conn.Close();
                }
            }

        }


        public ToDo CreateToDo(Guid guid, string description, Priority priority, int userId, string party)
        {
            using (SqlConnection conn = new(connectionString))
            {
                SqlCommand cmd = new("CreateToDo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ToDoID", guid);
                cmd.Parameters.AddWithValue("@Title", "Not Implemented");
                cmd.Parameters.AddWithValue("@Description", description);
                cmd.Parameters.AddWithValue("@Created", DateTime.Today);
                cmd.Parameters.AddWithValue("@Priority", priority);
                cmd.Parameters.AddWithValue("@Completed", false);
                cmd.Parameters.AddWithValue("@UserID", userId);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    ToDo todo = new()
                    {
                        Id = guid.ToString(),
                        Description = description,
                        Created = DateTime.Today,
                        Importance = priority
                    };
                    if (!string.IsNullOrWhiteSpace(party))
                    {

                        List<string> partyList = party.Split(',', StringSplitOptions.TrimEntries).ToList();
                        partyList.ForEach(u =>
                        {
                            conn.Open();
                            cmd = new("AddPartyMember", conn)
                            {
                                CommandType = CommandType.StoredProcedure
                            };
                            cmd.Parameters.AddWithValue("@ToDoID", guid);
                            cmd.Parameters.AddWithValue("@Username", u);
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        });
                    }
                    return todo;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    conn.Close();
                }
                return null;
            }
        }

        public List<ToDo> GetUsersToDo(int userId)
        {
            List<ToDo> toDos = new List<ToDo>();
            using (SqlConnection conn = new(connectionString))
            {
                SqlCommand cmd = new("GetUsersToDos", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ID", userId);
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Priority pri = Priority.Normal;
                        switch (reader.GetInt32("PrioID"))
                        {
                            case 1:
                                pri = Priority.Low;
                                break;
                            case 2:
                                pri = Priority.Normal;
                                break;
                            case 3:
                                pri = Priority.High;
                                break;
                            default:
                                break;
                        }
                        ToDo toDo = new()
                        {
                            Id = reader.GetGuid("ToDoID").ToString(),
                            Description = reader.GetString("Description"),
                            Created = reader.GetDateTime("Created"),
                            IsCompleted = reader.GetBoolean("Completed"),
                            Importance = pri
                        };
                        toDos.Add(toDo);
                    }
                    return toDos;
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
            return toDos;
        }

        public void CompleteToDo(string guid)
        {
            using (SqlConnection conn = new(connectionString))
            {
                SqlCommand cmd = new("CompleteToDo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ToDoID", guid);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
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
        }

        public void DisableToDo(string guid)
        {
            using (SqlConnection conn = new(connectionString))
            {
                SqlCommand cmd = new("DeleteToDo", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@ToDoID", guid);
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
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
        }
        #endregion

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
                    User user = new();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {

                        user.ID = reader.GetInt32("UserID");
                        user.FirstName = reader.GetString("FirstName");
                        user.LastName = reader.GetString("LastName");
                        user.Username = reader.GetString("Username");
                        user.Password = reader.GetString("Password");

                    }
                    if (user == null)
                        throw new UserNotFoundException($"No user with username: {username} exists.");
                    else
                        return user;
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
            return null;
        }

        public void SignUp(string firstName, string lastName, string username, string password)
        {
            List<string> usernames = new List<string>();

            using (SqlConnection conn = new(connectionString))
            {

                SqlCommand cmd = new("AllUsernames", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                try
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        usernames.Add(reader.GetString("Username"));
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }

                if (usernames.Contains(username))
                    throw new UserAlreadyExistsException($"A user with {username} as their username already exists");


                SqlCommand cmd1 = new("CreateUser", conn)
                {
                    CommandType = CommandType.StoredProcedure
                };
                cmd1.Parameters.AddWithValue("@F_Name", firstName);
                cmd1.Parameters.AddWithValue("@L_Name", lastName);
                cmd1.Parameters.AddWithValue("@Username", username);
                cmd1.Parameters.AddWithValue("@Password", password);
                try
                {
                    conn.Open();
                    cmd1.ExecuteNonQuery();
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    conn.Close();
                }
            };
        }


        #endregion
    }
}
