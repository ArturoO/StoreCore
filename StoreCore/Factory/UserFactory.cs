using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore.Factory
{
    class UserFactory
    {
        protected static User currentUser = null;

        public static void SetCurrentUser(User user)
        {
            UserFactory.currentUser = user;
        }

        public static User GetCurrentUser()
        {
            return UserFactory.currentUser;
        }

        public static string GetCurrentUserType()
        {
            if (currentUser == null)
                return "guest";
            else
                return currentUser.Type;
        }
    }
}
