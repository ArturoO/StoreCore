using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    abstract class Entity
    {
        protected int id;

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

    }
}
