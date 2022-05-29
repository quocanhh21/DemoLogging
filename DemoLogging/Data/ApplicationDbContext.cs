using Microsoft.EntityFrameworkCore;

namespace DemoLogging.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<Todo> Todos { get; set; }
    }
}
