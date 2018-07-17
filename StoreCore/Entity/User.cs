using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using StoreCore.Factory;
using StoreCore.DataMapper;

namespace StoreCore.Entity
{
    class User : AEntity
    {
        protected string username = "";
        protected string password = "";
        protected string type = "";
        protected string firstName = "";
        protected string lastName = "";
        protected string gender = "";
        protected int age = 0;
        protected string email = "";

        protected Cart cart = null;


        public User()
        {
        }

        public User(string username, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Age = age;
            Username = username;
            Password = password;
            Type = type;
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

        public User(int id, string firstName, string lastName, string gender, int age, string username, string password, string type, string email)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Age = age;
            Email = email;
        }

        public string FirstName
        {
            get
            {
                return this.firstName;
            }
            set
            {
                this.firstName = value;                
            }
        }

        public string LastName
        {
            get
            {
                return this.lastName;
            }
            set
            {
                this.lastName = value;
            }
        }

        public string Gender
        {
            get
            {
                return this.gender;
            }
            set
            {
                this.gender = value;
            }
        }

        public int Age
        {
            get
            {
                return this.age;
            }
            set
            {
                if (value > 0)
                {
                    this.age = value;
                }
            }
        }

        public string Username
        {
            get
            {
                return this.username;
            }
            set
            {
                this.username = value;
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = value;
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }
            set
            {
                this.email = value;
            }
        }

        public string Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value;
            }
        }

        public Cart Cart
        {
            get
            {
                if (cart == null)
                {
                    cart = CartDM.FindByUser(this);
                    if (cart.Id == 0)
                        CartDM.Create(this);

                }

                return cart;
            }
            set
            {
                cart = value;
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

            for(int i=0; i<20; i++)
            {
                if(hashBytes[16+i]!=hash[i])
                {
                    result = false;
                    break;
                }
            }

            return result;
            
        }

    }
}
