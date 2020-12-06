using System.Threading.Tasks;

namespace CalculadoraImpostoApi.ImpostoRenda
{
    public interface IServicoImpostoRenda
    {
        Task<decimal> ObterAliquota(decimal valor);
    }
}