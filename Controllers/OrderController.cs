using System.Collections.Generic;
using System.Linq;
using System;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Advantage.API.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ApiContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public OrderController(ApiContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Orders.OrderBy(order => order.Id));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "GetOrder")]
        public IActionResult Get(int id)
        {
            var order = _context.Orders.First(order => order.Id == id);
            return Ok(order);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageNumber"></param>
        /// <param name="resultsPerPage"></param>
        /// <returns></returns>
        [HttpGet("{pageNumber:int}/{resultsPerPage:int}")]
        public IActionResult Get(int pageNumber, int resultsPerPage)
        {
            var data = _context.Orders.
                Include(o => o.Customer).
                OrderByDescending(c => c.Placed);

            var page = new PaginatedResponse<Order>(data, pageNumber, resultsPerPage);

            var totalCount = page.Total;
            var totalPages = Math.Ceiling((double) totalCount / resultsPerPage);

            var response = new 
            {
                Page = page,
                TotalPages = totalPages
            };

            return Ok(response);
        }

        [HttpGet("ByState")]
        public IActionResult ByState()
        {
            var orders  = _context.Orders.Include(o => o.Customer).ToList();

            var groupedOrders = orders.GroupBy(o => o.Customer.State)
                .ToList()
                .Select(grp => new
                {
                    State = grp.Key,
                    Total = grp.Sum(x => x.Total)
                }).OrderBy(res => res.Total)
                .ToList();

            return Ok(groupedOrders);
        }

        [HttpGet("ByCustomer/{n:int}")]
        public IActionResult ByCustomer(int n)
        {
            var orders = _context.Orders.Include(o => o.Customer).ToList();

            var groupedOrders = orders.GroupBy(o => o.Customer.Id)
                .ToList()
                .Select(grp => new 
                {
                    Name = _context.Customers.Find(grp.Key).Name,
                    Total = grp.Sum(x => x.Total)
                }).OrderByDescending(res => res.Total)
                .Take(n)
                .ToList();

            return Ok(groupedOrders);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Post([FromBody] Order order)
        {
            if (order == null)
            {
                return BadRequest();
            }

            return CreatedAtRoute("GetOrder", new { id = order.Id }, order);
        }

    }

}