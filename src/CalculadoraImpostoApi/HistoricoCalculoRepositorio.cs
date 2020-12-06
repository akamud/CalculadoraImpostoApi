using System.Threading.Tasks;
using ToDoSample.Dados;

namespace ToDoSample
{
    public class HistoricoCalculoRepositorio
    {
        private readonly ImpostoRendaContext _context;

        public HistoricoCalculoRepositorio(ImpostoRendaContext context)
        {
            _context = context;
        }

        public async Task Inserir(HistoricoCalculo historicoCalculo)
        {
            await _context.AddAsync(historicoCalculo);
            await _context.SaveChangesAsync();
        }
    }
}