using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore.Entity
{
    class Product : AEntity
    {
        protected string name = "";
        protected string description = "";
        protected decimal price = 0;
        protected string category = "";

        public Product()
        {
        }

        public Product(string name, string description, decimal price, string category)
        {
            Name = name;
            Description = description;
            Price = price;
            Category = category;
        }

        public Product(int id, string name, string description, decimal price, string category)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
            Category = category;
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

        //public bool Create()
        //{
        //    var result = ProductDb.Create(Name, Description, Price, Category);

        //    if (result > 0)
        //    {
        //        this.id = result;
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        //public bool Update()
        //{
        //    var result = ProductDb.Update(Id, Name, Description, Price, Category);

        //    if (result > 0)
        //    {
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        //public bool Update(Product product)
        //{
        //    var result = ProductDb.Update(Id, Name, Description, Price, Category);

        //    if (result > 0)
        //    {
        //        return true;
        //    }
        //    else
        //        return false;
        //}

        //public static Product FindById(int id)
        //{
        //    return ProductDb.FindById(id);
        //}

        //public static bool Delete(int id)
        //{
        //    return ProductDb.Delete(id);
        //}


    }
}
