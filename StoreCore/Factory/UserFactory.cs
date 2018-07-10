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

        public static void SetCurrentUserAsGuest()
        {
            User guest = new User();
            guest.Username = "guest";
            guest.Type = "guest";
            UserFactory.currentUser = guest;
        }

        public static User GetCurrentUser()
        {
            return UserFactory.currentUser;
        }

        public static string GetCurrentUserType()
        {
            return currentUser.Type;
        }
    }
}
