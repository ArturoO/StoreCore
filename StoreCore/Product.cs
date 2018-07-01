using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    class Product
    {
        protected int id;
        protected string name;
        protected string description;
        protected decimal price;
        protected string category;

        public Product()
        {
            this.id = 0;
            this.name = "";
            this.description = "";
            this.price = 0;
            this.category = "";
        }

        public Product(int id, string name, string description, decimal price, string category)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.price = price;
            this.category = category;
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

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;                
            }
        }

        public string Description
        {
            get
            {
                return this.description;
            }
            set
            {
                this.description = value;
            }
        }

        public decimal Price
        {
            get
            {
                return this.price;
            }
            set
            {
                if(value>0)
                    this.price = value;
            }
        }

        public string Category
        {
            get
            {
                return this.category;
            }
            set
            {
                this.category = value;             
            }
        }

    }
}
