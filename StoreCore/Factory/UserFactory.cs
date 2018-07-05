using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore.Factory
{
    class UserFactory
    {
        protected static User user;

        public static void SetCurrentUser(User user)
        {
            UserFactory.user = user;
        }

        public static User GetCurrentUser()
        {
            return UserFactory.user;
        }
    }
}
