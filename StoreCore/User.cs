using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace StoreCore
{
    class User
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

        public static void list()
        {
            using (var client = new SqlConnection("Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False"))
            {
                client.Open();
                StringBuilder sbCmd = new StringBuilder();
                sbCmd.AppendLine("SELECT Id, first_name, last_name, gender, age FROM Users");
                SqlCommand cmd = new SqlCommand(sbCmd.ToString(), client);
                using (var reader = cmd.ExecuteReader())
                {
                    Console.WriteLine("---------------------------------------------------------------");
                    Console.WriteLine(" Id    | First name    | Last name     | Gender    | Age       ");
                    Console.WriteLine("---------------------------------------------------------------");

                    while (reader.Read())
                    {
                        Console.WriteLine(String.Format(" {0,-6}| {1,-14}| {2,-14}| {3,-10}| {3,-10}",
                            reader[0], reader[1], reader[2], reader[3], reader[4]));
                        Console.WriteLine("---------------------------------------------------------------");
                    }
                }
            }
        }

    }
}
