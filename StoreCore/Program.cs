using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using StoreCore.Factory;
using StoreCore.UserInterface;

namespace StoreCore
{
    class Program
    {
        static void Main(string[] args)
        {
            Config();

            MainUI mainUI = new MainUI();
            mainUI.Start();
        }

        static void Config()
        {
            //Create customCulture to use dot as decimal separator
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";
            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

            //Set guest as a current user
            UserFactory.SetCurrentUserAsGuest();
        }

    }
}
