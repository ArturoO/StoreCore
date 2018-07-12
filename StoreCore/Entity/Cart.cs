using System;
using System.Collections.Generic;
using System.Text;
using StoreCore.DataMapper;
using StoreCore.Factory;

namespace StoreCore.Entity
{
    class Cart: AEntity
    {
        protected int user_id = 0;
        protected decimal price = 0;
        protected int qty = 0;

        protected User user = null;
        protected List<CartProduct> products = null;
        
        public int UserId
        {
            get
            {
                return user_id;
            }
            set
            {
                user_id = value;
            }
        }

        public decimal Price
        {
            get
            {
                return price;
            }
            set
            {
                price = value;
            }
        }

        public User User
        {
            get
            {
                if(user==null)
                    user = UserDM.FindById(UserId);
                return user;
            }
            set
            {
                user = value;
            }
        }

        public List<CartProduct> Products
        {
            get
            {
                products = CartProductDM.ListProducts(this);
                return products;
            }
            set
            {
                products = value;
            }
        }

        public int Qty
        {
            get
            {
                return qty;
            }
            set
            {
                qty = value;
            }
        }

        public bool AddProduct(Product product, int qty)
        {
            return CartDM.AddProduct(this, product, qty);
        }

        //public List<Product> ListProducts()
        //{
        //    return CartDM.ListProducts(this);
        //}

    }
}
