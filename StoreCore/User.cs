using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    class User
    {
        protected int id;
        protected string firstName;
        protected string lastName;
        protected string gender;
        protected int age;

        public User()
        {
            this.id = 0;
            this.firstName = "";
            this.lastName = "";
            this.gender = "";
            this.age = 0;
        }

        public User(string firstName, string lastName, string gender, int age)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.gender = gender;
            this.age = age;
        }

        public User(int id, string firstName, string lastName, string gender, int age)
        {
            this.id = id;
            this.firstName = firstName;
            this.lastName = lastName;
            this.gender = gender;
            this.age = age;
        }

        public int Id
        {
            get
            {
                return this.id;
            }
            set
            {
                if (value > 0)
                {
                    this.id = value;
                }
            }
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


    }
}
