using System.Threading.Tasks;

namespace ComecandoTestes.ImpostoRenda
{
    public class CalculadoraImposto
    {
        private readonly IServicoImpostoRenda _servicoImpostoRenda;

        public CalculadoraImposto(IServicoImpostoRenda servicoImpostoRenda)
        {
            _servicoImpostoRenda = servicoImpostoRenda;
        }

        public async Task<decimal> CalcularSalarioLiquido(decimal salario)
        {
            var aliquota = await _servicoImpostoRenda.ObterAliquota(salario);

            return salario - salario * aliquota / 100;
        }
    }
}