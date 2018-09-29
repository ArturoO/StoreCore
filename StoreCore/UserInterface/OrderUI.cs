using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using StoreCore.DataMapper;
using StoreCore.Entity;
using StoreCore.Factory;

namespace StoreCore.UserInterface
{
    public class OrderUI : IConsoleUI
    {
        public void registerCommands(Dictionary<string, CommandInfo> commandsMap)
        {
            commandsMap.Add("list-orders", new CommandInfo(new string[] { "client", "admin" }, ListOrders));
            commandsMap.Add("view-order", new CommandInfo(new string[] { "client", "admin" }, ViewOrder));
        }

        
        public void ListOrders()
        {
            var user = UserFactory.GetCurrentUser2();

            using (var context = new StoreContext())
            {
                user.Orders = context.Orders
                    .Where(x=> x.UserId==user.Id)
                    .ToList();
                Console.WriteLine("---------------------------------------------------------------------");
                Console.WriteLine(" Id        | Date                | Status    | Quantity  | Total     ");
                Console.WriteLine("---------------------------------------------------------------------");

                foreach (var order in user.Orders)
                {
                    Console.WriteLine(String.Format(" {0,-10}| {1,-20}| {2,-10}| {3,-10}| {4,-10}",
                       order.Id, order.DateTime, order.Status, order.Qty, order.Total));
                    Console.WriteLine("---------------------------------------------------------------------");
                }
            }
        }

        public void ViewOrder()
        {
            Console.WriteLine("Please provide order id.");
            int Id;
            int.TryParse(Console.ReadLine(), out Id);

            using (var context = new StoreContext())
            {
                var order = context.Orders
                    .Include(x => x.Products)
                    .SingleOrDefault(x => x.Id == Id);

                if (order == null)
                {
                    Console.WriteLine("Error: Order doesn't exists.");
                    return;
                }

                Console.WriteLine("-----------------------------------------------");
                Console.WriteLine(" Name      | Price     | Category  | Quantity  ");
                Console.WriteLine("-----------------------------------------------");

                foreach (var product in order.Products)
                {
                    Console.WriteLine(String.Format(" {0,-10}| {1,-10}| {2,-10}| {3,-10}",
                       product.Name, product.Price, product.Category, product.Qty));
                    Console.WriteLine("-----------------------------------------------");
                }

                Console.WriteLine(String.Format(" Date: {0,40}", order.DateTime));
                Console.WriteLine(String.Format(" Status: {0,38}", order.Status));
                Console.WriteLine(String.Format(" Items: {0,39}", order.Qty));
                Console.WriteLine(String.Format(" Total: {0,39}", order.Total));
                Console.WriteLine("-----------------------------------------------");
            }

        }


    }
}
