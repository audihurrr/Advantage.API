using Microsoft.EntityFrameworkCore;

namespace Advantage.API.Models
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }

        public DbSet<Customer> Customers { get; set; }
    }
}