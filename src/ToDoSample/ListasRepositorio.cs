using System.Threading.Tasks;
using ToDoSample.Dados;

namespace ToDoSample
{
    public class ListasRepositorio
    {
        private readonly ToDoContext _context;

        public ListasRepositorio(ToDoContext context)
        {
            _context = context;
        }

        public async Task Inserir(Lista lista)
        {
            await _context.Listas.AddAsync(lista);
            await _context.SaveChangesAsync();
        }
    }
}