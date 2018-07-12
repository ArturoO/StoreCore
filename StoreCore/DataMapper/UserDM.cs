using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace StoreCore
{
    class UserDM 
    {

        protected const string connectionString = "Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False";

        public static bool Create(User user)
        {
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append(
                    "INSERT INTO Users(first_name, last_name, gender, age)"
                    + " OUTPUT INSERTED.Id VALUES (@first_name, @last_name, @gender, @age) ");

                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@first_name", user.FirstName);
                cmd.Parameters.AddWithValue("@last_name", user.LastName);
                cmd.Parameters.AddWithValue("@gender", user.Gender);
                cmd.Parameters.AddWithValue("@age", user.Age);

                var insertedId = int.Parse(cmd.ExecuteScalar().ToString());
                if(insertedId>0)
                {
                    user.Id = insertedId;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool Register(User user)
        {
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append(
                    "INSERT INTO Users(first_name, last_name, gender, age, username, password, type, email)"
                    + " OUTPUT INSERTED.Id VALUES (@first_name, @last_name, @gender, @age, @username, @password, @type, @email)");

                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@first_name", user.FirstName);
                cmd.Parameters.AddWithValue("@last_name", user.LastName);
                cmd.Parameters.AddWithValue("@gender", user.Gender);
                cmd.Parameters.AddWithValue("@age", user.Age);
                cmd.Parameters.AddWithValue("@username", user.Username);
                cmd.Parameters.AddWithValue("@password", user.Password);
                cmd.Parameters.AddWithValue("@type", user.Type);
                cmd.Parameters.AddWithValue("@email", user.Email);

                var insertedId = int.Parse(cmd.ExecuteScalar().ToString());
                if (insertedId > 0)
                {
                    user.Id = insertedId;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public static User FindById(int id)
        {
            User user = new User();
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append(
                    "SELECT *"
                    + " FROM Users WHERE Id=@Id");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@Id", id);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.Id = int.Parse(reader["id"].ToString());
                        user.Username = reader["username"].ToString();
                        user.Password = reader["password"].ToString();
                        user.FirstName = reader["first_name"].ToString();
                        user.LastName = reader["last_name"].ToString();
                        user.Gender = reader["gender"].ToString();
                        user.Age = int.Parse(reader["age"].ToString());
                        user.Type = reader["type"].ToString();
                        user.Email = reader["email"].ToString();
                    }
                }
            }
            return user;
        }

        public static User FindByUsername(string username)
        {
            User user = new User();
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.Append(
                    "SELECT *"
                    + " FROM Users WHERE username=@username");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                cmd.Parameters.AddWithValue("@username", username);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.Id = int.Parse(reader["Id"].ToString());
                        user.Username = username;
                        user.Password = reader["password"].ToString();
                        user.FirstName = reader["first_name"].ToString();
                        user.LastName = reader["last_name"].ToString();
                        user.Gender = reader["gender"].ToString();
                        user.Age = int.Parse(reader["age"].ToString());
                        user.Type = reader["type"].ToString();
                        user.Email = reader["email"].ToString();                        
                    }
                }
            }
            return user;
        }

        public static List<User> list()
        {
            List<User> users = new List<User>();

            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.AppendLine("SELECT * FROM Users");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["Id"].ToString());
                        string firstName = reader["first_name"].ToString();
                        string lastName = reader["last_name"].ToString();
                        string username = reader["username"].ToString();
                        string password = reader["password"].ToString();
                        string email = reader["email"].ToString();
                        string gender = reader["gender"].ToString();
                        string type = reader["type"].ToString();
                        int age = int.Parse(reader["age"].ToString());

                        User user = new User(id, firstName, lastName, gender, age, username, password, type, email);
                        users.Add(user);
                    }
                }
            }
            return users;
        }

    }
}
