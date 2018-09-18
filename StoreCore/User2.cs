using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    
    class User2
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string Email { get; set; }

        public User2(string firstName, string lastName, string gender, int age, string username, string type, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Age = age;
            Username = username;
            Type = type;
            Email = email;
        }

    }
}
