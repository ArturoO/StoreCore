using System;
using System.Collections.Generic;
using System.Text;
using StoreCore.Entity;

namespace StoreCore.Factory
{
    class UserFactory
    {
        protected static User currentUser = null;
        protected static User2 currentUser2 = null;

        public static void SetCurrentUser(User user)
        {
            UserFactory.currentUser = user;
        }

        public static void SetCurrentUser2(User2 user)
        {
            UserFactory.currentUser2 = user;
        }

        public static void SetCurrentUserAsGuest()
        {
            User guest = new User();
            guest.Username = "guest";
            guest.Type = "guest";
            UserFactory.currentUser = guest;
        }

        public static void SetCurrentUserAsGuest2()
        {
            User2 guest = new User2();
            guest.Username = "guest";
            guest.Type = "guest";
            UserFactory.currentUser2 = guest;
        }

        public static User GetCurrentUser()
        {
            return UserFactory.currentUser;
        }

        public static User2 GetCurrentUser2()
        {
            return UserFactory.currentUser2;
        }

        public static string GetCurrentUserType()
        {
            return currentUser.Type;
        }

        public static string GetCurrentUserType2()
        {
            return currentUser2.Type;
        }

    }
}
