using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore
{
    abstract class AbstractDM
    {
        protected const string connectionString = "Data Source=ARTUROO-PC;Initial Catalog=Store;Integrated Security=True;Pooling=False";

        public abstract bool Create(IEntity user);

    }
}
