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
                if(products==null)
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

        /// <summary>
        /// Checks if specified productId is in the Cart
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public bool ProductExists(int productId)
        {
            foreach (var cartProduct in Products)
            {
                if (cartProduct.ProductId == productId)
                    return true;
            }
            return false;
        }

        protected bool ProductReplace(CartProduct product)
        {
            for(var i=0; i<Products.Count; i++)
            {
                if(Products[i].ProductId == product.ProductId)
                {
                    Products[i] = product;
                    return true;
                }
            }
            return false;
        }

        protected bool ProductRemove(CartProduct product)
        {
            for (var i = 0; i < Products.Count; i++)
            {
                if (Products[i].ProductId == product.ProductId)
                {
                    Products.RemoveAt(i);
                    //Products[i] = product;
                    return true;
                }
            }
            return false;
        }

        public bool AddProduct(Product product, int qty)
        {
            bool result;
            CartProduct cartProduct = new CartProduct(Id, product.Id, qty);
            result = CartDM.AddProduct(cartProduct);
            if (result)
            {
                Products.Add(cartProduct);
                CartDM.UpdateSummary(this);
            }
            return result;
        }

        public bool UpdateProduct(Product product, int qty)
        {
            bool result;

            CartProduct cartProduct = new CartProduct(Id, product.Id, qty);

            result = CartDM.UpdateProduct(cartProduct);
            if(result)
            {
                ProductReplace(cartProduct);
                CartDM.UpdateSummary(this);
            }

            return result;
           
        }

        public bool RemoveProduct(Product product)
        {
            bool result;

            CartProduct cartProduct = new CartProduct(Id, product.Id);

            result = CartDM.RemoveProduct(cartProduct);
            if (result)
            {
                ProductRemove(cartProduct);
                CartDM.UpdateSummary(this);
            }
            return result;
        }

        public bool Checkout()
        {
            Order.Checkout(this);
            Clear();

            return true;
        }

        /// <summary>
        /// Removes all items from the cart, sets price and quantity to 0
        /// </summary>
        public void Clear()
        {
            CartProductDM.RemoveProducts(this);
            Products = null;
            CartDM.UpdateSummary(this);
        }

        public void Reload()
        {
            var updated = CartDM.FindByUser(User);
            Price = updated.Price;
            Qty = updated.Qty;
        }
        
    }
}
