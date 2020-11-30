using Microsoft.EntityFrameworkCore;

namespace ToDoSample.Dados
{
    public class ToDoContext : DbContext
    {
        public DbSet<Lista> Listas { get; set; }

        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {
        }
    }
}