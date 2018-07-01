using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace StoreCore
{
    class UserDb
    {

        public static bool add(string firstName, string lastName, string gender, int age)
        {
            using (var client = new SqlConnection("Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False"))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.AppendLine("INSERT INTO Users(first_name, last_name, gender, age) ");
                sbCmd.AppendFormat("VALUES ('{0}', '{1}', '{2}', {3}) ", 
                    firstName, lastName, gender, age);

                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                int result = cmd.ExecuteNonQuery();
                if (result == 1)
                    return true;
                else
                    return false;
            }
        }

        public static List<User> list()
        {
            List<User> users = new List<User>();

            using (var client = new SqlConnection("Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False"))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.AppendLine("SELECT * FROM Users");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader[0].ToString());
                        string first_name = reader[1].ToString();
                        string last_name = reader[2].ToString();
                        string gender = reader[3].ToString();
                        int age = int.Parse(reader[4].ToString());

                        users.Add(new User(id, first_name, last_name, gender, age));
                        //Console.WriteLine(String.Format(" {0,-6}| {1,-14}| {2,-14}| {3,-10}| {3,-10}",
                        //    reader[0], reader[1], reader[2], reader[3], reader[4]));
                        //Console.WriteLine("---------------------------------------------------------------");
                    }
                }
            }
            return users;
        }

    }
}
