using CalculadoraImpostoApi.Dados;
using CalculadoraImpostoApi.ImpostoRenda;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace CalculadoraImpostoApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalarioLiquidoController : ControllerBase
    {
        private readonly HistoricoCalculoRepositorio _historicoCalculoRepositorio;
        private readonly CalculadoraImposto _calculadoraImposto;

        public SalarioLiquidoController(HistoricoCalculoRepositorio historicoCalculoRepositorio,
            CalculadoraImposto calculadoraImposto)
        {
            _historicoCalculoRepositorio = historicoCalculoRepositorio;
            _calculadoraImposto = calculadoraImposto;
        }

        [HttpPost("{valorSalario:decimal}")]
        public async Task<ActionResult> Post(decimal valorSalario)
        {
            var valorCalculado = await _calculadoraImposto.CalcularSalarioLiquido(valorSalario);
            var historicoCalculo = new HistoricoCalculo
            {
                ValorSalario = valorSalario,
                ValorCalculado = valorCalculado
            };
            await _historicoCalculoRepositorio.Inserir(historicoCalculo);

            return Ok(new {valorCalculado});
        }
        
        [HttpGet("historico")]
        public async Task<ActionResult> Get()
        {
            var historicos = await _historicoCalculoRepositorio.ObterTodos();

            if (!historicos.Any())
                return NoContent();

            return Ok(new {dados = historicos});
        }
    }
}