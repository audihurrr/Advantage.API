using System.Linq;
using Advantage.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Advantage.API.Controllers
{

    [Route("api/[controller]")]
    public class ServerController : Controller
    {
         private readonly ApiContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public ServerController(ApiContext context)
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
            var servers = _context.Servers.OrderBy(s => s.Id).ToList();
            return Ok(servers);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name="GetServer")]
        public IActionResult Get(int id)
        {
            var server = _context.Servers.First(s => s.Id == id);
            return Ok(server);
        }
    }
}