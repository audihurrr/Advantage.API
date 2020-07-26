using System;
using System.Collections.Generic;
using System.Linq;
using Advantage.API.Models;

namespace Advantage.API
{
    public class DataSeed 
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ApiContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public DataSeed(ApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numCustomers"></param>
        /// <param name="numOrders"></param>
        public void SeedData(int numCustomers, int numOrders)
        {
            if (!_context.Customers.Any())
            {
                SeedCustomers(numCustomers);
                _context.SaveChanges();
            }

            if (!_context.Orders.Any())
            {
                SeedOrders(numOrders);
                _context.SaveChanges();
            }

            if (!_context.Servers.Any())
            {
                SeedServers();
                _context.SaveChanges();
            }    
        }

        /// <summary>
        /// 
        /// </summary>
        private void SeedServers()
        {
            var servers = BuildServerList();

            servers.ForEach(server => _context.Servers.Add(server));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private List<Server> BuildServerList()
        {
            return new List<Server>() 
            {
                new Server() 
                {
                    Id = 1,
                    Name = "Dev-Web",
                    IsOnline = true
                },
                new Server() 
                {
                    Id = 2,
                    Name = "Dev-Mail",
                    IsOnline = true
                },
                new Server() 
                {
                    Id = 3,
                    Name = "Dev-Services",
                    IsOnline = true
                },
                new Server() 
                {
                    Id = 4,
                    Name = "QA-Web",
                    IsOnline = true
                },
                new Server() 
                {
                    Id = 5,
                    Name = "QA-Mail",
                    IsOnline = false
                },
                new Server() 
                {
                    Id = 6,
                    Name = "QA-Services",
                    IsOnline = false
                },
                new Server() 
                {
                    Id = 7,
                    Name = "Prod-Web",
                    IsOnline = true
                },
                new Server() 
                {
                    Id = 8,
                    Name = "Prod-Mail",
                    IsOnline = true
                },
                new Server() 
                {
                    Id = 9,
                    Name = "Prod-Services",
                    IsOnline = false
                }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numOrders"></param>
        private void SeedOrders(int numOrders)
        {
            var orders = BuildOrderList(numOrders);

            orders.ForEach(order => _context.Orders.Add(order));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numOrders"></param>
        /// <returns></returns>
        private List<Order> BuildOrderList(int numOrders)
        {
            var newOrders = new List<Order>();
            var rand = new Random();
            var customers = _context.Customers.ToList();
            
            for (int i = 1; i <= numOrders; ++i)
            {
                var randCustomerId = rand.Next(1, _context.Customers.Count());
                var placed = Helpers.GetRandomOrderPlaced();
                var completed = Helpers.GetRandomOrderCompleted(placed);
                
                newOrders.Add(new Order(){
                    Id = i,
                    Customer = customers.First(c => c.Id == randCustomerId),
                    Total = Helpers.GetRandomOrderTotal(),
                    Placed = placed,
                    Fulfilled = completed
                });
            }

            return newOrders;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numCustomers"></param>
        public void SeedCustomers(int numCustomers)
        {
            var customers = BuildCustomerList(numCustomers);

            customers.ForEach(customer => _context.Customers.Add(customer));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="numCustomers"></param>
        /// <returns></returns>    
        private List<Customer> BuildCustomerList(int numCustomers)
        {
            List<Customer> newCustomers = new List<Customer>();
            List<string> names = new List<string>();

            for (int i = 1; i <= numCustomers; ++i)
            {
                string name = Helpers.GenerateRandomUniqueCustomer(names);
                names.Add(name);
                
                newCustomers.Add(new Customer() 
                {
                    Id = i,
                    Name = name,
                    Email = Helpers.GenerateRandomEmail(name),
                    State = Helpers.GenerateRandomState()
                });
            }

            return newCustomers;
        }
    }
}