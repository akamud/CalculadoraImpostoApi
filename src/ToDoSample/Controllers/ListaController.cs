using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoSample.Dados;

namespace ToDoSample.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListaController : ControllerBase
    {
        private readonly ListasRepositorio _listasRepositorio;

        public ListaController(ListasRepositorio listasRepositorio)
        {
            _listasRepositorio = listasRepositorio;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Lista lista)
        {
            await _listasRepositorio.Inserir(lista);
            
            return Ok();
        }
    }
}