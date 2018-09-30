using System;
using System.Collections.Generic;
using System.Text;

namespace StoreCore.UserInterface
{
    abstract public class ConsoleUI
    {
        abstract public void registerCommands(Dictionary<string, CommandInfo> commandsMap);

        public static string RequiredTextField()
        {
            var input = "";
            input = Console.ReadLine();
            while (input == "")
            {
                Console.WriteLine("Field is required, please enter the value.");
                input = Console.ReadLine();
            }
            while (input == "") ;
            return input;
        }

        public static string RequiredEmailField()
        {
            var input = "";
            input = Console.ReadLine();
            
            while(!isValidEmail(input))
            {
                Console.WriteLine("Field is required, please enter the value.");
                input = Console.ReadLine();
            }
            return input;
        }

        public static int RequiredIntField()
        {
            var input = "";
            int valueInt;
            input = Console.ReadLine();
            bool isInt = int.TryParse(input, out valueInt);
            while (!isInt)
            {
                Console.WriteLine("Field is required, please enter the value.");
                input = Console.ReadLine();
                isInt = int.TryParse(input, out valueInt);
            }
            return valueInt;
        }

        public static decimal RequiredDecimalField()
        {
            var input = "";
            decimal valueDecimal;
            input = Console.ReadLine();
            bool isDecimal = decimal.TryParse(input, out valueDecimal);
            while (!isDecimal)
            {
                Console.WriteLine("Field is required, please enter the value.");
                input = Console.ReadLine();
                isDecimal = decimal.TryParse(input, out valueDecimal);
            }
            return valueDecimal;
        }

        public static string RequiredPasswordField()
        {
            StringBuilder passwordBuilder = new StringBuilder("");
            while (true)
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    if (passwordBuilder.Length == 0)
                    {
                        Console.WriteLine("Field is required, please enter the value.");
                        continue;
                    }
                    else
                    {
                        Console.WriteLine();
                        break;
                    }                    
                }
                passwordBuilder.Append(key.KeyChar);
                Console.Write("\b*");
            }
            return passwordBuilder.ToString();
        }

        public static bool isValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }
}
