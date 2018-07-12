using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore.Entity
{
    abstract class AEntity
    {
        protected int id = 0;

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
