using Microsoft.EntityFrameworkCore;

namespace ToDoSample.Dados
{
    public class ImpostoRendaContext : DbContext
    {
        public DbSet<HistoricoCalculo> HistoricoCalculos { get; set; }

        public ImpostoRendaContext(DbContextOptions<ImpostoRendaContext> options) : base(options)
        {
        }
    }
}