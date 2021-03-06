﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace StoreCore.Entities
{
    
    class User
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
        public Cart Cart { get; set; }
        public ICollection<Order> Orders { get; set; }

        public User()
        {

        }

        public User(string firstName, string lastName, string gender, int age, string username, string type, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Age = age;
            Username = username;
            Type = type;
            Email = email;
        }

        public void LoadCart()
        {
            using (var context = new StoreContext())
            {
                Cart = context.Carts
                    .Include(c => c.Products)
                    .ThenInclude(p => p.Product)
                    .SingleOrDefault(x => x.UserId == Id);
                Cart.UpdateSummary();
            }
        }

        public void HashPassword(string password)
        {
            //generate salt
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            //hash password and salt, repeat 1000 times
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);

            //generated hash, 20 bytes
            byte[] hash = pbkdf2.GetBytes(20);

            //concatenate salt(16bytes) and hash(20bytes)
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            //convert bytes to string
            string passwordHash = Convert.ToBase64String(hashBytes);

            Password = passwordHash;
        }

        public bool ValidatePassword(string password)
        {
            byte[] hashBytes = Convert.FromBase64String(Password);
            byte[] salt = new byte[16];
            Array.Copy(hashBytes, 0, salt, 0, 16);
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000);
            byte[] hash = pbkdf2.GetBytes(20);

            bool result = true;

            for (int i = 0; i < 20; i++)
            {
                if (hashBytes[16 + i] != hash[i])
                {
                    result = false;
                    break;
                }
            }

            return result;

        }

    }
}
