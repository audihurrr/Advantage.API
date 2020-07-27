using System.Collections.Generic;
using System.Linq;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(int id)
        {
            var order = _context.Orders.First(order => order.Id == id);
            return Ok(order);
        }

        [HttpGet("{pageNumber}/{resultsPerPage}")]
        public IActionResult Get(int pageNumber, int resultsPerPage)
        {
            var orders = new List<Order>();
            var startIndex = pageNumber * resultsPerPage;
            var endIndex = startIndex + resultsPerPage;

            if (startIndex > _context.Orders.Count())
            {
                return BadRequest();
            }
            
            if (endIndex > _context.Orders.Count())
            {
                endIndex = _context.Orders.Count();
            }

            orders.AddRange(_context.Orders.Skip(startIndex).Take(endIndex - startIndex).ToList());

            return Ok(orders);
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