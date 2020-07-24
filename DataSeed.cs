

using System;
using System.Collections.Generic;
using System.Linq;
using Advantage.API.Models;

namespace Advantage.API
{
    public class DataSeed 
    {
        private readonly ApiContext _context;
        private List<string> _names;

        public DataSeed(ApiContext context)
        {
            _context = context;
        }

        public void SeedData(int numCustomers, int numOrders)
        {
            if (!_context.Customers.Any())
            {
                SeedCustomers(numCustomers);
            }

            if (!_context.Orders.Any())
            {
                SeedOrders(numOrders);
            }

            if (!_context.Servers.Any())
            {
                SeedServers();
            }    

            _context.SaveChanges();

        }

        private void SeedServers()
        {
            var servers = BuildServerList();

            servers.ForEach(server => _context.Servers.Add(server));
        }

        private List<Server> BuildServerList()
        {
            return new List<Server>(4);
        }

        private void SeedOrders(int numOrders)
        {
            var orders = BuildOrderList(numOrders);

            orders.ForEach(order => _context.Orders.Add(order));
        }

        private List<Order> BuildOrderList(int numOrders)
        {
            return new List<Order>(numOrders);
        }

        public void SeedCustomers(int numCustomers)
        {
            var customers = BuildCustomerList(numCustomers);

            customers.ForEach(customer => _context.Customers.Add(customer));
        }

        private List<Customer> BuildCustomerList(int numCustomers)
        {
            List<Customer> newCustomers = new List<Customer>();

            for (int i = 1; i <= numCustomers; ++i)
            {
                string name = Helpers.GenerateRandomUniqueCustomer(_names);
                _names.Add(name);
                
                newCustomers.Add(new Customer(){
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