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
                sbCmd.AppendFormat(
                    "INSERT INTO Users(first_name, last_name, gender, age)"
                    + " OUTPUT INSERTED.Id VALUES ('{0}', '{1}', '{2}', {3}) ", 
                    user.FirstName, user.LastName, user.Gender, user.Age);

                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
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
                sbCmd.AppendFormat(
                    "INSERT INTO Users(first_name, last_name, gender, age, username, password, type, email)"
                    + " OUTPUT INSERTED.Id VALUES ('{0}', '{1}', '{2}', {3}, '{4}', '{5}', '{6}', '{7}') ",
                    user.FirstName, user.LastName, user.Gender, user.Age, user.Username, user.Password, user.Type, user.Email);

                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
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

        public static User FindByUsername(string username)
        {
            User user = new User();
            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.AppendFormat(
                    "SELECT *"
                    + " FROM Users WHERE username='{0}'",
                    username);
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
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
