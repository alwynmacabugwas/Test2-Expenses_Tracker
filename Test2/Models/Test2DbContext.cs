using Microsoft.EntityFrameworkCore;

namespace Test2.Models
{
    public class Test2DbContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }
        public Test2DbContext(DbContextOptions<Test2DbContext> options) : base(options)
        {

        }
    }
}
