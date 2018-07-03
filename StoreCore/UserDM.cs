using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace StoreCore
{
    class UserDM 
    {

        protected const string connectionString = "Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False";

        public bool Create(User user)
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

        public List<User> list()
        {
            List<User> users = new List<User>();

            using (var client = new SqlConnection(connectionString))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.AppendLine("SELECT Id, first_name, last_name, gender, age  FROM Users");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["Id"].ToString());
                        string first_name = reader["first_name"].ToString();
                        string last_name = reader["last_name"].ToString();
                        string gender = reader["gender"].ToString();
                        int age = int.Parse(reader["age"].ToString());
                        User user = new User(id, first_name, last_name, gender, age);
                        users.Add(user);
                    }
                }
            }
            return users;
        }

    }
}
